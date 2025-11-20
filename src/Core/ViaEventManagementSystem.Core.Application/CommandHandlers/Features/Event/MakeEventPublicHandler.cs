using ViaEventManagementSystem.Core.AppEntry.Commands;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events;
using ViaEventManagementSystem.Core.Domain.Common.UnitOfWork;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.Application.CommandHandlers.Features.Event;

public class MakeEventPublicHandler : ICommandHandler<MakeEventPublicCommand>
{
    //TODO: ASK why readonly and not just  private IViaEventRepository _eventRepository; private IUnitOfWork _unitOfWork;
    
    private readonly IViaEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public MakeEventPublicHandler(IViaEventRepository eventRepository, IUnitOfWork unitOfWork)
        => (_eventRepository, _unitOfWork) = (eventRepository, unitOfWork);
    
    public async Task<Result> Handle(MakeEventPublicCommand command)
    {
        var viaEvent = await _eventRepository.GetById(command.EventId);

        if (viaEvent == null)
        {
            return Result.Failure(Error.NotFound(ErrorMessage.General.EventNotFound));
        }

        Result eventPublicResult = viaEvent.MakeEventPublic();

        if (!eventPublicResult.IsSuccess)
        {
            return eventPublicResult;
        }

        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
      
    }
}