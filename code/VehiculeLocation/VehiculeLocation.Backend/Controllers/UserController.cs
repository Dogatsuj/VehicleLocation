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
    /// POST: api/User/register
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
    /// Récupère toutes les réservations effectuées par l'utilisateur connecté.
    /// GET: api/User/locations
    /// </summary>
    [HttpGet("locations")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Rental>>> GetMyLocations()
    {
        // 1. Récupérer l'ID de l'utilisateur à partir du token
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim)) return Unauthorized("Utilisateur non identifié.");

        int userId = int.Parse(userIdClaim);

        // 2. Chercher les locations liées à cet utilisateur
        // On inclut souvent .Include(l => l.Vehicle) si tu veux afficher les détails du véhicule loué
        var locations = await _context.Locations
            .Where(l => l.UserId == userId)
            .ToListAsync();

        return Ok(locations);
    }

    /// <summary>
    /// Récupère tous les véhicules mis en location par l'utilisateur connecté (propriétaire).
    /// GET: api/User/vehicles
    /// </summary>
    [HttpGet("vehicles")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Vehicle>>> GetMyVehicles()
    {
        // 1. Récupérer l'ID de l'utilisateur à partir du token
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim)) return Unauthorized("Utilisateur non identifié.");

        int userId = int.Parse(userIdClaim);

        // 2. Chercher les véhicules dont l'utilisateur est le propriétaire
        var vehicles = await _context.Vehicles
            .Where(v => v.UserId == userId)
            .ToListAsync();

        return Ok(vehicles);
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
