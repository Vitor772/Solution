using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using ServiceB.Adapters;
using ServiceB.Repositories;
using ServiceB.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurações de serviços
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(builder.Configuration["MongoDb:ConnectionString"]));
builder.Services.AddScoped(sp =>
    new PaymentRepository(sp.GetRequiredService<IMongoClient>()));
builder.Services.AddScoped<PaymentService>();
builder.Services.AddSingleton(sp =>
    new PaymentQueueConsumer(
        builder.Configuration["AzureServiceBus:ConnectionString"],
        builder.Configuration["AzureServiceBus:QueueName"],
        sp.GetRequiredService<PaymentService>()
    ));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();

app.MapControllers();

var consumer = app.Services.GetRequiredService<PaymentQueueConsumer>();
await consumer.StartAsync();

app.Lifetime.ApplicationStopping.Register(async () => await consumer.StopAsync());

app.Run();
