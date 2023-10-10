using MassTransit;
using Messages.Abstractions.DatabaseContext;
using Messages.Abstractions.Options;
using OrderConsumer.Consumer;

namespace OrderConsumer.Configuration;

public static class MasstransitWorkerExtensions
{
    public static IServiceCollection AddMasstransitWorkerConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        var busOptions = configuration.GetSection(BrokerOptions.SectionName).Get<BrokerOptions>();

        services.AddMassTransit(x => {
            x.AddEntityFrameworkOutbox<OrderDbContext>(o =>
            {
                o.QueryDelay = TimeSpan.FromSeconds(1);
                o.UseSqlServer();
                o.UseBusOutbox(c => c.DisableDeliveryService());
            });

            x.SetKebabCaseEndpointNameFormatter();

            x.AddConsumer<ConsumeOrderPublished>();
            x.AddConsumer<ConsumeOrderProcessed>();

            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(busOptions.ConnectionString);
                cfg.ConfigureEndpoints(ctx);
                // cfg.UseInMemoryOutbox(); 
                // when activate this line and remove lines 16 - 21 the outbox works fine
            });
        });

        return services;
    }
}
