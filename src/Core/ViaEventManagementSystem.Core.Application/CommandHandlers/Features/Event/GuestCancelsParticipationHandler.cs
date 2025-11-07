using ViaEventManagementSystem.Core.AppEntry.Commands;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events;
using ViaEventManagementSystem.Core.Domain.Common.UnitOfWork;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.Application.CommandHandlers.Features.Event;

public class GuestCancelsParticipationHandler : ICommandHandler<GuestCancelsParticipationCommand>
{
    private IViaEventRepository _eventRepository;
    private IUnitOfWork _unitOfWork;

    public GuestCancelsParticipationHandler(IViaEventRepository eventRepository, IUnitOfWork unitOfWork)
        => (_eventRepository, _unitOfWork) = (eventRepository, unitOfWork);

    public async Task<Result> Handle(GuestCancelsParticipationCommand command)
    {
        var viaEvent = await _eventRepository.GetById(command.EventId);

        if (viaEvent == null)
        {
            return Result.Failure(Error.NotFound(ErrorMessage.General.EventNotFound));
        }

        Result result = viaEvent.CancelGuestParticipation(command.GuestId);

        if (result.IsSuccess)
        {
            await _unitOfWork.SaveChangesAsync();
        }

        return result;
    }
}