using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.Entities.Invitation;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.Entities.ValueObjects;

namespace ViaEventManagmentSystem.Core.Domain.Contracts;

public interface IViaRequestRepository : IViaRepository<InvitationRequest, InvitationId>
{
    
}