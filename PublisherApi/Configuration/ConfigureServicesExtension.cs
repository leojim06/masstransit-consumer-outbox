using Messages.Abstractions.DatabaseContext;
using Messages.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using OrderConsumer.Repositories;

namespace PublisherApi.Configuration;

public static class ConfigureServicesExtension
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OrderDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        services.AddMasstransitConfiguration(configuration);
    }
}
