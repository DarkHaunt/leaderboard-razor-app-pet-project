using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RatingApp.Application.UseCases;

namespace leaderboard_razor_app_pet_project.Pages;

[IgnoreAntiforgeryToken]
public class AdminPage(PlayerCreateUseCase playerCreateUseCase, PlayerDeleteUseCase playerDeleteUseCase) : PageModel
{
   public async Task<IActionResult> OnPostCreateAsync(string nickname, int? rating)
   {
      var result = await playerCreateUseCase.CreatePlayerAsync(nickname, rating, HttpContext.RequestAborted);

      if (result.IsFailure)
         ModelState.AddModelError("", $"Failed to create player {nickname}, try again");
      
      return Page();
   }
   
   public async Task<IActionResult> OnPostDeleteAsync(string nickname)
   {
      var result = await playerDeleteUseCase.DeleteAllPlayersWithNicknameAsync(nickname, HttpContext.RequestAborted);
      
      if (result.IsFailure)
         ModelState.AddModelError("", $"Failed to delete players with nickname {nickname}, try again");
      
      return Page();
    }
}