using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using VehiculeLocation.Frontend.Models;

namespace VehiculeLocation.Frontend.Pages
{
    public class InfoCarsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public Vehicle? Vehicle { get; set; }

        [BindProperty]
        public string? DateStart { get; set; }

        [BindProperty]
        public string? DateEnd { get; set; }

        public InfoCarsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient("ApiBackend");
            var response = await client.GetAsync($"api/Vehicle/{Id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var content = await response.Content.ReadAsStringAsync();
            Vehicle = JsonSerializer.Deserialize<Vehicle>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (Vehicle == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string token = Request.Cookies["token"];
            if (token == null)
            {
                ModelState.AddModelError(string.Empty, "Please log in to rent a vehicle");
                await OnGetAsync();
                return Page();
            }
            {
                if (string.IsNullOrEmpty(DateStart) || string.IsNullOrEmpty(DateEnd))
                {
                    ModelState.AddModelError(string.Empty, "Please select start and end dates.");
                    await OnGetAsync();
                    return Page();
                }

                var client = _httpClientFactory.CreateClient("ApiBackend");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var body = new
                {
                    vehicleId = Id,
                    dateStart = DateStart,
                    dateEnd = DateEnd
                };

                var jsonBody = JsonSerializer.Serialize(body);


                var content = new StringContent(
                    JsonSerializer.Serialize(body),
                    System.Text.Encoding.UTF8,
                    "application/json"
                );

                var response = await client.PostAsync($"api/Vehicle/{Id}/locations", content);
                Console.WriteLine("Response Status Code: " + response.StatusCode);
                Console.WriteLine("Response Content: " + response.Content);
                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "There is already a reservation for these dates");
                    await OnGetAsync();
                    return Page();
                }

                TempData["BookingSuccess"] = "Your reservation has been confirmed!";

                await OnGetAsync();

                DateStart = null;
                DateEnd = null;

                return Page();
            }
        }
    }
}