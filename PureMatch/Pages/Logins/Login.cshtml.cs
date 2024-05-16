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
        private DataBaseReader _repo; 
        public LoginModel(DataBaseReader repo)
        {
            _repo = repo;
        }

        [Required(ErrorMessage = "Du skal skrive et brugernavn")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Du skal skrive et kodeord")]
        public string Password { get; set; }


        public void OnGet()
        {
        }

        public IActionResult OnPostLogin(string username, string password)
        {
            if (!ModelState.IsValid)
            {
                return Page(); 
            }
            if (_repo.ReadLogin(username, password) != null)
            {
                User u = _repo.ReadLogin(username, password);
                SessionHelper.Set(User, HttpContext); 
            }
            return RedirectToPage("/Index");
        }

        public IActionResult OnPostLogout()
        {
            User u = null!;
            SessionHelper.Get(u, HttpContext);
            SessionHelper.Clear(u, HttpContext);
            return RedirectToPage("/Index");
        }

        public IActionResult OnPostAbort()
        {
            return RedirectToPage("/Index");
        }
    }
}
