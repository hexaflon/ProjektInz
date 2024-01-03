using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using TestTest.Models.Db;

namespace TestTest.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResendEmailConfirmationModel : PageModel
    {
        private readonly UserManager<Osoba> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<Osoba> _signInManager;
        private readonly ILogger<ResendEmailConfirmationModel> _logger;

        public ResendEmailConfirmationModel(UserManager<Osoba> userManager, IEmailSender emailSender, SignInManager<Osoba> signInManager, ILogger<ResendEmailConfirmationModel> logger)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "To pole jest wymagane.")]
            [EmailAddress(ErrorMessage = "WprowadŸ poprawny adres e-mail.")]
            [Display(Name = "Adres e-mail")]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || await _userManager.IsEmailConfirmedAsync(user))
                {
                    return RedirectToPage("/Identity/Account/ResendEmailConfirmationSent");
                }

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                var callbackUrl = Url.Page(
                    "/Identity/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = user.Id, code },
                    protocol: Request.Scheme);

                if (callbackUrl != null)
                {
                    await _emailSender.SendEmailAsync(
                        Input.Email,
                        "PotwierdŸ swój adres e-mail",
                        $"Aby potwierdziæ swój adres e-mail, <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>kliknij tutaj</a>.");
                }
                else
                {
                    return RedirectToPage("/Error");
                }

                return RedirectToPage("/Identity/Account/ResendEmailConfirmationSent");
            }

            return Page();
        }


    }
}
