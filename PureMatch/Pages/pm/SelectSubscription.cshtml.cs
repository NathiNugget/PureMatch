using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PureLib.Model;

namespace PureMatch.Pages.pm;

public class SelectSubscription : PageModel
{
    
    

        public IActionResult OnGet()
        {
        User u = null!; 
        try
        {
            u = SessionHelper.Get<User>(u, HttpContext); 
        }
        catch (Exception ex)
        {
            return Page(); 
        }
        return RedirectToPage("/Index");
            
        }

        public IActionResult OnPost(int number)
        {
            
            if (!ModelState.IsValid)
            {
                // Hvis validering mislykkes, returneres den samme side med fejlmeddelelser
                return Page();
            }
            

            // Behandling af valgt abonnement (f.eks. gemt i database )
            // Her kan brugeren  videresendes  til næste side, "CreateAccount"
            return RedirectToPage("./CreateAccount", new { subnumber = number});
        }
    }
  



