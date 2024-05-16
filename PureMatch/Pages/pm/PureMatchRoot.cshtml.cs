using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PureLib.Model;
using PureLib.Services;
using System.Text.RegularExpressions;

namespace PureMatch.Pages.pm;

[BindProperties]
public class PureMatchRoot : PageModel
{
    public PureMatchRoot(DataBaseReader repo)
    {
        Repo = repo;
    }
    public void OnGet()
    {
        
        Groups = Enum.GetValues(typeof(MuscleGroupEnum)).Cast<MuscleGroupEnum>().ToList();
        MuscleGroups = new List<bool>();
        for (int i = 0; i < Groups.Count; i++)
        {
            MuscleGroups.Add(false);
        }

        Levels = Enum.GetValues(typeof(LevelsEnum)).Cast<LevelsEnum>().ToList();


        Days = Enum.GetValues(typeof(DaysEnum)).Cast<DaysEnum>().ToList();
        List<bool> DaysList = new List<bool>();
        for (int i = 0; i < Days.Count; i++)
        {
            DaysList.Add(false);
        }

    }

    public IActionResult OnPost()
    {
        

        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        return Page();
    }


    public IActionResult OnPostResetMatch()
    {
        User u = null;
        u = SessionHelper.Get<User>(u, HttpContext); 
        int userid = u.UserID;
        if (!ModelState.IsValid)
        {
            return Page(); 
        }
        Repo.SetMatching(userid, null, null, 0);
        return RedirectToPage("/Index");
    }

    public IActionResult OnPostChangeCriteria()
    {
        if (!ModelState.IsValid)
        {
            return Page(); 
        }
        User u = null; 
        u = SessionHelper.Get<User>(u, HttpContext);
        u.Level = SelectedLevel;
        u.MuscleGroups = Groups;
        u.Days = Days;
        SessionHelper.Set<User>(u, HttpContext);
        int userid = u.UserID;
        List<MuscleGroupEnum> groups = new List<MuscleGroupEnum>();
        for (int i = 0; i < MuscleGroups.Count; i++)
        {
            if (MuscleGroups[i])
            {
                groups.Add((MuscleGroupEnum)i);
            }
        }

        List<DaysEnum> days = new List<DaysEnum>();
        for (int i = 0; i < DaysList.Count; i++)
        {
            if (DaysList[i])
            {
                days.Add((DaysEnum)i);
            }
        }

        Repo.SetMatching(userid, groups, days, SelectedLevel); 
        return RedirectToPage("/Index");
    }

    public List<MuscleGroupEnum> Groups { get; set; }
    public List<LevelsEnum> Levels { get; set; }
    public List<DaysEnum> Days { get; set; }


    public DataBaseReader Repo { get; set; }
    
    public List<bool> MuscleGroups { get; set; }
    public int SelectedLevel { get; set; }
    public List<bool> DaysList { get; set; }


}