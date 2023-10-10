using MassTransit;
using Messages.Abstractions.Messages;

namespace OrderConsumer.Consumer;
public class ConsumeOrderProcessed : IConsumer<OrderProcessed>
{
    public Task Consume(ConsumeContext<OrderProcessed> context)
    {
        var message = context.Message;
        return Task.CompletedTask;
    }
}
