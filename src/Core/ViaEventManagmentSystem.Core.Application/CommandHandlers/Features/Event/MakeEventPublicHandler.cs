using ViaEventManagmentSystem.Core.AppEntry.Commands;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Common.UnitOfWork;

namespace ViaEventManagmentSystem.Core.Application.CommandHandlers.Features.Event;

public class MakeEventPublicHandler : ICommandHandler<MakeEventPublicCommand>
{
    //TODO: ASK why readonly and not just  private IViaEventRepository _eventRepository; private IUnitOfWork _unitOfWork;
    
    private readonly IViaEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public MakeEventPublicHandler(IViaEventRepository eventRepository, IUnitOfWork unitOfWork)
        => (_eventRepository, _unitOfWork) = (eventRepository, unitOfWork);
    
    public async Task<Result> Handle(MakeEventPublicCommand command)
    {
        var _ViaEvent = await _eventRepository.GetById(command.EventId);
        Result eventPublicResult = _ViaEvent.MakeEventPublic();

        if (eventPublicResult.IsSuccess)
        {
            _unitOfWork.SaveChangesAsync();
            return Result.Success();
        }
        return Result.Failure(eventPublicResult.Error);
      
    }
}