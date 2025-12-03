using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehiculeLocation.Backend.Data;
using VehiculeLocation.Backend.Models;

[ApiController]
[Route("api/[controller]")] // L'URL de base sera /api/Vehicule
public class VehicleController : ControllerBase
{
    private readonly AppDbContext _context;

    public VehicleController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Récupère la liste de tous les véhicules.
    /// GET: api/Vehicule
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicules()
    {
        // Retourne la liste complète des véhicules de la base de données
        return await _context.Vehicles.ToListAsync();
    }

    /// <summary>
    /// Récupère un véhicule par son ID.
    /// GET: api/Vehicle/5
    /// </summary>
    /// <param name="id">L'ID du véhicule à récupérer.</param>
    /// <returns>Le véhicule correspondant à l'ID spécifié.</returns>
    /// <response code="200">Retourne le véhicule trouvé.</response>
    /// <response code="404">Si le véhicule n'existe pas.</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<Vehicle>> GetVehicle(int id)
    {
        var vehicle = await _context.Vehicles.FindAsync(id);

        if (vehicle == null)
        {
            return NotFound();
        }

        return vehicle;
    }

    /// <summary>
    /// Récupère la liste de toutes les périodes de location pour un véhicule donné.
    /// GET: api/Vehicule/{vehiculeId}/locations
    /// </summary>
    /// <param name="vehiculeId">L'ID du véhicule à vérifier.</param>
    [HttpGet("{vehiculeId}/locations")]
    public async Task<ActionResult<IEnumerable<Rental>>> GetLocationsByVehicule(int vehiculeId)
    {
        // 1. Vérifier si le véhicule existe
        if (!await _context.Vehicles.AnyAsync(v => v.Id == vehiculeId))
        {
            return NotFound($"Véhicule avec l'ID {vehiculeId} non trouvé.");
        }

        // 2. Récupérer toutes les locations liées à cet ID
        var locations = await _context.Locations
                                      .Where(l => l.VehicleId == vehiculeId)
                                      .ToListAsync();

        return locations;
    }
    /// <summary>
    /// Vérifie si le véhicule ciblé n'a AUCUNE location sur le créneau donné.
    /// GET: api/Vehicule/{vehiculeId}/available?start={dateDebut}&end={dateFin}
    /// </summary>
    /// <param name="vehiculeId">L'ID du véhicule à vérifier.</param>
    /// <param name="dateDebut">Date et heure de début du créneau (format ISO 8601).</param>
    /// <param name="dateFin">Date et heure de fin du créneau (format ISO 8601).</param>
    [HttpGet("{vehiculeId}/available")]
    public async Task<ActionResult<bool>> CheckAvailability(
        int vehiculeId,
        [FromQuery] DateTime dateDebut,
        [FromQuery] DateTime dateFin)
    {
        // 1. Validation de base du créneau
        if (dateDebut >= dateFin)
        {
            return BadRequest("La date de début doit précéder la date de fin.");
        }

        // 2. Vérifier l'existence du véhicule
        if (!await _context.Vehicles.AnyAsync(v => v.Id == vehiculeId))
        {
            return NotFound($"Véhicule avec l'ID {vehiculeId} non trouvé.");
        }

        // 3. Logique de chevauchement de créneau (Overlap Logic) :
        // Un chevauchement existe si : (Début_Actuel < Fin_Nouvelle) ET (Fin_Actuelle > Début_Nouvelle)

        var isBooked = await _context.Locations
            .Where(l => l.VehicleId == vehiculeId)
            .AnyAsync(l => l.DateStart < dateFin && l.DateEnd> dateDebut);

        // Si isBooked est VRAI, cela signifie qu'une location existe sur ce créneau.
        // On retourne l'inverse : VRAI si aucune location n'est trouvée (DISPONIBLE).
        return !isBooked;
    }

