using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PureLib.Model;
using PureLib.Services;
using System.ComponentModel.DataAnnotations;

namespace PureMatch.Pages.pm;
[BindProperties]
public class CreateAccount : PageModel
{
    private IDB _repo;

    public CreateAccount(IDB repo)
    {
        Repo = repo;
    }
    public IActionResult OnGet(int subumber)
    {
        SubNumber = subumber;
        User u = null!; 
        try
        {
            u = SessionHelper.Get(u, HttpContext); 
        }
        catch
        {
            return Page(); 
        }
        return RedirectToPage("/Index");
        
    }

    public IActionResult OnPost()
    {
        if (!_repo.IsAvailableUserName(UserName))
        {
            return Page();
        }
        ModelState.Remove("UserNameTaken");
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Repo.AddUser(new User(1, Name, UserName, Password, PhoneNumber, Email, CardNumber, CVC, CardExpMonth.ToString("0#"), CardExpYear.ToString("##"), (SubscriptionEnum)SubNumber, LevelsEnum.Begynder));
        return RedirectToPage("/checkmark/ProfileCreated");
    }

    
    private IDB Repo {  get { return _repo; } set { _repo = value; } }

    public int SubNumber { get; set; }
    [Required(ErrorMessage = "Du skal skrive dit fulde navn")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Du skal skrive mindst 3 tegn og maks 50 tegn")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Du skal skrive et brugernvan")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Du skal skrive mindst 3 tegn og maks 30 tegn")]
    public string UserName { get; set; }
    [Required(ErrorMessage = "Brugernavn allerede taget. Forsøg med et andet")]
    public string UserNameTaken { get; set; }
    [Required(ErrorMessage = "Du skal skrive et kodeord")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Du skal skrive mindst 3 tegn og maks 50 tegn")]
    public string Password { get; set; }
    [Required(ErrorMessage = "Du skal skrive et telefonnummer på 8 tal")]
    [RegularExpression(ValidationRegex.PHONEFILTER, ErrorMessage = "Du skal skrive 8 tal på formen ########")]
    public string PhoneNumber { get; set; }
    [Required(ErrorMessage = "Du skal skrive en mail")]
    [RegularExpression(ValidationRegex.MAILFILTER, ErrorMessage = "Du skal skrive på formen navn@domæne.tld")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Du skal skrive et kortnummer på 16 cifre")]
    [RegularExpression(ValidationRegex.CARDNUMBERFILTER, ErrorMessage = "Du skal skrive 16 cifre")]
    public string CardNumber { get; set; }
    [Required(ErrorMessage = "Du skal skrive et CVC på 3 cifre")]
    [RegularExpression(ValidationRegex.CARDCVCFILTER, ErrorMessage = "Du skal skrive 3 cifre")]
    public string CVC { get; set; }
    [Required(ErrorMessage = "Du skal skrive kortets måned for udløbsdato, mellem 1 og 12")]
    public int CardExpMonth { get; set; }

    [Required(ErrorMessage = "Du skal skrive kortets år for udløbsdato, 2 cifre tak")]
    public int CardExpYear { get; set; }
    
}
