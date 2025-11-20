
namespace IntegrationTest;

public class AppDbContextTest
{
    /*
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

    }

    public static async Task SaveAndClearAsync<T>(T entity, AppDbContext context) 
        where T : class
    {
        await context.Set<T>().AddAsync(entity);
        await context.SaveChangesAsync();
        context.ChangeTracker.Clear();
    }
    */
     
}