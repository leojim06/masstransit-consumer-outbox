using Messages.Abstractions.DatabaseContext;
using Messages.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using OrderConsumer.Repositories;

namespace OrderConsumer.Configuration;
public static class WorkerExtensions
{
    public static void ConfigureService(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddDbContext<OrderDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();

        services.AddMasstransitWorkerConfiguration(configuration);
    }
}
