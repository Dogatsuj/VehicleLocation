using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    public UserController(AppDbContext context, IUserService userService, IPasswordHasherService hasherService)
    {
        _context = context;
        _userService = userService;
        _hasherService = hasherService;
    }

    /// <summary>
    /// Ajoute un nouvel utilisateur.
    /// POST: api/User
    /// </summary>
    /// <param name="user">Le véhicule à ajouter.</param>
    /// <returns>Le véhicule créé avec son ID.</returns>
    /// <response code="201">Retourne le véhicule créé.</response>
    /// <response code="400">Si les données fournies sont invalides.</response>
    [HttpPost]
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


        user.Password = _hasherService.HashPassword(user.Password);

        // on s'assure que isAdmin n'a pas été mis a true
        user.IsAdmin = false;

        // Ajout de l'utilisateur dans la base de données
        _context.User.Add(user);
        await _context.SaveChangesAsync();

        // Par sécurité on supprime le mot de passe après l'insertion
        user.Password = null;

        // Retourne le véhicule créé avec le code HTTP 201
        return StatusCode(StatusCodes.Status201Created, user);
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

        // ICI : Retourner le JWT (Jeton d'authentification) - Cette partie reste à implémenter
        return Ok(new { Message = "Authentification réussie.", UserId = user.Id /*, Token = "..." */ });
    }    
}
