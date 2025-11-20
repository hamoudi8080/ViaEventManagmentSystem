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
        var _ViaEvent = await _eventRepository.GetById(command.EventId);
        Result eventTitleResult = _ViaEvent.UpdateTitle(command.EventTitle);
        
        if (_ViaEvent == null)
        {
            return Result.Failure(Error.NotFound(ErrorMessage.General.EventNotFound));
        }
        
        if (eventTitleResult.IsSuccess)
        {
            await _unitOfWork.SaveChangesAsync();
        }
        
        return Result.Success();
       
    }
}