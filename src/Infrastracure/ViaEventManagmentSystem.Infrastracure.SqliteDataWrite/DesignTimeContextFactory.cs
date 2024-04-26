using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ViaEventManagmentSystem.Infrastracure.SqliteDataWrite;
//refers to the time where e.g. a migration is created.
//Here, an instance of our DbContext will be created.

/*But when just generating the database or verifying a configuration, or testing whether a DbContext can actually be created,
 or verifying the output sql looks right, we need some way to provide this argument.*/

public class DesignTimeContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlite(@"Data Source = VEADatabaseProduction.db");
        return new AppDbContext(optionsBuilder.Options);
    }
}