﻿using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.AppEntry.Commands.Event;

public class UpdateEventTitleCommand : ICommand
{
    public EventId EventId { get; }
    public EventTitle EventTitle { get; }

    private UpdateEventTitleCommand(EventId eventId, EventTitle eventTitle)
    {
        EventId = eventId;
        EventTitle = eventTitle;
    }

    public static Result<UpdateEventTitleCommand> Create(string eventId, string eventTitle)
    {
        var idResult = EventId.Create(eventId);
        var titleResult = EventTitle.Create(eventTitle);


        var combinedResult = Result.CombineResultsInto<UpdateEventTitleCommand>(idResult, titleResult)
            .WithPayloadIfSuccess(() =>
                new UpdateEventTitleCommand(idResult.Payload!, titleResult.Payload!));

        return combinedResult;
    }
}