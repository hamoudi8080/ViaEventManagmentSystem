using Microsoft.EntityFrameworkCore;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Domain.Common.Values;

namespace ViaEventManagmentSystem.Infrastructure.SqliteDataWrite.ViaEventPersistance;

public  class ViaEventRepoEfc(AppDbContext context) : RepositoryEfcBase<ViaEvent,EventId>(context), IViaEventRepository
{
    public override Task<ViaEvent> GetById(EventId id)
    {
        return context.Set<ViaEvent>().SingleAsync(e => e.Id == id);
    }

    public override async Task Add(ViaEvent entity)
    {
        await context.Set<ViaEvent>().AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public  async Task<IEnumerable<ViaEvent>> GetAll()
    {
        return await context.Set<ViaEvent>().ToListAsync();
    }

    public override async Task Remove(EventId id)
    {
        var entity = await context.Set<ViaEvent>().FindAsync(id);
        if (entity != null)
        {
            context.Set<ViaEvent>().Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}