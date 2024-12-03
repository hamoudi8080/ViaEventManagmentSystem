using ViaEventManagmentSystem.Core.AppEntry.Commands;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Common.UnitOfWork;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.Application.CommandHandlers.Features.Event;

public class InviteGuestHandler : ICommandHandler<InviteGuestCommand>
{
    private readonly IViaEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public InviteGuestHandler(IViaEventRepository eventRepository, IUnitOfWork unitOfWork)
        => (_eventRepository, _unitOfWork) = (eventRepository, unitOfWork);
    
    
    public async Task<Result> Handle(InviteGuestCommand command)
    {
     var viaEvent  = await _eventRepository.GetById(command.EventId);
     Result ivnviteguest = viaEvent.InviteGuest(command.GuestId);
     
        if (viaEvent == null)
        {
            return Result.Failure(Error.NotFound(ErrorMessage.EventNotFound));
        }
     
        if (ivnviteguest.IsSuccess)
        {
            _unitOfWork.SaveChangesAsync();
           
        }
        return Result.Success();
     

    }
}