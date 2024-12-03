using ViaEventManagmentSystem.Core.AppEntry.Commands;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Common.UnitOfWork;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.Application.CommandHandlers.Features.Event;

public class ParticipateGuestHandler : ICommandHandler<ParticipateGuestCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IViaEventRepository _eventRepository;

    public ParticipateGuestHandler(IViaEventRepository eventRepository, IUnitOfWork unitOfWork)
        => (_unitOfWork, _eventRepository) = (unitOfWork, eventRepository);


    public async Task<Result> Handle(ParticipateGuestCommand command)
    {
        var viaEvent = await _eventRepository.GetById(command.EventId);
        Result participateGuestResult = viaEvent.AddGuestParticipation(command.GuestId);

        if (viaEvent == null)
        {
            return Result.Failure(Error.NotFound(ErrorMessage.EventNotFound));
        }

        if (participateGuestResult.IsSuccess)
        {
            _unitOfWork.SaveChangesAsync();
        }

        return Result.Success();
    }
}