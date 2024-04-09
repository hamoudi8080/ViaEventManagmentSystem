using ViaEventManagmentSystem.Core.AppEntry.Commands;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Common.UnitOfWork;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.Application.CommandHandlers.Features.Event;

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
        
       
        if (eventTitleResult.IsSuccess)
        {
            await _unitOfWork.SaveChangesAsync();
        }

        return Result.Success();
    }
}