using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ServiceA.Adapters;
using ServiceA.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurações de serviços
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(sp => new PaymentQueueAdapter(
    builder.Configuration["AzureServiceBus:ConnectionString"],
    builder.Configuration["AzureServiceBus:QueueName"]
));

builder.Services.AddScoped<PaymentService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();

app.MapControllers();

app.Run();
