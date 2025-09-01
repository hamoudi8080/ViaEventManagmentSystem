using Microsoft.EntityFrameworkCore;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Domain.Aggregates.Guests;
using ViaEventManagementSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagementSystem.Core.Domain.Common.Values;
using IGuestRepository = ViaEventManagementSystem.Core.Domain.Aggregates.Guests.IGuestRepository;

namespace ViaEventManagmentSystem.Infrastructure.SqliteDataWrite.GuestPersistance;

public class GuestRepoEfc(AppDbContext context) : IGuestRepository
{
    public Task<ViaGuest> GetById(GuestId id)
    {
        return context.Set<ViaGuest>().SingleAsync(e => e.Id == id);
    }

    public async Task Add(ViaGuest entity)
    {
        await context.Set<ViaGuest>().AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ViaGuest>> GetAll()
    {
        return await context.Set<ViaGuest>().ToListAsync();
    }

    public async Task Remove(GuestId id)
    {
        var entity = await context.Set<ViaEvent>().FindAsync(id);
        if (entity != null)
        {
            context.Set<ViaEvent>().Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}