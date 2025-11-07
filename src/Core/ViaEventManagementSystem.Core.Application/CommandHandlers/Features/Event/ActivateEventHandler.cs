using ViaEventManagementSystem.Core.AppEntry.Commands;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events;
using ViaEventManagementSystem.Core.Domain.Common.UnitOfWork;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.Application.CommandHandlers.Features.Event;

public class ActivateEventHandler : ICommandHandler<ActivateEventCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IViaEventRepository _eventRepository;

    public ActivateEventHandler(IViaEventRepository eventRepository, IUnitOfWork unitOfWork) =>
        (_unitOfWork, _eventRepository) = (unitOfWork, eventRepository);


    public async Task<Result> Handle(ActivateEventCommand command)
    {
        ViaEvent viaEvent = await _eventRepository.GetById(command.EventId);

        if (viaEvent == null)
        {
            return Result.Failure(Error.NotFound(ErrorMessage.General.EventNotFound));
        }

        Result activateEventResult = viaEvent.ActivateEvent();

        if (activateEventResult.IsSuccess)
        {
            await _unitOfWork.SaveChangesAsync();
        }

        return activateEventResult;
    }
}