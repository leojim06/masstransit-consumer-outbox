using OrderConsumer.Configuration;

var builder = Host.CreateDefaultBuilder(args);

var host = builder.ConfigureServices((hostContext, services) =>
{
    services.ConfigureService(hostContext.Configuration);
})
.Build();

await host.RunAsync();

