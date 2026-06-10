using Microsoft.AspNetCore.Mvc.RazorPages;
using RatingApp.Application.Interfaces;
using RatingApp.Domain.Entities;

namespace leaderboard_razor_app_pet_project.Pages;

public class PlayerModel(IPlayerRepository playerRepository) : PageModel
{
   public PlayerEntity Player { get; private set; }
   
   public async void OnGet(Guid playerId)
   {
      PlayerEntity? player = await playerRepository.GetPlayerByIdAsync(playerId);
      
      if (player == null)
         throw new Exception("Player of id " + playerId + " not found");
      
      Player = player;
   }
}