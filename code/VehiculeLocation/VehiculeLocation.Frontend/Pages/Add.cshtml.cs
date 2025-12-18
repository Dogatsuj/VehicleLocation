using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace VehiculeLocation.Frontend.Pages
{
    public class AddModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public AddModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [BindProperty]
        public VehicleDto Vehicle { get; set; } = new VehicleDto();

        [TempData]
        public string? ErrorMessage { get; set; }

        [TempData]
        public string? SuccessMessage { get; set; }

        public void OnGet()
        {
            // Initialize with default values if needed
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string token = Request.Cookies["token"];
            if (token == null)
            {
                ErrorMessage = "Please log in to add a vehicle";
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var httpClient = _httpClientFactory.CreateClient("ApiBackend");

                // Serialize with camelCase naming policy to match API expectations
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                var jsonContent = JsonSerializer.Serialize(Vehicle, options);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync($"api/vehicle", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/List");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ErrorMessage = $"Error adding vehicle: {response.StatusCode}. {errorContent}";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred: {ex.Message}";
                return Page();
            }
        }
    }

    public class VehicleDto
    {
        [Required(ErrorMessage = "Brand is required")]
        [StringLength(100, ErrorMessage = "Brand cannot exceed 100 characters")]
        public string Brand { get; set; } = string.Empty;

        [Required(ErrorMessage = "Model is required")]
        [StringLength(100, ErrorMessage = "Model cannot exceed 100 characters")]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "Number of seats is required")]
        [Range(1, 9, ErrorMessage = "Seats must be between 1 and 9")]
        public int Seats { get; set; }

        [Required(ErrorMessage = "Daily rental price is required")]
        [Range(0.01, 999999.99, ErrorMessage = "Price must be greater than 0")]
        [Display(Name = "Daily Rental Price")]
        public decimal DailyRentalPrice { get; set; }

        [Required(ErrorMessage = "Motorisation is required")]
        public string Motorisation { get; set; } = string.Empty;

        [Required(ErrorMessage = "Transmission type is required")]
        [Display(Name = "Automatic Transmission")]
        public bool IsAutomaticTransmission { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        [Url(ErrorMessage = "Please enter a valid URL")]
        [Display(Name = "Image URL")]
        public string? ImagePath { get; set; }
    }
}