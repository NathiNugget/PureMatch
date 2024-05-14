using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PureLib.Model;
using PureLib.Services;
using System.ComponentModel.DataAnnotations;

namespace PureMatch.Pages.pm;
[BindProperties]
public class CreateAccount : PageModel
{
    private DataBaseReader _repo;
    

    
    
    public void CreateAccountModel(DataBaseReader repo)
    {
        _repo = repo; 
    }
    public void OnGet(int subumber)
    {
        SubNumber = subumber; 
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page(); 
        }
        return RedirectToPage("/Index");
    }
    
    public int SubNumber { get; set; }
    [Required(ErrorMessage = "Du skal skrive dit fulde navn")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Du skal skrive mindst 3 tegn og maks 50 tegn")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Du skal skrive et brugernvan")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Du skal skrive mindst 3 tegn og maks 30 tegn")]
    public string UserName { get; set; }
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
    [Required(ErrorMessage = "Du skal skrive kortets måned for udløbsdato, 2 cifre tak")]
    [RegularExpression(ValidationRegex.CARDEXPMONTHFILTER, ErrorMessage = "Du skal skrive 2 cifre")]
    public string CardExpMonth { get; set; }
    [Required(ErrorMessage = "Du skal skrive kortets år for udløbsdato, 2 cifre tak")]
    [RegularExpression(ValidationRegex.CARDEXPYEARFILTER, ErrorMessage = "Du skal skrive 2 cifre")]
    public string CardExpYear { get; set; }
    
}
