using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.Entities.Invitation;
using ViaEventManagmentSystem.Core.Domain.Common.Values;

namespace ViaEventManagmentSystem.Core.Domain.Contracts;

public interface IInviteRepository : IViaRepository<Invitation, ViaId>
{
    
}