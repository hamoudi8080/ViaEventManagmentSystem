using ViaEventManagmentSystem.Core.AppEntry.Commands;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Common.UnitOfWork;

namespace ViaEventManagmentSystem.Core.Application.CommandHandlers.Features.Event;

public class AcceptInvitationHandler : ICommandHandler<AcceptInvitationCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IViaEventRepository _eventRepository;
    
    public AcceptInvitationHandler (IViaEventRepository eventRepository, IUnitOfWork unitOfWork)
        => (_eventRepository, _unitOfWork) = (eventRepository, unitOfWork);

    public async Task<Result> Handle(AcceptInvitationCommand command)
    {
        var viaEvent = await _eventRepository.GetById(command.EventId);

        Result result = viaEvent.AcceptGuestInvitation(command.GuestId);

        if (result.IsSuccess)
        {
            await _unitOfWork.SaveChangesAsync();
            return Result.Success();
        }

        return Result.Failure(result.Error);
    }
}