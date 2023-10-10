namespace Messages.Abstractions.Messages;
public record OrderPublished
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
