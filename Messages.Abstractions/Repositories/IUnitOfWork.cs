namespace Messages.Abstractions.Repositories;
public interface IUnitOfWork
{
    Task BeginTransactionAsync(CancellationToken cancellationToken);

    Task CommitTransactionAsync();

    Task RollbackTransactionAsync();
}
