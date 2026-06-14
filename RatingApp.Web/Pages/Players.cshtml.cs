using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RatingApp.Application.Interfaces;
using RatingApp.Domain.Entities;
using RatingApp.Domain.Specifications.Players;

namespace leaderboard_razor_app_pet_project.Pages;

public class PlayersModel(IPlayerRepository playerRepository) : PageModel
{
   public List<PlayerEntity> FilteredPlayers { get; set; } = new();
   
   public async Task OnGet()
   {
      FilteredPlayers = await playerRepository.GetAllPlayersAsync(ct: HttpContext.RequestAborted);
   }
   
   public async Task<IActionResult> OnPostFind(string nickname)
   {
      var spec = new PlayersWithName(nickname);
      FilteredPlayers = await playerRepository.GetAllPlayersAsync(spec, ct: HttpContext.RequestAborted);
      
      if (FilteredPlayers.Count == 0)
      {
         ViewData["Message"] = "Player not found";
      }
      
      return Page();
   }
   
   public async Task<IActionResult> OnPostReset()
   {
      FilteredPlayers = await playerRepository.GetAllPlayersAsync(ct: HttpContext.RequestAborted);
      return Page();
   }
}