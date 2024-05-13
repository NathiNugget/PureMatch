using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PureMatch.Pages.pm;

public class SelectSubscription : PageModel
{
    [BindProperty]
    [Required(ErrorMessage =  "Der skal vælges en medlemskab")]
    
    public int SelectedSubscription { get; set; }
    

        public void OnGet()
        {
            // Ingen specifik handling krævet, når siden indlæses
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Hvis validering mislykkes, returneres den samme side med fejlmeddelelser
                return Page();
            }
            

            // Behandling af valgt abonnement (f.eks. gemt i database )
            // Her kan brugeren  videresendes  til næste side, "CreateAccount"
            return RedirectToPage("./CreateAccount");
        }
    }
  



