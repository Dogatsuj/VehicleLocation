using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using VehiculeLocation.Frontend.Models;

namespace VehiculeLocation.Frontend.Pages
{
    
    //La récupération de l'ID dans le controller backend dans InfoCars.cshtml.cs
    public class InfoCarsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public Vehicle? Vehicle { get; set; }

        public InfoCarsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Console.WriteLine($" InfoCars - Requested ID: {Id}");
            
            try
            {
                var client = _httpClientFactory.CreateClient("ApiBackend");
                
                Console.WriteLine($"Calling API: api/Vehicle/{Id}");
                var response = await client.GetAsync($"api/Vehicle/{Id}");
                
                Console.WriteLine($"API Response Status: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($" API Content: {content}");

                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                    Vehicle = JsonSerializer.Deserialize<Vehicle>(content, options);

                    if (Vehicle == null)
                    {
                        Console.WriteLine(" Vehicle is null after deserialization");
                        return NotFound();
                    }
                    
                    Console.WriteLine($" Vehicle loaded: {Vehicle.Brand} {Vehicle.Model}");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return NotFound();
                }
                else
                {
                    Console.WriteLine($" API Call failed: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Exception: {ex.Message}");
                Console.WriteLine($" StackTrace: {ex.StackTrace}");
            }

            return Page();
        }
    }
}
