using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RatingApp.Application.UseCases;

namespace leaderboard_razor_app_pet_project.Pages;

[IgnoreAntiforgeryToken]
public class AdminPage(PlayerCreateUseCase playerCreateUseCase, PlayerDeleteUseCase playerDeleteUseCase) : PageModel
{
   public async Task<IActionResult> OnPostCreateAsync(string nickname, int? rating)
   {
      await playerCreateUseCase.CreatePlayerAsync(nickname, rating, HttpContext.RequestAborted);
      return Page();
   }
   
   public async Task<IActionResult> OnPostDeleteAsync(string nickname)
   {
      await playerDeleteUseCase.DeleteAllPlayersWithNicknameAsync(nickname, HttpContext.RequestAborted);
      return Page();
    }
}