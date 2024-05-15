using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PureLib.Model;
using PureLib.Services;

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


    public void OnPostReset(int userid)
    {
        Repo.SetMatching(userid, null, null, 0);
    }

    public IActionResult OnPostChangeCriteria(int someval)
    {
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