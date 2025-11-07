using ViaEventManagementSystem.Core.AppEntry.Commands;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events;
using ViaEventManagementSystem.Core.Domain.Common.UnitOfWork;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.Application.CommandHandlers.Features.Event;

public class UpdateEventTitleHandler : ICommandHandler<UpdateEventTitleCommand>
{
    
    /*
     * This class is responsible for handling the UpdateEventTitleCommand.
     * It will update the title of the event with the given id.
     * It will return a Result object with the success or failure message.
     */

    private readonly IViaEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateEventTitleHandler(IViaEventRepository eventRepository, IUnitOfWork unitOfWork)
        => (_eventRepository, _unitOfWork) = (eventRepository, unitOfWork);

    public async Task<Result> Handle(UpdateEventTitleCommand command)
    {
        var viaEvent = await _eventRepository.GetById(command.EventId);

        // Check for null BEFORE using the object
        if (viaEvent == null)
        {
            return Result.Failure(Error.NotFound(ErrorMessage.General.EventNotFound));
        }

        // Update the title
        Result eventTitleResult = viaEvent.UpdateTitle(command.EventTitle);

        // Return early if update failed
        if (!eventTitleResult.IsSuccess)
        {
            return eventTitleResult;
        }

        // Save changes through UnitOfWork
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}