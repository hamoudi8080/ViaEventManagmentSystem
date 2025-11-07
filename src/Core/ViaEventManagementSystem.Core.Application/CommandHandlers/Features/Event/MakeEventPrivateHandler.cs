using ViaEventManagementSystem.Core.AppEntry.Commands;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events;
using ViaEventManagementSystem.Core.Domain.Common.UnitOfWork;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.Application.CommandHandlers.Features.Event;

public class MakeEventPrivateHandler : ICommandHandler<MakeEventPrivateCommand>
{
      
    private readonly IViaEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public MakeEventPrivateHandler(IViaEventRepository eventRepository, IUnitOfWork unitOfWork)
        => (_eventRepository, _unitOfWork) = (eventRepository, unitOfWork);


    public async Task<Result> Handle(MakeEventPrivateCommand command)
    {
        var _ViaEvent = await _eventRepository.GetById(command.EventId);

        if (_ViaEvent == null)
        {
            return Result.Failure(Error.NotFound(ErrorMessage.General.EventNotFound));
        }

        Result eventPrivateResult = _ViaEvent.MakeEventPrivate();

        if (eventPrivateResult.IsSuccess)
        {
            await _unitOfWork.SaveChangesAsync();
        }

        return eventPrivateResult;
    }
}