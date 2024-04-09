using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Common.Values;

namespace ViaEventManagmentSystem.Core.Domain.Contracts;

public interface IViaEventRepository: IViaRepository<ViaEvent, ViaId>
{
    
}