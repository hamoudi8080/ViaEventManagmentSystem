using ViaEventManagmentSystem.Core.AppEntry.Commands;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Common.UnitOfWork;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.Application.CommandHandlers.Features.Event;

internal class UpdateEventTitleHandler :ICommandHandler<UpdateEventTitleCommand>
{
    //TODO: ASK if UpdateEventTitleHandler must be in app entry assembly inside Features folder or in the core application assembly
    /* 
     * This class is responsible for handling the UpdateEventTitleCommand.
     * It will update the title of the event with the given id.
     * It will return a Result object with the success or failure message.
     */
    
    private readonly IViaEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;

    internal UpdateEventTitleHandler(IViaEventRepository eventRepository, IUnitOfWork unitOfWork)
    => (_eventRepository, _unitOfWork) = (eventRepository, unitOfWork);

    public async Task<Result> Handle(UpdateEventTitleCommand command)
    {
        ViaEvent? eventToUpdate = await _eventRepository.GetById(command.EventId);
        Result eventTitleResult = eventToUpdate.UpdateTitle(command.EventTitle.ToString());
        
        if (!eventTitleResult.IsSuccess)
        {
            return Result.Failure(Error.AddCustomError("Failed to update event title due to invalid title."));
        }

        // eventToUpdate.UpdateTitle(command.Title);

        //_eventRepository.Update(eventToUpdate);

       // await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
