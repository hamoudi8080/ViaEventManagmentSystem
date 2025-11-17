using ViaEventManagementSystem.Core.AppEntry.Commands;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events;
using ViaEventManagementSystem.Core.Domain.Common.UnitOfWork;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.Application.CommandHandlers.Features.Event;

public class AcceptInvitationHandler : ICommandHandler<AcceptInvitationCommand>
{
    private readonly IViaEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AcceptInvitationHandler(IViaEventRepository eventRepository, IUnitOfWork unitOfWork)
    {
        (_eventRepository, _unitOfWork) = (eventRepository, unitOfWork);
    }

    public async Task<Result> Handle(AcceptInvitationCommand command)
    {
        var viaEvent = await _eventRepository.GetAsync(command.EventId.Value);
        var result = viaEvent.AcceptGuestInvitation(command.GuestId);

        if (viaEvent == null) return Result.Failure(Error.NotFound(ErrorMessage.General.EventNotFound));

        if (result.IsSuccess) await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}