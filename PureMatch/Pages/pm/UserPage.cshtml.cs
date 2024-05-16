using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PureLib.Model;
using PureLib.Services;
using System.ComponentModel.DataAnnotations;

namespace PureMatch.Pages.pm
{
    [BindProperties]
    public class UserPageModel : PageModel
    {
        private readonly DataBaseLink _repo;
        private User? _matchuser; 

        public UserPageModel(DataBaseLink repo) { 
            _repo = repo; 
        }
       
        public User MatchUser { get { return _matchuser!; } set { _matchuser = value; } }
        [Required (ErrorMessage = "Du skal skrive en besked!")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Besked skal være mindst 2 tegn lang")]
        public string Message { get; set; }

        public void OnGet(int mid)
        {
            _matchuser = _repo.ReadUser(mid);
            _matchuser.Days = _repo.ReadDays(mid);
            _matchuser.MuscleGroups = _repo.ReadMuscleGroups(mid); 
        }



        public IActionResult OnPost(int ownid, int matchid)
        {
            MatchUser = _repo.ReadUser(matchid);
            MatchUser.Days = _repo.ReadDays(matchid);
            MatchUser.MuscleGroups = _repo.ReadMuscleGroups(matchid);
            ModelState.Remove("Days");
            ModelState.Remove("MuscleGroups"); 
            if (!ModelState.IsValid){
                
                return Page(); 
            }
             
            _repo.SendMessage(ownid, matchid, Message);
            return RedirectToPage("./PureMatch"); 
        }


    }
}
