using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PureLib.Model;
using PureLib.Services;

namespace PureMatch.Pages.pm
{
    [BindProperties]
    public class PureMatchModel : PageModel
    {
        private readonly DataBaseReader _repo;
        private List<User> _matches;
        
        public PureMatchModel(DataBaseReader repo)
        {
            _repo = repo;

        }
        public void OnGet(int userid, int? chatid)
        {
            Matches = _repo.GetMatches(userid); 
        }
        
        public IActionResult OnPostUserPage(int ownid, int matchid)
        {
            return RedirectToPage("./UserPage", new { userid = ownid, mid = matchid });
            
        }

        public List<User> Matches { get => _matches; set { _matches = value; } }
    }
}
