﻿using ViaEventManagmentSystem.Core.AppEntry.Commands;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Common.UnitOfWork;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.Application.CommandHandlers.Features.Event;

public class GuestCancelsParticipationHandler : ICommandHandler<GuestCancelsParticipationCommand>
{
    private IViaEventRepository _eventRepository;
    private IUnitOfWork _unitOfWork;

    public GuestCancelsParticipationHandler(IViaEventRepository eventRepository, IUnitOfWork unitOfWork)
        => (_eventRepository, _unitOfWork) = (eventRepository, unitOfWork);

    public async Task<Result> Handle(GuestCancelsParticipationCommand command)
    {
        var viaEvent = await _eventRepository.GetById(command.EventId);
        Result result = viaEvent.CancelGuestParticipation(command.GuestId);

        if (viaEvent == null)
        {
            return Result.Failure(Error.NotFound(ErrorMessage.EventNotFound));
        }

        if (result.IsSuccess)
        {
            await _unitOfWork.SaveChangesAsync();
        }

        return Result.Success();
    }
}