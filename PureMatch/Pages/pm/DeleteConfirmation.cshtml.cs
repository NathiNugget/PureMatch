using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PureLib.Services;

namespace PureMatch.Pages.pm
{
    [BindProperties]
    public class DeleteConfirmationModel : PageModel
    {
        private DataBaseLink _repo;
        private int _recipient; 

        public DeleteConfirmationModel(DataBaseLink repo)
        {
            _repo=repo;
        }
        public void OnGet(int messageid)
        {
            MessageContent = _repo.GetMessage(messageid).Messagevalue;
            _recipient = _repo.GetMessage(messageid).RecipientID;
            MessageID = messageid;
        }
        public IActionResult OnPost(bool delete)
        {
            if (!ModelState.IsValid) { 
                return Page();
            }
            if (delete)
            {
                _repo.DeleteMessage(MessageID);
                return RedirectToPage("/pm/PureMatch", new {chatid = _recipient});
            }
            return Page();
            

        }

        public string MessageContent { get; set; }
        public int MessageID {  get; set; }
    }
}
