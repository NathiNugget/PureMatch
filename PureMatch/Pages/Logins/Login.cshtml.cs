using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PureLib.Model;
using PureLib.Services;
using System.ComponentModel.DataAnnotations;

namespace PureMatch.Pages.Logins
{
    [BindProperties]
    public class LoginModel : PageModel
    {
        private readonly IDB _repo;
        public LoginModel(IDB repo)
        {
            _repo = repo;
        }

        [Required(ErrorMessage = "Du skal skrive et brugernavn")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Du skal skrive et kodeord")]
        public string Password { get; set; }
        public string LoginFailed { get; set; }


        public void OnGet()
        {
        }

        public IActionResult OnPostLogin(string username, string password)
        {
            ModelState.Remove("LoginFailed");
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (_repo.ReadLogin(username, password) != null)
            {
                User u = _repo.ReadLogin(username, password)!;
                SessionHelper.Set(u, HttpContext);
            }
            else
            {
                LoginFailed = "Brugernavn eller kodeord forkert - prøv igen";
                return Page();
            }
            return RedirectToPage("/Index");

        }

        public IActionResult OnPostLogout()
        {
            User u = null!;
            u = SessionHelper.Get(u, HttpContext);
            SessionHelper.Clear(u, HttpContext);
            return RedirectToPage("/Index");
        }

        public IActionResult OnPostAbort()
        {
            return RedirectToPage("/Index");
        }
    }
}
