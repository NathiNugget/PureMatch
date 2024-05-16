using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PureLib.Model;
using PureLib.Services;

namespace PureMatch.Pages.Logins
{
    [BindProperties]
    public class MyProfile : PageModel
    {
        private User u;
        public MyProfile(DataBaseReader repo)
        {
            Repo = repo;
        }
        public void OnGet(int userid)
        {
            u = Repo.ReadUser(userid); //TODO: Change to session
            NameNew = u.Name;
            UserNameNew = u.UserName;
            PasswordNew = u.Password;
            EmailNew = u.Email;
            PhoneNumberNew = u.PhoneNumber;
            CardNumberNew = u.CardNumber;
            CVCNew = u.CardCVC;
            CardExpMonthNew = int.Parse(u.CardExpMonth);
            CardExpYearNew = int.Parse(u.CardExpYear);



        }

        public IActionResult OnPostUpdate()
        {
            ModelState.Remove("Days");
            ModelState.Remove("MuscleGroups");
            if (!ModelState.IsValid)
            {
                return Page(); 
            }
            RedirectToPage("./ConfirmChanges", new { userid = u.UserID,
                
            }); 
            




            if (!ModelState.IsValid)
            {
                return Page(); 
            }
            return RedirectToPage("/Index"); 
        }
        
        public User ProfileUser { get; 
            set; }
        
        

        [Required(ErrorMessage = "Du skal skrive dit fulde navn")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Du skal skrive mindst 3 tegn og maks 50 tegn")]
        public string NameNew { get; set; }
        [Required(ErrorMessage = "Du skal skrive et brugernvan")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Du skal skrive mindst 3 tegn og maks 30 tegn")]
        public string UserNameNew { get; set; }
        [Required(ErrorMessage = "Du skal skrive et kodeord")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Du skal skrive mindst 3 tegn og maks 50 tegn")]
        public string PasswordNew { get; set; }
        [Required(ErrorMessage = "Du skal skrive et telefonnummer på 8 tal")]
        [RegularExpression(ValidationRegex.PHONEFILTER, ErrorMessage = "Du skal skrive 8 tal på formen ########")]
        public string PhoneNumberNew { get; set; }
        [Required(ErrorMessage = "Du skal skrive en mail")]
        [RegularExpression(ValidationRegex.MAILFILTER, ErrorMessage = "Du skal skrive på formen navn@domåne.tld")]
        public string EmailNew { get; set; }
        [Required(ErrorMessage = "Du skal skrive et kortnummer på 16 cifre")]
        [RegularExpression(ValidationRegex.CARDNUMBERFILTER, ErrorMessage = "Du skal skrive 16 cifre")]
        public string CardNumberNew { get; set; }
        [Required(ErrorMessage = "Du skal skrive et CVC på 3 cifre")]
        [RegularExpression(ValidationRegex.CARDCVCFILTER, ErrorMessage = "Du skal skrive 3 cifre")]
        public string CVCNew { get; set; }
        [Required(ErrorMessage = "Du skal skrive kortets måned for udlåbsdato, mellem 1 og 12")]
        public int CardExpMonthNew { get; set; }

        [Required(ErrorMessage = "Du skal skrive kortets år for udlåbsdato, 2 cifre tak")]
        public int CardExpYearNew { get; set; }
        public DataBaseReader Repo { get; set; }
    }
}
