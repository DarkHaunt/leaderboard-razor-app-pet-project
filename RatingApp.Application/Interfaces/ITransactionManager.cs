using CSharpFunctionalExtensions;
using RatingApp.Domain.Errors;

namespace RatingApp.Application.Interfaces;

public interface ITransactionManager : IDisposable, IAsyncDisposable
{
   Task<UnitResult<Error>> BeginTransactionAsync(CancellationToken ct = default);
   Task<UnitResult<Error>> CommitTransactionAsync(CancellationToken ct = default);
   Task<UnitResult<Error>> SaveChangesAsync(CancellationToken ct = default);
   Task<UnitResult<Error>> RollbackTransaction(CancellationToken ct = default);
}