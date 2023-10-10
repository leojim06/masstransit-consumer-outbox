using MassTransit;
using Messages.Abstractions.DatabaseContext;
using Messages.Abstractions.Options;

namespace PublisherApi.Configuration;

public static class MasstransitExtensions
{
    public static IServiceCollection AddMasstransitConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        var busOptions = configuration.GetSection(BrokerOptions.SectionName).Get<BrokerOptions>();

        services.AddMassTransit(x => {
            x.AddEntityFrameworkOutbox<OrderDbContext>(o =>
            {
                o.QueryDelay = TimeSpan.FromSeconds(1);
                o.UseSqlServer();
                o.UseBusOutbox();
            });

            x.SetKebabCaseEndpointNameFormatter();

            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(busOptions.ConnectionString);
            });
        });

        return services;
    }
}
