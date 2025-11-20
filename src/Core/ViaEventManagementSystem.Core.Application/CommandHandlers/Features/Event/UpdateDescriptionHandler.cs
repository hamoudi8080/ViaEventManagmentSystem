using ViaEventManagementSystem.Core.AppEntry.Commands;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events;
using ViaEventManagementSystem.Core.Domain.Common.UnitOfWork;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.Application.CommandHandlers.Features.Event;

public class UpdateDescriptionHandler : ICommandHandler<UpdateDescriptionCommand>
{
    private readonly IViaEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDescriptionHandler(IViaEventRepository eventRepository, IUnitOfWork unitOfWork)
        => (_eventRepository, _unitOfWork) = (eventRepository, unitOfWork);

    public async Task<Result> Handle(UpdateDescriptionCommand command)
    {
        var _ViaEvent = await _eventRepository.GetById(command.EventId);
        Result eventDescriptionResult = _ViaEvent.UpdateDescription(command.Description);

        if (_ViaEvent == null)
        {
            return Result.Failure(Error.NotFound(ErrorMessage.General.EventNotFound));
        }

        if (eventDescriptionResult.IsSuccess)
        {
            await _unitOfWork.SaveChangesAsync();
            
        }

        return Result.Success();
    }
}