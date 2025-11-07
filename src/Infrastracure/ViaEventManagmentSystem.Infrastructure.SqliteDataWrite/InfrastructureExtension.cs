using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events;
using ViaEventManagementSystem.Core.Domain.Aggregates.Guests;
using ViaEventManagementSystem.Core.Domain.Common.UnitOfWork;
using ViaEventManagmentSystem.Infrastructure.SqliteDataWrite.GuestPersistance;
using ViaEventManagmentSystem.Infrastructure.SqliteDataWrite.UnitOfWork;
using ViaEventManagmentSystem.Infrastructure.SqliteDataWrite.ViaEventPersistance;

namespace ViaEventManagmentSystem.Infrastructure.SqliteDataWrite;

public static class InfrastructureExtension
{
    /// <summary>
    /// Registers all write-side persistence services including DbContext, Repositories, and UnitOfWork
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="connectionString">Database connection string</param>
    /// <returns>Service collection for chaining</returns>
    public static IServiceCollection RegisterWritePersistence(
        this IServiceCollection services,
        string connectionString)
    {
        // Register DbContext
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite(connectionString);
        });

        // Register Repositories
        services.AddScoped<IViaEventRepository, ViaEventRepoEfc>();
        services.AddScoped<IGuestRepository, GuestRepoEfc>();

        // Register Unit of Work
        services.AddScoped<IUnitOfWork, SqliteUnitOfWork>();

        return services;
    }
}
