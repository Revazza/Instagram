using Instagram.Api;
using Instagram.Api.Mappings;
using Instagram.Application;
using Instagram.Application.Common.Converters;
using Instagram.Application.Hubs.Chat;
using Instagram.Application.Hubs.Notification;
using Instagram.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
    });
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerConfigurations();

builder.Services
    .AddMappings()
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();
app.MapHub<ChatHub>("ChatHub");
app.MapHub<NotificationHub>("NotificationHub");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
