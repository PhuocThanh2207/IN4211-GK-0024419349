using EduHub.Application.Interfaces;
using EduHub.Application.Services;
using EduHub.Domain.Interfaces;
using EduHub.Infrastructure.Notifications;
using EduHub.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Đăng ký DI (Câu 3b & Câu 5d)
builder.Services.AddSingleton<IEventRepository, InMemoryEventRepository>();
builder.Services.AddScoped<INotificationService>(_ => NotificationFactory.Create("email"));
builder.Services.AddScoped<EventService>();

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