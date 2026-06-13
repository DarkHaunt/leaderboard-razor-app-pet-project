using Microsoft.AspNetCore.Mvc.RazorPages;
using RatingApp.Application.Interfaces;
using RatingApp.Domain.Entities;

namespace leaderboard_razor_app_pet_project.Pages;

public class PlayersModel(IPlayerRepository playerRepository) : PageModel
{
   public List<PlayerEntity> Players { get; set; } = new();
   
   public async Task OnGet()
   {
      Players = await playerRepository.GetAllPlayersAsync(ct: HttpContext.RequestAborted);
   }
}