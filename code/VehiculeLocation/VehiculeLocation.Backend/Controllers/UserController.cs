using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VehiculeLocation.Backend.Data;
using VehiculeLocation.Backend.Models;
using VehiculeLocation.Backend.Services;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IUserService _userService;
    private readonly IPasswordHasherService _hasherService;
    private readonly IConfiguration _configuration;

    public UserController(AppDbContext context, IUserService userService, IPasswordHasherService hasherService, IConfiguration configuration)
    {
        _context = context;
        _userService = userService;
        _hasherService = hasherService;
        _configuration = configuration;
    }

    /// <summary>
    /// Ajoute un nouvel utilisateur.
    /// POST: api/User/Register
    /// </summary>
    /// <param name="user">Le véhicule à ajouter.</param>
    /// <returns>Le véhicule créé avec son ID.</returns>
    /// <response code="201">Retourne le véhicule créé.</response>
    /// <response code="400">Si les données fournies sont invalides.</response>
    [HttpPost("register")]
    public async Task<ActionResult<User>> Register([FromBody] User user)
    {
        if (user == null)
        {
            return BadRequest("L'utilisateur fourni est nul.");
        }

        if (string.IsNullOrWhiteSpace(user.Username))
            return BadRequest("Le nom d'utilisateur est obligatoire.");

        if (string.IsNullOrWhiteSpace(user.Password))
            return BadRequest("Le modèle est obligatoire.");

        if (await _context.User.AnyAsync(u => u.Username == user.Username))
        {
            return BadRequest("Ce nom d'utilisateur est déjà utilisé.");
        }
        user.Password = _hasherService.HashPassword(user.Password);

        user.IsAdmin = false;

        _context.User.Add(user);
        await _context.SaveChangesAsync();

        var tokenString = GenerateJwtToken(user);

        return StatusCode(StatusCodes.Status201Created, new
        {
            Token = tokenString,
            Username = user.Username,
            IsAdmin = user.IsAdmin
        });
    }

    /// <summary>
    /// Authentifie un utilisateur et renvoie un token (Logique de token à implémenter).
    /// POST: api/User/login
    /// </summary>
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _context.User.FirstOrDefaultAsync(u => u.Username == model.Username);

        if (user == null)
        {

            return Unauthorized("Nom d'utilisateur ou mot de passe invalide.");
        }

        var isPasswordValid = _hasherService.VerifyPassword(model.Password, user.Password);

        if (!isPasswordValid)
        {
            return Unauthorized("Nom d'utilisateur ou mot de passe invalide.");
        }

        var tokenString = GenerateJwtToken(user);

        return Ok(new { Token = tokenString, Username = user.Username, IsAdmin = user.IsAdmin });
    }

    private string GenerateJwtToken(User user)
    {
        var config = _configuration.GetSection("Jwt");
        var key = Encoding.ASCII.GetBytes(config["Key"]);

        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
    };


        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = config["Issuer"],
            Audience = config["Audience"],
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        // 3. Générer le Token
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    /// <summary>
    /// Déconnecte l'utilisateur en supprimant le token JWT (cookie).
    /// POST: api/User/logout
    /// </summary>
    [HttpPost("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        const string TokenCookieName = "authToken";

        if (!Request.Cookies.ContainsKey(TokenCookieName))
        {
            return Ok(new { Message = "Déconnexion effectuée (aucune session active trouvée)." });
        }

        Response.Cookies.Delete(TokenCookieName);

        return Ok(new { Message = "Déconnexion réussie." });
    }
}
