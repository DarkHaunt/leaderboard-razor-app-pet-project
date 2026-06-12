using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RatingApp.Application.UseCases;

namespace leaderboard_razor_app_pet_project.Pages;

[IgnoreAntiforgeryToken]
public class AdminPage(PlayerAddUseCase playerAddUseCase) : PageModel
{
   public async Task<IActionResult> OnPostAsync(string nickname, int? rating)
   {
      await playerAddUseCase.CreatePlayerAsync(nickname, rating);
      return RedirectToPage("Index");
   }
}