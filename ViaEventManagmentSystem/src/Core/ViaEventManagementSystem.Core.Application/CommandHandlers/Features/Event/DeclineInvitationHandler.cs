using ViaEventManagementSystem.Core.AppEntry.Commands;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events;
using ViaEventManagementSystem.Core.Domain.Common.UnitOfWork;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.Application.CommandHandlers.Features.Event;

public class DeclineInvitationHandler : ICommandHandler<DeclineInvitationCommand>
{
    private readonly IViaEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeclineInvitationHandler(IViaEventRepository eventRepository, IUnitOfWork unitOfWork)
    {
        (_eventRepository, _unitOfWork) = (eventRepository, unitOfWork);
    }

    public async Task<Result> Handle(DeclineInvitationCommand command)
    {
        var _ViaEvent = await _eventRepository.GetAsync(command.EventId.Value);
        var eventDeclineResult = _ViaEvent.RejectGuestInvitation(command.GuestId);

        if (_ViaEvent == null) return Result.Failure(Error.NotFound(ErrorMessage.General.EventNotFound));
        if (eventDeclineResult.IsSuccess) await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}