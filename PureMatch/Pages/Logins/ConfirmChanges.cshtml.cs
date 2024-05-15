using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PureLib.Model;
using PureLib.Services;
using System.Xml.Linq;

namespace PureMatch.Pages.Logins
{
    public class ConfirmChangesModel : PageModel
    {
        private int _userid;
        private string _name;
        private string _username;
        private string _password;
        private string _email;
        private string _phonenumber;
        private string _cardnumber;
        private string _cardexpmonth;
        private string _cardexpyear;
        private string _cardcvc;
        private DataBaseReader _repo; 

        public ConfirmChangesModel(DataBaseReader repo)
        {
            _repo = repo;
        }

        public void OnGet(int userid, string name, string username, string password, string email, string phonenumber, string cardnumber, string cardexpmonth, string cardexpyear, string cardcvc)
        {
            _userid = userid;
            _name = name;
            _username = username;
            _password = password;
            _email = email;
            _phonenumber = phonenumber;
            _cardnumber = cardnumber;
            _cardexpmonth = cardexpmonth;
            _cardcvc = cardcvc;
            _cardexpyear = cardexpyear;
            _cardcvc = cardcvc;
        }

        public IActionResult OnPostAbort()
        {
            return RedirectToPage("MyProfile", new { userid = _userid }); 
        }

        public IActionResult OnPostConfirm()
        {
            User u = _repo.ReadUser(_userid); //TODO: Change to session
            u.Name = _name;
            u.UserName = _username;
            u.Password = _password;
            u.Email = _email;
            u.PhoneNumber = _phonenumber;
            u.CardNumber = _cardnumber;
            u.CardCVC = _cardcvc;
            u.CardExpMonth = _cardexpmonth;
            u.CardExpYear = _cardexpyear;
            _repo.UpdateUser(u);
            return RedirectToPage("/Index");
        }
    }
}
