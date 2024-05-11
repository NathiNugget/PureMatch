using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PureLib.Model;
using PureLib.Services;
using System.ComponentModel.DataAnnotations;

namespace PureMatch.Pages.pm
{
    
    public class UserPageModel : PageModel
    {
        private readonly DataBaseReader _repo;
        private User _ouser;
        private User _matchuser; 

        public UserPageModel(DataBaseReader repo) { 
            _repo = repo;
        }
        [BindProperty]
        public User OUser { get { return _ouser; } set { _ouser = value;} }
        [BindProperty]
        public User MatchUser { get { return _matchuser; } set { _matchuser = value; } }
        [BindProperty]
        [Required (ErrorMessage = "Du skal skrive en besked!")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Besked skal være mindst 2 tegn lang")]
        public string Message { get; set; }

        public void OnGet(int userid ,int mid)
        {
            _ouser = _repo.ReadUser(2); 
            _matchuser = _repo.ReadUser(mid);
            _matchuser.Days = _repo.ReadDays(mid);
            _matchuser.MuscleGroups = _repo.ReadMuscleGroups(mid); 
        }



        public IActionResult OnPost(int ownid, int matchid)
        {
            OUser = _repo.ReadUser(ownid);
            OUser.Days = _repo.ReadDays(ownid);
            OUser.MuscleGroups = _repo.ReadMuscleGroups(ownid); 
            MatchUser = _repo.ReadUser(matchid);
            MatchUser.Days = _repo.ReadDays(matchid);
            MatchUser.MuscleGroups = _repo.ReadMuscleGroups(matchid);
            ModelState.Remove("Days");
            ModelState.Remove("MuscleGroups"); 
            if (!ModelState.IsValid){
                
                return Page(); 
            }
             
            _repo.SendMessage(ownid, matchid, Message);
            return RedirectToPage("./PureMatch", new { userid = ownid}); 
        }


    }
}
