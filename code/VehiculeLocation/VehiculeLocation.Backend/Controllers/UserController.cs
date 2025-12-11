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
[Route("api/[controller]")] // L'URL de base sera /api/User
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
        // Validation basique
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

        // on s'assure que isAdmin n'a pas été mis a true
        user.IsAdmin = false;

        // Ajout de l'utilisateur dans la base de données
        _context.User.Add(user);
        await _context.SaveChangesAsync();

        var tokenString = GenerateJwtToken(user);

        // Retourne le véhicule créé avec le code HTTP 201
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
        // Récupérer les paramètres JWT depuis la configuration
        var config = _configuration.GetSection("Jwt");
        var key = Encoding.ASCII.GetBytes(config["Key"]);

        // 1. Définir les Claims (Payload) : informations sur l'utilisateur à inclure dans le token
        var claims = new List<Claim>
    {
        // Ceci est l'ID unique utilisé pour identifier l'utilisateur (utile pour l'autorisation)
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
    };

        // 2. Créer l'objet Token
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1), // Expiration du token (ex: 1 heure)
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
    [Authorize] // Optionnel, mais bonne pratique
    public IActionResult Logout()
    {
        // 1. Déterminer le nom du cookie contenant le JWT (doit être le même que dans Login/Register)
        const string TokenCookieName = "authToken";

        // 2. Vérifier si le cookie existe
        if (!Request.Cookies.ContainsKey(TokenCookieName))
        {
            // L'utilisateur est déjà déconnecté ou le cookie a expiré
            return Ok(new { Message = "Déconnexion effectuée (aucune session active trouvée)." });
        }

        // 3. Demander au navigateur de SUPPRIMER le cookie
        // La méthode Append avec une date d'expiration passée ou la méthode Delete le fait.
        Response.Cookies.Delete(TokenCookieName);

        // 4. Réponse au client
        return Ok(new { Message = "Déconnexion réussie." });
    }
}
