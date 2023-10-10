namespace Messages.Abstractions.Options;
public class BrokerOptions
{
    public const string SectionName = "BrokerConfiguration";
    public string ConnectionString { get; set; } = string.Empty;
}
