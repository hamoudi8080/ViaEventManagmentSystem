using ViaEventManagementSystem.Core.AppEntry.Commands;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events;
using ViaEventManagementSystem.Core.Domain.Common.UnitOfWork;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.Application.CommandHandlers.Features.Event;

public class CreateEventHandler : ICommandHandler<CreateEventCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IViaEventRepository _eventRepository;
    
    public CreateEventHandler(IViaEventRepository eventRepository, IUnitOfWork unitOfWork)
        => (_eventRepository, _unitOfWork) = (eventRepository, unitOfWork);
    

    public async Task<Result> Handle(CreateEventCommand command)
    {
        // Create the ViaEvent aggregate from the command
        var eventResult = ViaEvent.Create(
            command.EventId,
            command.Title,
            command.Description,
            command.Start,
            command.End,
            command.MaxGuests,
            command.Visibility,
            EventStatus.Draft
        );

        if (!eventResult.IsSuccess)
        {
            return Result.Failure(eventResult.ErrorCollection);
        }

        // Add the event to the repository
        await _eventRepository.Add(eventResult.Payload);

        // Save changes through UnitOfWork
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}