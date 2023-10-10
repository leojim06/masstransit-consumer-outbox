using Messages.Abstractions.Entities;

namespace Messages.Abstractions.Repositories;
public interface IOrderRepository
{
    Task AddAsync(Order order, CancellationToken cancellationToken = default);
}
