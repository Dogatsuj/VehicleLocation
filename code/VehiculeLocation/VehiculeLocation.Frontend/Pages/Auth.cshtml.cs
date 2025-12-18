using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using System.Text.Json;

namespace VehiculeLocation.Frontend.Pages
{

    public class AuthResponse
    {
        public required string Token { get; set; }
        public required string Username { get; set; }
        public bool IsAdmin { get; set; }
    }


    public class AuthModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public string? LoginName { get; set; }

        [BindProperty]
        public string? LoginPassword { get; set; }

        [BindProperty]
        public string? SignupName { get; set; }

        [BindProperty]
        public string? SignupPassword { get; set; }

        [BindProperty]
        public string? SignupConfirmPassword { get; set; }

        public string ActiveTab { get; private set; } = "login";

        public string? SignupMessage { get; private set; }

        public string? SignupError { get; private set; }

        public void OnGet()
        {
        }

        public IActionResult OnPostLogin()
        {
            ActiveTab = "login";
            //not implemented
            return Page();
        }

        public async Task<IActionResult> OnPostSignup()
        {
            ActiveTab = "signup";

            if (string.IsNullOrWhiteSpace(SignupName) ||
                string.IsNullOrWhiteSpace(SignupPassword) ||
                string.IsNullOrWhiteSpace(SignupConfirmPassword))
            {
                SignupError = "Merci de remplir tous les champs.";
                return Page();
            }

            if (!string.Equals(SignupPassword, SignupConfirmPassword, StringComparison.Ordinal))
            {
                SignupError = "Les mots de passe ne correspondent pas.";
                return Page();
            }

            var client = _httpClientFactory.CreateClient("ApiBackend");
            var payload = new
            {
                username = SignupName,
                password = SignupPassword   
            };

            try
            {
                var response = await client.PostAsJsonAsync("api/User/register", payload);
                if (response.IsSuccessStatusCode)
                {
                    SignupMessage = "Inscription réussie";

                    var json = await response.Content.ReadAsStringAsync();

                    var authResponse = JsonSerializer.Deserialize<AuthResponse>(
                        json,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );

                    CookieOptions options = new CookieOptions
                    {
                        Expires = DateTimeOffset.Now.AddHours(1), 
                        SameSite = SameSiteMode.Strict
                    };

                    // Add cookie
                    Response.Cookies.Append("token", authResponse.Token, options);
                    Response.Cookies.Append("username", authResponse.Username, options);


                }
                else
                {
                    var details = await response.Content.ReadAsStringAsync();
                    SignupError = $"Erreur API {(int)response.StatusCode}: {details}";
                }
            }
            catch (Exception ex)
            {
                SignupError = $"Impossible d'appeler l'API: {ex.Message}";
            }

            return Page();
        }
    }
}
