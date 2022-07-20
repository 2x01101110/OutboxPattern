using MassTransit;
using Microsoft.EntityFrameworkCore;
using OutboxPattern.Services.Ordering.OutboxWorker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var sqlConnectionString = context.Configuration.GetConnectionString("sqlConnectionString") ??
            throw new ArgumentNullException("sqlConnectionString enviromental variable not defined.");

        var serviceBusConnectionString = context.Configuration.GetConnectionString("serviceBusConnectionString") ??
            throw new ArgumentException("serviceBusConnectionString enviromental variable not defined.");

        services.AddDbContext<OutboxDbContext>(builder =>
        {
            builder.UseSqlServer(sqlConnectionString);
        });

        services.AddMassTransit(c =>
        {
            c.UsingAzureServiceBus((ctx, cfg) =>
            {
                cfg.Host(serviceBusConnectionString);

                cfg.ConfigureEndpoints(ctx);
            });
        });

        services.AddHostedService<OutboxPublisher>();
    })
    .Build();

await host.RunAsync();
