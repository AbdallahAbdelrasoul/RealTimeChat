using RealTimeChat.Presentation.Hubs;
using RealTimeChat.Presentation.Middlewares;
using RealTimeChat.Presentation.ServiceCollectionExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddDataAccessServices(builder.Configuration)
    .AddDomainServices()
    .AddInfrastructureServices()
    .AddApplicationLayerServices()
    .AddAuthenticationServices(builder.Configuration)
    .AddSwaggerGenServices();

// Add SignalR
builder.Services.AddSignalR();

builder.Services.AddSingleton<ExceptionHandlerMiddleware>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseMiddleware<ActiveContextMiddleware>();

app.UseAuthorization();

// Map SignalR Hub
app.MapHub<ChatHub>($"/{nameof(ChatH
app.MapControllers();

app.Run();
