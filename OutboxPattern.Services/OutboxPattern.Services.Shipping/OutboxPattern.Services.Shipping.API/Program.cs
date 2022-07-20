using MassTransit;
using OutboxPattern.Demo.Services.API.Application.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var serviceBusConnectionString = builder.Configuration.GetConnectionString("serviceBusConnectionString") ??
    throw new ArgumentException("serviceBusConnectionString enviromental variable not defined.");

builder.Services.AddMassTransit(c =>
{
    c.AddConsumersFromNamespaceContaining<OrderCreatedConsumer>();

    c.UsingAzureServiceBus((ctx, cfg) =>
    {
        cfg.Host(serviceBusConnectionString);

        cfg.ConfigureEndpoints(ctx);
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
