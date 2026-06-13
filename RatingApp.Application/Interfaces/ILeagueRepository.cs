using System.Linq.Expressions;
using RatingApp.Domain.Entities;
using RatingApp.Domain.Specifications;

namespace RatingApp.Application.Interfaces;

public interface ILeagueRepository
{
   Task<List<LeagueEntity>> GetAllLeaguesAsync(Specification<LeagueEntity>? specification = null, Expression<Func<LeagueEntity, object>>? orderBy = null, CancellationToken ct = default);
   Task<LeagueEntity?> GetLeagueByIdAsync(Guid id, bool tracked = false, CancellationToken ct = default);
   Task AddLeagueAsync(Guid id, string name, string description, uint requiredRating, CancellationToken ct = default);
   Task UpdateLeagueAsync(Guid id, string name, string description, uint requiredRating, CancellationToken ct = default);
   Task DeleteLeagueAsync(Guid id, CancellationToken ct = default);
   Task AddPlayerToLeague(Guid leagueId, PlayerEntity player, CancellationToken ct = default);
   Task RemovePlayerFromHisLeague(PlayerEntity player, CancellationToken ct = default);
}