using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PureLib.Services;
using System.ComponentModel.DataAnnotations;

namespace PureMatch.Pages.pm
{

    [BindProperties]
    public class DeleteConfirmationModel : PageModel
    {
        private readonly IDB _repo;

        public DeleteConfirmationModel(IDB repo)
        {
            _repo = repo;
        }
        public void OnGet(int messageid)
        {
            MessageContent = _repo.GetMessage(messageid).Messagevalue;

            MessageID = messageid;
            RecipientID = _repo.GetMessage(messageid).RecipientID;
        }
        public IActionResult OnPost(bool delete)
        {
            MessageContent = _repo.GetMessage(MessageID).Messagevalue;
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (delete)
            {
                _repo.DeleteMessage(MessageID);
                return RedirectToPage("/pm/PureMatch", new { chatid = RecipientID });
            }
            return RedirectToPage("/pm/PureMatch", new { chatid = RecipientID });


        }
        [Required]
        public string MessageContent { get; set; }
        [Required]
        public int MessageID { get; set; }
        [Required]
        public int RecipientID { get; set; }
    }
}
