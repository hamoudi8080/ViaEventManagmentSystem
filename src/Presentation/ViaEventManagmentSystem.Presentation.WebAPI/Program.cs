using ViaEventManagementSystem.Core.Application.Extension;
using ViaEventManagmentSystem.Infrastructure.SqliteDataWrite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Register application services
// builder.Services.RegisterDispatcher(); // TODO: Implement dispatcher registration if needed
builder.Services.RegisterHandlers();

// Register infrastructure services (DbContext, Repositories, UnitOfWork)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Data Source=VEADatabaseProduction.db";
builder.Services.RegisterWritePersistence(connectionString);

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
