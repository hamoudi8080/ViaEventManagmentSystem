using ViaEventManagmentSystem.Core.AppEntry.Commands;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Common.UnitOfWork;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.Application.CommandHandlers.Features.Event;

public class MakeEventPrivateHandler : ICommandHandler<MakeEventPrivateCommand>
{
      
    private readonly IViaEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public MakeEventPrivateHandler(IViaEventRepository eventRepository, IUnitOfWork unitOfWork)
        => (_eventRepository, _unitOfWork) = (eventRepository, unitOfWork);


    public async Task<Result> Handle(MakeEventPrivateCommand command)
    {
        var _ViaEvent = await _eventRepository.GetById(command.EventId);
        Result eventPublicResult = _ViaEvent.MakeEventPrivate();

        if (_ViaEvent == null)
        {
            return Result.Failure(Error.NotFound(ErrorMessage.EventNotFound));
        }
        if (eventPublicResult.IsSuccess)
        {
            _unitOfWork.SaveChangesAsync();
            
        }
       
        return Result.Success();
    }
}