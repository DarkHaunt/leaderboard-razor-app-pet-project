using Microsoft.AspNetCore.Mvc.RazorPages;
using RatingApp.Application.Interfaces;
using RatingApp.Domain.Entities;

namespace leaderboard_razor_app_pet_project.Pages;

public class LeagueModel(ILeagueRepository leagueRepository) : PageModel
{
   public LeagueEntity League { get; private set; }
   
   public async Task OnGetAsync(Guid leagueId)
   {
      LeagueEntity? league = await leagueRepository.GetLeagueByIdAsync(leagueId, ct: HttpContext.RequestAborted);
      
      if (league == null)
         ModelState.AddModelError("", $"League of id {leagueId} not found");
      
      League = league ?? throw new Exception("League of id " + leagueId + " not found");
   }
}