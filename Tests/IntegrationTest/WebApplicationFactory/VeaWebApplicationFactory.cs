using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
 
using ViaEventManagmentSystem.Infrastructure.SqliteDataWrite;
using ViaEventManagmentSystem.Infrastructure.EfcQueries;

 
 
internal class VeaWebApplicationFactory : WebApplicationFactory<Program>
 
{
    private IServiceCollection serviceCollection;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // setup extra test services.
        builder.ConfigureTestServices(services =>
        {
            serviceCollection = services;
            // Remove the existing DbContexts and Options
            services.RemoveAll(typeof(DbContextOptions<AppDbContext>));
            services.RemoveAll(typeof(DbContextOptions<VeadatabaseProductionContext>));
            services.RemoveAll<AppDbContext>();
            services.RemoveAll<VeadatabaseProductionContext>();

            string connString = GetConnectionString();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite(connString);
            });
            services.AddDbContext<VeadatabaseProductionContext>(options =>
            {
                options.UseSqlite(connString);
            });

//            services.AddScoped<ISystemTime, IntegrationTestsFakeTime>();
            
            SetupCleanDatabase(services);
        });
    }

    private void SetupCleanDatabase(IServiceCollection services)
    {
        AppDbContext dmContext = services.BuildServiceProvider().GetService<AppDbContext>()!;
        dmContext.Database.EnsureDeleted();
        dmContext.Database.EnsureCreated();
        // could seed database here?
    }

    private string GetConnectionString()
    {
        string testDbName = "Test" + Guid.NewGuid() + ".db";
        return "Data Source = " + testDbName;
    }

    protected override void Dispose(bool disposing)
    {
        // clean up the database
        AppDbContext dmContext = serviceCollection.BuildServiceProvider().GetService<AppDbContext>()!;
        dmContext.Database.EnsureDeleted();
        base.Dispose(disposing);
    }
}