    /// <summary>
    /// Recherche des véhicules selon plusieurs critères et disponibilité optionnelle.
    /// Exemple d'appel :
    /// GET /api/Vehicle/research?brand=Citroën&seats=5&isAutomaticTransmission=true&dateDebut=2025-11-12&dateFin=2025-11-14
    /// </summary>
    /// <param name="brand">Filtre par marque du véhicule.</param>
    /// <param name="model">Filtre par modèle du véhicule.</param>
    /// <param name="seats">Filtre par nombre de places.</param>
    /// <param name="maxDailyPrice">Filtre par prix journalier maximum.</param>
    /// <param name="isAutomaticTransmission">Filtre par type de transmission.</param>
    /// <param name="motorisation">Filtre par type de motorisation.</param>
    /// <param name="dateDebut">Date et heure de début pour vérifier la disponibilité.</param>
    /// <param name="dateFin">Date et heure de fin pour vérifier la disponibilité.</param>
    [HttpGet("research")]
    public async Task<ActionResult<IEnumerable<Vehicle>>> Research(
        [FromQuery] string? brand,
        [FromQuery] string? model,
        [FromQuery] int? seats,
        [FromQuery] float? maxDailyPrice,
        [FromQuery] bool? isAutomaticTransmission,
        [FromQuery] TypeMotorisationEnum? motorisation,
        [FromQuery] DateTime? dateDebut,
        [FromQuery] DateTime? dateFin)
    {
        // Début de la requête
        var query = _context.Vehicles.AsQueryable();

        // Application des filtres de base
        if (!string.IsNullOrEmpty(brand))
            query = query.Where(v => v.Brand.ToLower().Contains(brand.ToLower()));

        if (!string.IsNullOrEmpty(model))
            query = query.Where(v => v.Model.ToLower().Contains(model.ToLower()));

        if (seats.HasValue)
            query = query.Where(v => v.Seats == seats.Value);

        if (maxDailyPrice.HasValue)
            query = query.Where(v => v.DailyRentalPrice <= maxDailyPrice.Value);

        if (isAutomaticTransmission.HasValue)
            query = query.Where(v => v.IsAutomaticTransmission == isAutomaticTransmission.Value);

        if (motorisation.HasValue)
            query = query.Where(v => v.Motorisation == motorisation.Value);

        // Si des dates sont fournies, on filtre sur la disponibilité
        if (dateDebut.HasValue && dateFin.HasValue)
        {
            if (dateDebut >= dateFin)
                return BadRequest("La date de début doit précéder la date de fin.");

            query = query.Where(v =>
                !_context.Locations.Any(l =>
                    l.VehicleId == v.Id &&
                    l.DateStart < dateFin.Value &&
                    l.DateEnd > dateDebut.Value
                )
            );
        }

        var result = await query.ToListAsync();

        if (!result.Any())
            return NotFound("Aucun véhicule ne correspond aux critères de recherche.");

        return Ok(result);
    }

    /// <summary>
    /// Ajoute un nouveau véhicule.
    /// POST: api/Vehicule
    /// </summary>
    /// <param name="vehicle">Le véhicule à ajouter.</param>
    /// <returns>Le véhicule créé avec son ID.</returns>
    /// <response code="201">Retourne le véhicule créé.</response>
    /// <response code="400">Si les données fournies sont invalides.</response>
    [HttpPost]
    public async Task<ActionResult<Vehicle>> CreateVehicle([FromBody] Vehicle vehicle)
    {
        // Validation basique
        if (vehicle == null)
        {
            return BadRequest("Le véhicule fourni est nul.");
        }

        if (string.IsNullOrWhiteSpace(vehicle.Brand))
            return BadRequest("La marque est obligatoire.");

        if (string.IsNullOrWhiteSpace(vehicle.Model))
            return BadRequest("Le modèle est obligatoire.");

        if (vehicle.Seats <= 0)
            return BadRequest("Le nombre de places doit être supérieur à 0.");

        if (vehicle.DailyRentalPrice < 0)
            return BadRequest("Le prix journalier doit être positif.");

        // Ajout du véhicule dans la base de données
        _context.Vehicles.Add(vehicle);
        await _context.SaveChangesAsync();

        // Retourne le véhicule créé avec le code HTTP 201
        return CreatedAtAction(nameof(GetVehicle), new { id = vehicle.Id }, vehicle);
    }

    /// <summary>
    /// Ajoute une nouvelle location à un véhicule existant.
    /// POST: api/Vehicule/{vehiculeId}/locations
    /// </summary>
    /// <param name="vehiculeId">ID du véhicule pour lequel créer la location.</param>
    /// <param name="rental">Détails de la location à ajouter.</param>
    /// <returns>La location créée.</returns>
    [HttpPost("{vehiculeId}/locations")]
    public async Task<ActionResult<Rental>> AddRental(int vehiculeId, [FromBody] Rental rental)
    {
        // Vérifier si le véhicule existe
        var vehicle = await _context.Vehicles.FindAsync(vehiculeId);
        if (vehicle == null)
            return NotFound($"Véhicule avec l'ID {vehiculeId} non trouvé.");

        // Validation des dates
        if (rental.DateStart >= rental.DateEnd)
            return BadRequest("La date de début doit précéder la date de fin.");

        // Vérifier la disponibilité
        bool isBooked = await _context.Locations
            .Where(l => l.VehicleId == vehiculeId)
            .AnyAsync(l => l.DateStart < rental.DateEnd && l.DateEnd > rental.DateStart);

        if (isBooked)
            return BadRequest("Le véhicule n'est pas disponible sur ce créneau.");

        // Associer la location au véhicule
        rental.VehicleId = vehiculeId;

        // Ajouter la location
        _context.Locations.Add(rental);
        await _context.SaveChangesAsync();

        // Retourner la location créée avec code HTTP 201
        return CreatedAtAction(nameof(GetLocationsByVehicule), new { vehiculeId = vehiculeId }, rental);
    }

}