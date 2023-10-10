using Messages.Abstractions.DatabaseContext;
using Messages.Abstractions.Entities;

namespace Messages.Abstractions.Repositories;
public class PaymentRepository : IPaymentRepository
{
    private readonly OrderDbContext _dbContext;

    public PaymentRepository(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Payment payment, CancellationToken cancellationToken = default)
    {
        await _dbContext.Payments.AddAsync(payment, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
