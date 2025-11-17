using ViaEventManagementSystem.Core.AppEntry.Commands;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events;
using ViaEventManagementSystem.Core.Domain.Common.UnitOfWork;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.Application.CommandHandlers.Features.Event;

public class ParticipateGuestHandler : ICommandHandler<ParticipateGuestCommand>
{
    private readonly IViaEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ParticipateGuestHandler(IViaEventRepository eventRepository, IUnitOfWork unitOfWork)
    {
        (_unitOfWork, _eventRepository) = (unitOfWork, eventRepository);
    }


    public async Task<Result> Handle(ParticipateGuestCommand command)
    {
        var viaEvent = await _eventRepository.GetAsync(command.EventId.Value);
        var participateGuestResult = viaEvent.AddGuestParticipation(command.GuestId);

        if (viaEvent == null) return Result.Failure(Error.NotFound(ErrorMessage.General.EventNotFound));

        if (participateGuestResult.IsSuccess) _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}