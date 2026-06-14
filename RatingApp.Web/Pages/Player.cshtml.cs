using Microsoft.AspNetCore.Mvc.RazorPages;
using RatingApp.Application.Interfaces;
using RatingApp.Domain.Entities;

namespace leaderboard_razor_app_pet_project.Pages;

public class PlayerModel(IPlayerRepository playerRepository) : PageModel
{
   public PlayerEntity Player { get; private set; }
   
   public async Task OnGetAsync(Guid playerId)
   {
      PlayerEntity? player = await playerRepository.GetPlayerByIdAsync(playerId, ct: HttpContext.RequestAborted);
      
      if (player == null)
         ModelState.AddModelError("", $"Player of id {playerId} not found");
      
      Player = player ?? throw new Exception("Player of id " + playerId + " not found");
   }
}