using ViaEventManagmentSystem.Core.AppEntry.Commands;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Common.UnitOfWork;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.Application.CommandHandlers.Features.Event;

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
            return Result.Failure(Error.NotFound(ErrorMessage.EventNotFound));
        }

        if (eventDescriptionResult.IsSuccess)
        {
            await _unitOfWork.SaveChangesAsync();
            
        }

        return Result.Success();
    }
}