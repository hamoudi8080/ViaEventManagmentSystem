using ViaEventManagmentSystem.Core.AppEntry.Commands;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Common.UnitOfWork;

namespace ViaEventManagmentSystem.Core.Application.CommandHandlers.Features.Event;

public class MakeEventReadyHandler: ICommandHandler<MakeEventReadyCommand>
{
    private readonly IViaEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public MakeEventReadyHandler(IViaEventRepository eventRepository, IUnitOfWork unitOfWork)
        => (_eventRepository, _unitOfWork) = (eventRepository, unitOfWork);


    public async Task<Result> Handle(MakeEventReadyCommand command)
    {
        var viaEvent = await _eventRepository.GetById(command.EventId);
        Result eventReadyResult = viaEvent.MakeEventReady();
        
        if (eventReadyResult.IsSuccess)
        {
            _unitOfWork.SaveChangesAsync();
            return Result.Success();
        }
        
        return Result.Failure(eventReadyResult.Error);
        
    }
}