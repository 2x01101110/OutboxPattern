using MassTransit;
using Microsoft.EntityFrameworkCore;
using OutboxPattern.Services.Ordering.API.Application.Consumers;
using OutboxPattern.Services.Ordering.API.Application.Contracts;
using OutboxPattern.Services.Ordering.API.Domain.Orders;
using OutboxPattern.Services.Ordering.API.Infrastructure;
using OutboxPattern.Services.Ordering.API.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var sqlConnectionString =  builder.Configuration.GetConnectionString("sqlConnectionString") ??
    throw new ArgumentNullException("sqlConnectionString enviromental variable is missing.");

var serviceBusConnectionString = builder.Configuration.GetConnectionString("serviceBusConnectionString") ??
    throw new ArgumentException("serviceBusConnectionString enviromental variable not defined.");

builder.Services.AddDbContext<OrderingDbContext>(builder =>
{
    builder.UseSqlServer(sqlConnectionString, x => x.MigrationsHistoryTable("__OrderingServiceMigrationsHistory"));
});

builder.Services.AddTransient<IOrdersRepository, OrderRepository>();

builder.Services.AddMassTransit(cfg =>
{
    cfg.AddRequestClient<ICreateOrder>();
    cfg.AddConsumersFromNamespaceContaining<CreateOrderConsumer>();

    cfg.UsingAzureServiceBus((ctx, c) =>
    {
        c.Host(serviceBusConnectionString);

        c.ConfigureEndpoints(ctx);
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
