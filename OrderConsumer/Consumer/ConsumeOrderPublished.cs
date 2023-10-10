using MassTransit;
using Messages.Abstractions.Entities;
using Messages.Abstractions.Messages;
using Messages.Abstractions.Repositories;

namespace OrderConsumer.Consumer;

public class ConsumeOrderPublished : IConsumer<OrderPublished>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ConsumeOrderPublished(IPublishEndpoint publishEndpoint,
        IPaymentRepository paymentRepository, IUnitOfWork unitOfWork)
    {
        _publishEndpoint = publishEndpoint;
        _paymentRepository = paymentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Consume(ConsumeContext<OrderPublished> context)
    {
        var orderPublished = context.Message;
        var payment = new Payment
        {
            Id = Guid.NewGuid(),
            Amount = 20,
        };

        await _unitOfWork.BeginTransactionAsync(context.CancellationToken);
        await _paymentRepository.AddAsync(payment, context.CancellationToken);
        await _publishEndpoint.Publish<OrderProcessed>(new
        {
            OrderId = orderPublished.Id,
            PaymentId = payment.Id,
        });

        await _unitOfWork.CommitTransactionAsync();
    }
}
