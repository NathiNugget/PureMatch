using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PureLib.Services;
using System.ComponentModel.DataAnnotations;

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
    [RegularExpression()]
    public string Name { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Phonenumber { get; set; }
    public string Email { get; set; }
    public string CardNumber { get; set; }
    public string CVC { get; set; }
    public string CardExpMonth { get; set; }
    public string CardExpYear { get; set; }
    
}
