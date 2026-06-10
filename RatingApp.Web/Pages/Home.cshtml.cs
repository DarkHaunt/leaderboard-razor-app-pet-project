using Microsoft.AspNetCore.Mvc.RazorPages;
using RatingApp.Application.Interfaces;
using RatingApp.Domain.Entities;

namespace leaderboard_razor_app_pet_project.Pages;

public class HomeModel(ILeagueRepository leagueRepository) : PageModel
{
   public List<LeagueEntity> Leagues { get; set; } = new();
   
   public async void OnGet()
   {
      Leagues = await leagueRepository.GetAllLLeaguesAsync();
   }
}