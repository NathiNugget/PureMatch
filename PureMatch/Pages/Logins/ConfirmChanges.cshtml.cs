using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PureLib.Model;
using PureLib.Services;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PureMatch.Pages.Logins
{
    [BindProperties]
    public class ConfirmChangesModel : PageModel
    {
        
        private DataBaseLink _repo;

        public ConfirmChangesModel(DataBaseLink repo)
        {
            Repo = repo;
        }

        public void OnGet(string name, string username, string password, string email, string phonenumber, string cardnumber, string cardexpmonth, string cardexpyear, string cardcvc)
        {
            NameNew = name;
            UsernameNew = username;
            PasswordNew = password;
            EmailNew = email;
            PhoneNumberNew = phonenumber;
            CardNumberNew = cardnumber;
            CardExpMonthNew = cardexpmonth;
            CardCVCNew = cardcvc;
            CardExpYearNew = cardexpyear;
            CardCVCNew = cardcvc;
        }

        [Required()]
        [BindProperty]
        public string NameNew { get; set; }
        [Required()]
        public string UsernameNew { get; set; }
        [Required()]
        public string PasswordNew { get; set; }
        [Required()]
        public string EmailNew { get; set; }
        [Required()]
        public string PhoneNumberNew { get; set; }
        [Required()]
        public string CardNumberNew { get; set; }
        [Required()]
        public string CardExpMonthNew { get; set; }
        [Required()]
        public string CardExpYearNew { get; set; }
        [Required()]
        public string CardCVCNew { get; set; }
        public DataBaseLink Repo { get => _repo; set => _repo = value; }

        public IActionResult OnPostAbort()
        {
            return RedirectToPage("MyProfile"); 
        }

        

        public IActionResult OnPostConfirmAction(bool save)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (save)
            {
                User u = null;
                u = SessionHelper.Get<User>(u, HttpContext);
                u.Name = NameNew;
                u.UserName = UsernameNew;
                u.Password = PasswordNew;
                u.Email = EmailNew;
                u.PhoneNumber = PhoneNumberNew;
                u.CardNumber = CardNumberNew;
                u.CardCVC = CardCVCNew;
                int expmonth = int.Parse(CardExpMonthNew);
                int expyear = int.Parse(CardExpYearNew);
                u.CardExpMonth = expmonth.ToString("0#");
                u.CardExpYear = expyear.ToString("0#");
                Repo.UpdateUser(u);
                SessionHelper.Set<User>(u, HttpContext);
                return RedirectToPage("/checkmark/ProfileChanged");
            }
            return RedirectToPage("MyProfile");
            

        }
    }
}
