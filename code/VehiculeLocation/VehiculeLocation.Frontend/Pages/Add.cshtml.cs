using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VehiculeLocation.Frontend.Models;

namespace VehiculeLocation.Frontend.Pages
{
    public class AddModel : PageModel
    {
        public Vehicle Vehicle { get; set; } = new Vehicle();

        public void OnGet()
        {
        }
    }
}
