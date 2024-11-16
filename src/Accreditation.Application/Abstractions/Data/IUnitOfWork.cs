using System.Data;

namespace Accreditation.Application.Abstractions.Data;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    // Task<IDbTransaction> BeginTransactionAsync();
    Task BeginTransactionAsync();

    Task CommitTransactionAsync(CancellationToken cancellationToken = default);

    void Rollback();

}
