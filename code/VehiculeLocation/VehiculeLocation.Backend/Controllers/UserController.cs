using Microsoft.AspNetCore.Mvc;
using VehiculeLocation.Backend.Data;
using VehiculeLocation.Backend.Models;
using VehiculeLocation.Backend.Services;

[ApiController]
[Route("api/[controller]")] // L'URL de base sera /api/User
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IUserService _userService;
    public UserController(AppDbContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    /// <summary>
    /// Ajoute un nouvel utilisateur.
    /// POST: api/Vehicle
    /// </summary>
    /// <param name="user">Le véhicule à ajouter.</param>
    /// <returns>Le véhicule créé avec son ID.</returns>
    /// <response code="201">Retourne le véhicule créé.</response>
    /// <response code="400">Si les données fournies sont invalides.</response>
    [HttpPost]
    public async Task<ActionResult<User>> CreateVehicle([FromBody] User user)
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

        // on s'assure que isAdmin n'a pas été mis a true
        user.IsAdmin = false;

        // Ajout de l'utilisateur dans la base de données
        _context.User.Add(user);
        await _context.SaveChangesAsync();

        // Retourne le véhicule créé avec le code HTTP 201
        return StatusCode(StatusCodes.Status201Created, user);
    }
    
}
