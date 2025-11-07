using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events;
using ViaEventManagementSystem.Core.Domain.Aggregates.Guests;
using ViaEventManagementSystem.Core.Domain.Common.UnitOfWork;
using ViaEventManagmentSystem.Infrastructure.SqliteDataWrite.GuestPersistance;
using ViaEventManagmentSystem.Infrastructure.SqliteDataWrite.UnitOfWork;
using ViaEventManagmentSystem.Infrastructure.SqliteDataWrite.ViaEventPersistance;

namespace ViaEventManagmentSystem.Infrastructure.SqliteDataWrite.Extensions;

public static class PersistenceExtension
{
    public static IServiceCollection RegisterWritePersistence(this IServiceCollection services, string connectionString)
    {
        // Register DbContext
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(connectionString));

        // Register UnitOfWork
        services.AddScoped<IUnitOfWork, SqliteUnitOfWork>();

        // Register Repositories
        services.AddScoped<IViaEventRepository, ViaEventRepoEfc>();
        services.AddScoped<IGuestRepository, GuestRepoEfc>();

        return services;
    }
}
