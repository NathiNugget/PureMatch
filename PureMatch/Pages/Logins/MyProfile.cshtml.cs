using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PureLib.Model;
using PureLib.Services;

namespace PureMatch.Pages.Logins
{
    [BindProperties]
    public class MyProfileModel : PageModel
    {
        public MyProfileModel(DataBaseReader repo)
        {
            Repo = repo;    
        }
        public void OnGet()
        {
            
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page(); 
            }
            return Page(); 
        }
        
        
        
        
        public User user { get; set; }
        [Required(ErrorMessage = "Du skal skrive dit fulde navn")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Du skal skrive mindst 3 tegn og maks 50 tegn")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Du skal skrive et brugernavn")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Du skal skrive mindst 3 tegn og maks 30 tegn")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Du skal skrive et kodeord")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Du skal skrive mindst 3 tegn og maks 50 tegn")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Du skal skrive et telefonnummer på 8 tal")]
        [RegularExpression(ValidationRegex.PHONEFILTER, ErrorMessage = "Du skal skrive 8 tal p� formen ########")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Du skal skrive en mail")]     
        [RegularExpression(ValidationRegex.MAILFILTER, ErrorMessage = "Du skal skrive p� formen navn@dom�ne.tld")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Du skal skrive et kortnummer p� 16 cifre")]
        [RegularExpression(ValidationRegex.CARDNUMBERFILTER, ErrorMessage = "Du skal skrive 16 cifre")]
        public string CardNumber { get; set; }
        [Required(ErrorMessage = "Du skal skrive et CVC på 3 cifre")]
        [RegularExpression(ValidationRegex.CARDCVCFILTER, ErrorMessage = "Du skal skrive 3 cifre")]
        public string CardCVC { get; set; }
        [Required(ErrorMessage = "Du skal skrive kortets m�ned for udl�bsdato, mellem 1 og 12")]
        
        public string CardExpireMonth { get; set; }
        [Required(ErrorMessage = "Du skal skrive kortets år for udl�bsdato, mellem 1 og 12")]
        public string CardExpireYear { get; set; }
        public string Subscription { get; set; }
        public DataBaseReader Repo { get; set; }
    }
}
