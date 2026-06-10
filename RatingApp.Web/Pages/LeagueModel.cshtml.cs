using Microsoft.AspNetCore.Mvc.RazorPages;
using RatingApp.Application.Interfaces;
using RatingApp.Domain.Entities;

namespace leaderboard_razor_app_pet_project.Pages;

public class LeagueModel(ILeagueRepository leagueRepository) : PageModel
{
   public LeagueEntity League { get; private set; }
   
   public async void OnGet(Guid leagueId)
   {
      LeagueEntity? league = await leagueRepository.GetLeagueByIdAsync(leagueId);

      if (league == null)
         throw new Exception("League of id " + leagueId + " not found");
         
      League = league;
   }
}