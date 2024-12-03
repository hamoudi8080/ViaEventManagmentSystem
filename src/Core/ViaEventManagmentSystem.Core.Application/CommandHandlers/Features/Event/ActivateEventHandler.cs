using ViaEventManagmentSystem.Core.AppEntry.Commands;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Common.UnitOfWork;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.Application.CommandHandlers.Features.Event;

public class ActivateEventHandler : ICommandHandler<ActivateEventCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IViaEventRepository _eventRepository;

    public ActivateEventHandler(IViaEventRepository eventRepository, IUnitOfWork unitOfWork) =>
        (_unitOfWork, _eventRepository) = (unitOfWork, eventRepository);


    public async Task<Result> Handle(ActivateEventCommand command)
    {
        ViaEvent viaEvent = await _eventRepository.GetById(command.EventId);
        Result activateEventResult = viaEvent.ActivateEvent();
        
        if (viaEvent == null)
        {
            return Result.Failure(Error.NotFound(ErrorMessage.EventNotFound));
        }
        
        if (activateEventResult.IsSuccess)
        {
            _unitOfWork.SaveChangesAsync();
        
        }
        
        return Result.Success();
         
    }
}