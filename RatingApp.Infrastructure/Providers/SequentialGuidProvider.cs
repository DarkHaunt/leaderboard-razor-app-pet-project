using RatingApp.Application.Interfaces;

namespace RatingApp.Infrastructure.Providers;

public class SequentialGuidProvider : IGuidProvider
{
   public Guid CreateNew() =>
      Guid.CreateVersion7();
}