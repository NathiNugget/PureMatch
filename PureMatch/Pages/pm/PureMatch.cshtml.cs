using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.FileSystemGlobbing;
using PureLib.Model;
using PureLib.Services;
using System.ComponentModel.DataAnnotations;

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
            if (chatid != 0 && chatid != null)
            {
                Messages = _repo.ReadMessages(u.UserID, (int)chatid!);

            }
             
        }

        public IActionResult OnPostMessage(int chatid)
        {
            ModelState.Remove("MessageValue");
            if (!ModelState.IsValid)
            {
                return Page();
            }
            User u = null!;
            u = SessionHelper.Get<User>(u, HttpContext);
            Matches = _repo.GetMatches(u.UserID);
            Chats = _repo.GetChatUsers(u.UserID);
            if (chatid != 0)
            {
                Messages = _repo.ReadMessages(u.UserID, (int)chatid!);

            }
            return Page(); 
            
        }

        public IActionResult OnPostUserPage(int ownid, int matchid)
        {
            return RedirectToPage("./UserPage", new { userid = ownid, mid = matchid });

        }

        public IActionResult OnPostSendMessage(int ownid, int chatid)
        {
            
            User u = null!;
            u = SessionHelper.Get<User>(u, HttpContext);
            Matches = _repo.GetMatches(u.UserID);
            Chats = _repo.GetChatUsers(u.UserID);
            Messages = _repo.ReadMessages(u.UserID, chatid);
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./PureMatch", new { chatid = chatid });
            }

            _repo.SendMessage(ownid, chatid, MessageValue);
            return RedirectToPage("./PureMatch");
        }



        public List<User> Matches { get; set; }
        public List<User> Chats { get; set; }
        public List<Message> Messages { get; set; }
        [Required (ErrorMessage = "Du ville da gerne sende en besked, ikke? :)")]
        public string MessageValue { get; set; }
        
    }
}
