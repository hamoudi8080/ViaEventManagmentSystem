using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests;
using ViaEventManagmentSystem.Core.Domain.Common.Values;

namespace ViaEventManagmentSystem.Core.Domain.Contracts;

public interface IGuestRepository: IViaRepository<ViaGuest, ViaId>
{
    
}