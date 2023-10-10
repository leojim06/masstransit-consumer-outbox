using Messages.Abstractions.Entities;

namespace Messages.Abstractions.Repositories;
public interface IPaymentRepository
{
    Task AddAsync(Payment payment, CancellationToken cancellationToken = default);
}
