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
        for (int i = 0; i < 7; i++)
        {
            MuscleGroups.Add(false);
        }

        Levels = Enum.GetValues(typeof(LevelsEnum)).Cast<LevelsEnum>().ToList();
        List<bool> Levelslist = new List<bool>();
        for (int i = 0; i < 3; i++)
        {
            Levelslist.Add(false);
        }

        Days = Enum.GetValues(typeof(DaysEnum)).Cast<DaysEnum>().ToList();
        List<bool> Dayslist = new List<bool>();
        for (int i = 0; i < 7; i++)
        {
            Levelslist.Add(false);
        }
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
    [BindProperty]
    public List<bool> MuscleGroups { get; set; }
    public List<bool> LevelsList { get; set; }
    public List<bool> DaysList { get; set; }


}