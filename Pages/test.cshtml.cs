using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestTest.Models;

namespace TestTest.Pages
{
    public class testModel : PageModel
    {
        
        private readonly TestTest.Models.Db.DatabaseContext _context;

        public testModel(TestTest.Models.Db.DatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LoginViewModel loginData { get; set; } = default!;

        public void OnGet()
        {
            Console.WriteLine("Get");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine("Post");
            var user = _context.Users.FirstOrDefault(u => Convert.ToString(u.Username) == loginData.Username && Convert.ToString(u.Password) == this.loginData.Password);
            if (user == null) return NotFound();

            return RedirectToPage("Index", new { message = "Test pomyœlny" });

        }
    }
}
