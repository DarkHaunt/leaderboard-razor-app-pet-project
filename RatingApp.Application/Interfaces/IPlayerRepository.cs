using System.Linq.Expressions;
using RatingApp.Domain.Entities;
using RatingApp.Domain.Specifications;

namespace RatingApp.Application.Interfaces;

public interface IPlayerRepository
{
   Task<List<PlayerEntity>> GetAllPlayersAsync(Specification<PlayerEntity>? specification = null, Expression<Func<PlayerEntity, object>>? orderBy = null, CancellationToken ct = default);
   Task<PlayerEntity?> GetPlayerByIdAsync(Guid id, CancellationToken ct = default);
   Task AddPlayerAsync(Guid id, string nickname, int rating, Guid? leagueId = null, CancellationToken ct = default);
   Task UpdatePlayerAsync(Guid id, string nickname, int rating, CancellationToken ct = default);
   Task DeletePlayerAsync(Guid id, CancellationToken ct = default);
}