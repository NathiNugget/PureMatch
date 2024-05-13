using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PureLib.Services;

namespace PureMatch.Pages.pm;
[BindProperties]
public class CreateAccount : PageModel
{
    private DataBaseReader _repo;
    

    
    
    public void CreateAccountModel(DataBaseReader repo)
    {
        _repo = repo; 
    }
    public void OnGet(int subumber)
    {
        SubNumber = subumber; 
    }
    
    public int SubNumber { get; set; }
    public string _Name { get; set; }
    public string _UserName { get; set; }
    public string _password { get; set; }
    public string _phonenumber { get; set; }
    public string _Email { get; set; }
    public string _cardnumber { get; set; }
    public string _cvc { get; set; }
    public string _cardexpmonth { get; set; }
    public string _cardexpyear { get; set; }
    
}
