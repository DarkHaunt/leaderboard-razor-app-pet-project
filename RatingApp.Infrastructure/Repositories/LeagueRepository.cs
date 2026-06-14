using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RatingApp.Application.Interfaces;
using RatingApp.Domain.Entities;
using RatingApp.Domain.Specifications;
using RatingApp.Infrastructure.Persistence;

namespace RatingApp.Infrastructure.Repositories;

public class LeagueRepository(RatingAppDbContext context) : ILeagueRepository
{
   public async Task<List<LeagueEntity>> GetAllLeaguesAsync(Specification<LeagueEntity>? specification = null,
      Expression<Func<LeagueEntity, object>>? orderBy = null, CancellationToken ct = default)
   {
      var q = context.Leagues.AsNoTracking();

      if (specification is not null)
         q = q.Where(specification.ToExpression());

      if (orderBy is not null)
         q = q.OrderBy(orderBy);

      return await q
         .Include(l => l.Players)
         .ToListAsync(ct);
   }

   public async Task<LeagueEntity?> GetLeagueByIdAsync(Guid id, CancellationToken ct = default)
   {
      return await context.Leagues.AsNoTracking()
         .Include(l => l.Players)
         .FirstOrDefaultAsync(l => l.Id == id, ct);
   }

   public async Task AddLeagueAsync(Guid id, string name, string description, uint requiredRating, CancellationToken ct = default)
   {
      var league = new LeagueEntity
      {
         Id = id,
         Description = description,
         Name = name,
         RequiredRating = requiredRating
      };

      await context.Leagues.AddAsync(league, ct);
   }

   public async Task UpdateLeagueAsync(Guid id, string name, string description, uint requiredRating, CancellationToken ct = default)
   {
      await context.Leagues.Where(l => l.Id == id)
         .ExecuteUpdateAsync
         (
            b => b
               .SetProperty(l => l.Name, name)
               .SetProperty(l => l.Description, description)
               .SetProperty(l => l.RequiredRating, requiredRating), 
            ct
         );
   }

   public async Task DeleteLeagueAsync(Guid id, CancellationToken ct = default)
   {
      await context.Leagues
         .Where(l => l.Id == id)
         .ExecuteDeleteAsync(ct);
   }
}