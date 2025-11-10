var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Only set URLs when NOT in Docker
if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") != "true")
{
    builder.WebHost.UseUrls("http://localhost:5001", "https://localhost:5002");
}
// In Docker, it will use ASPNETCORE_URLS environment variable (http://+:8080)

// Uncomment when you want database functionality
/*
builder.Services.RegisterDispatcher();
builder.Services.RegisterHandlers();
builder.Services.RegisterWritePersistence(@"Data Source=/app/data/VEADatabaseProduction.db");
*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Remove this in Docker (or make it conditional)
// app.UseHttpsRedirection();

app.MapControllers();
app.Run();