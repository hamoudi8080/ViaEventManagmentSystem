using Microsoft.EntityFrameworkCore;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagmentSystem.Core.Domain.Common.Values;
using IGuestRepository = ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.IGuestRepository;

namespace ViaEventManagmentSystem.Infrastracure.SqliteDataWrite.GuestPersistance;

public class GuestRepoEfc(AppDbContext context) : IGuestRepository
{
    public Task<Guest> GetById(GuestId id)
    {
        return context.Set<Guest>().SingleAsync(e => e.Id == id);
    }

    public async Task Add(Guest entity)
    {
        await context.Set<Guest>().AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Guest>> GetAll()
    {
        return await context.Set<Guest>().ToListAsync();
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