namespace Messages.Abstractions.Messages;
public record OrderProcessed
{
    public Guid OrderId { get; set; }
    public Guid PaymentId { get; set; }
}
