using ViaEventManagementSystem.Core.AppEntry.Commands;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events;
using ViaEventManagementSystem.Core.Domain.Common.UnitOfWork;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.Application.CommandHandlers.Features.Event;

public class MakeEventReadyHandler: ICommandHandler<MakeEventReadyCommand>
{
    private readonly IViaEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public MakeEventReadyHandler(IViaEventRepository eventRepository, IUnitOfWork unitOfWork)
        => (_eventRepository, _unitOfWork) = (eventRepository, unitOfWork);


    public async Task<Result> Handle(MakeEventReadyCommand command)
    {
        var viaEvent = await _eventRepository.GetById(command.EventId);

        // Check for null BEFORE using the object
        if (viaEvent == null)
        {
            return Result.Failure(Error.NotFound(ErrorMessage.General.EventNotFound));
        }

        // Make event ready
        var eventReadyResult = viaEvent.MakeEventReady();

        // Return early if making ready failed
        if (!eventReadyResult.IsSuccess)
        {
            return Result.Failure(eventReadyResult.ErrorCollection);
        }

        // Save changes through UnitOfWork with await
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}