using ViaEventManagmentSystem.Core.AppEntry.Commands;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Common.UnitOfWork;

namespace ViaEventManagmentSystem.Core.Application.CommandHandlers.Features.Event;

public class DeclineInvitationHandler : ICommandHandler<DeclineInvitationCommand>
{
    private IViaEventRepository _eventRepository;
    private IUnitOfWork _unitOfWork;
    
    public DeclineInvitationHandler(IViaEventRepository eventRepository, IUnitOfWork unitOfWork)
        => (_eventRepository, _unitOfWork) = (eventRepository, unitOfWork);
    
    public async Task<Result> Handle(DeclineInvitationCommand command)
    {
        var _ViaEvent = await _eventRepository.GetById(command.EventId);
        Result eventDeclineResult = _ViaEvent.RejectGuestInvitation(command.GuestId);
        
        if (eventDeclineResult.IsSuccess)
        {
            await _unitOfWork.SaveChangesAsync();
            return Result.Success();
        }
        
        return Result.Failure(eventDeclineResult.Error);
    }
}