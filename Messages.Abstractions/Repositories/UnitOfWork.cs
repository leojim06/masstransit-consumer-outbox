using Messages.Abstractions.DatabaseContext;
using Messages.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace OrderConsumer.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly OrderDbContext _dbContext;

    public UnitOfWork(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private IDbContextTransaction? _currentTransaction;

    public async Task BeginTransactionAsync(CancellationToken cancellationToken)
    {
        _currentTransaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await _dbContext.SaveChangesAsync();
            if (_currentTransaction is not null)
                await _currentTransaction.CommitAsync();

        }
        catch (Exception)
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            _currentTransaction?.Dispose();
        }
    }

    public async Task RollbackTransactionAsync()
    {
        try
        {
            if (_currentTransaction is not null)
                await _currentTransaction.RollbackAsync();
        }
        finally
        {
            _currentTransaction?.Dispose();
        }
    }
}
