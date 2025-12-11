using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using System.Text.Json;

namespace VehiculeLocation.Frontend.Pages
{
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

        public LogInResponse loginResponse { get; private set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostLoginAsync()
        {
            ActiveTab = "login";

            if (!string.IsNullOrWhiteSpace(LoginName) &&
                !string.IsNullOrWhiteSpace(LoginPassword))
            {
                var payload = new
                {
                    username = LoginName,
                    password = LoginPassword
                };

                var client = _httpClientFactory.CreateClient("ApiBackend");

                var response = await client.PostAsJsonAsync("api/User/login", payload);
                if (response.IsSuccessStatusCode) { 

                    var content = await response.Content.ReadAsStringAsync();
                    var logInResponse = JsonSerializer.Deserialize<LogInResponse>(content);

                    if (logInResponse != null)
                    {
                        Response.Cookies.Append("AuthToken", logInResponse.token, new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Strict,
                            Expires = DateTimeOffset.UtcNow.AddHours(1)
                        });

                        return RedirectToPage("/Index");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "incorrect username or password");
                }
            }

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
                Username = SignupName,
                Password = SignupPassword
            };

            try
            {
                var response = await client.PostAsJsonAsync("api/User", payload);
                if (response.IsSuccessStatusCode)
                {
                    SignupMessage = "Inscription envoyée à l'API (mot de passe en clair pour l'instant).";
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

    public class LogInResponse
    {
        public string token { get; set; }
        public string username { get; set; }
        public bool isAdmin { get; set; }
    }
}
