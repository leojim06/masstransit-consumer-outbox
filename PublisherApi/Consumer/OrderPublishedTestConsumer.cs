using MassTransit;
using Messages.Abstractions.Messages;

namespace PublisherApi.Consumer;

public class OrderPublishedTestConsumer : IConsumer<OrderPublished>
{
    public Task Consume(ConsumeContext<OrderPublished> context)
    {
        var message = context.Message;
        return Task.CompletedTask;
    }
}