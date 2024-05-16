using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PureLib.Model;
using PureLib.Services;

namespace PureMatch.Pages.pm
{
    [BindProperties]
    public class PureMatchModel : PageModel
    {
        private readonly DataBaseLink _repo;
         

        public PureMatchModel(DataBaseLink repo)
        {
            _repo = repo;

        }
        public void OnGet(int? chatid)
        {
            User u = null!;
            u = SessionHelper.Get<User>(u, HttpContext);
            Matches = _repo.GetMatches(u.UserID);
            Chats = _repo.GetChatUsers(u.UserID);
            if (chatid != 0)
            {
                Messages = _repo.ReadMessages(u.UserID, (int)chatid!);

            }
             
        }

        public IActionResult OnPostUserPage(int ownid, int matchid)
        {
            return RedirectToPage("./UserPage", new { userid = ownid, mid = matchid });

        }

        public IActionResult OnPostChat(int ownid, int chatid)
        {
            return RedirectToPage("./", new { chatid = chatid });
        }

        public List<User> Matches { get; set; }
        public List<User> Chats { get; set; }
        public List<Message> Messages { get; set; }
        
    }
}
