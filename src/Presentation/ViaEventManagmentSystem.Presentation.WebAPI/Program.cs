/*
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddControllers();

 
 
builder.Services.RegisterDispatcher();
builder.Services.RegisterHandlers();
builder.Services.RegisterWritePersistence(@"Data Source = C:\TRMO\RiderProjects\ViaEventAssociation\src\Infrastructure\ViaEventAssociation.Infrastructure.EfcDmPersistence\VEADatabaseProduction.db");
 
var app = builder.Build();
app.MapControllers(); // <--- Add this line
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();

*/



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(); // <--- Add this line
/*
builder.Services.RegisterDispatcher();
builder.Services.RegisterHandlers();
builder.Services.RegisterWritePersistence(@"Data Source = C:\TRMO\RiderProjects\ViaEventAssociation\src\Infrastructure\ViaEventAssociation.Infrastructure.EfcDmPersistence\VEADatabaseProduction.db");
*/
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers(); // <--- Add this line
app.Run();
