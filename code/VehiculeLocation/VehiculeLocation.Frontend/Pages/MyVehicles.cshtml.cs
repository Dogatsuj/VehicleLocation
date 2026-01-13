using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using VehiculeLocation.Frontend.Models;

namespace VehiculeLocation.Frontend.Pages
{
    public class MyVehiclesModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public List<Vehicle> vehicles { get; set; } = new List<Vehicle>();

        public MyVehiclesModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync()
        {
            try
            {
                string token = Request.Cookies["token"];
                var client = _httpClientFactory.CreateClient("ApiBackend");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync("api/User/vehicles");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                    vehicles = JsonSerializer.Deserialize<List<Vehicle>>(content, options) ?? new List<Vehicle>();
                }
                else
                {
                    Console.WriteLine($"API Call failed: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}