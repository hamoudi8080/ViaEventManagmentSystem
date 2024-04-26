using Microsoft.EntityFrameworkCore;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Infrastracure.SqliteDataWrite;

namespace IntegrationTest;

public class AppDbContextTest
{
    public DbSet<Result<ViaEvent>> Results { get; set; }

    public static AppDbContext InitializeDatabaseContext()
    {
        var dbContextOptionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        var testDatabaseName = $"Test{Guid.NewGuid()}.db";
        dbContextOptionsBuilder.UseSqlite($"Data Source = {testDatabaseName}");
        var dbContext = new AppDbContext(dbContextOptionsBuilder.Options);
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
        return dbContext;
/*
var dbContextOptionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
dbContextOptionsBuilder.UseSqlite("Data Source = TestDatabase.db");
var dbContext = new AppDbContext(dbContextOptionsBuilder.Options);
dbContext.Database.EnsureCreated();
return dbContext;
*/
    }

    public static async Task AddEntityAndSaveChangesAsync<T>(Result<T> result, AppDbContext dbContext)
        where T : class
    {
        
        /*
         * When you define a DbSet<T> property in your DbContext class,
         * you're telling Entity Framework Core that you have a table in your database that corresponds to the entity type T.
         * When you call Set<T>(), Entity Framework Core provides you with a DbSet<T> that you can use to query and save instances of T.
         */
        if (result.IsSuccess)
        {
            await dbContext.Set<T>().AddAsync(result.Payload);
            await dbContext.SaveChangesAsync();
            dbContext.ChangeTracker.Clear();
        }
        else
        {
            // Handle the case when the result is a failure...
        }
    }
}