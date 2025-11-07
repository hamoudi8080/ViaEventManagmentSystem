using ViaEventManagementSystem.Core.Application.Extension;
using ViaEventManagmentSystem.Infrastructure.SqliteDataWrite.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Register Application Services
builder.Services.RegisterDispatcher();
builder.Services.RegisterHandlers();

// Register Database - use relative path in project directory
var databasePath = Path.Combine(Directory.GetCurrentDirectory(), "ViaEventDatabase.db");
builder.Services.RegisterWritePersistence($"Data Source={databasePath}");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
