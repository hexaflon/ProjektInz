using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestTest.Models.Db;

namespace TestTest.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResendEmailConfirmationSentModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
