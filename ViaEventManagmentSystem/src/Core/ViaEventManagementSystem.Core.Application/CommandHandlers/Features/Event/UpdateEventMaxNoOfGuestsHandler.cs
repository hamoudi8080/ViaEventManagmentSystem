using ViaEventManagementSystem.Core.AppEntry.Commands;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events;
using ViaEventManagementSystem.Core.Domain.Common.UnitOfWork;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.Application.CommandHandlers.Features.Event;

public class UpdateEventMaxNoOfGuestsHandler : ICommandHandler<UpdateEventMaxNoOfGuestsCommand>
{
    private readonly IViaEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateEventMaxNoOfGuestsHandler(IViaEventRepository eventRepository, IUnitOfWork unitOfWork)
    {
        (_eventRepository, _unitOfWork) = (eventRepository, unitOfWork);
    }

    public async Task<Result> Handle(UpdateEventMaxNoOfGuestsCommand command)
    {
        var _ViaEvent = await _eventRepository.GetAsync(command.EventId.Value);
        Result eventMaxNoOfGuestsResult = _ViaEvent.SetMaxNumberOfGuests(command.MaxNoOfGuests);

        if (_ViaEvent == null) return Result.Failure(Error.NotFound(ErrorMessage.General.EventNotFound));

        if (eventMaxNoOfGuestsResult.IsSuccess) await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}