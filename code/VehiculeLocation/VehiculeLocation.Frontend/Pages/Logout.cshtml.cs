using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class LogoutModel : PageModel
{
    public IActionResult OnPost()
    {
        // Delete the cookies
        Response.Cookies.Delete("token");
        Response.Cookies.Delete("username");

        // Redirect to home page after logout
        return RedirectToPage("/Index");
    }
}
