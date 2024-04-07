using UnitTests.Common.Factories.EventFactory;
using UnitTests.Fakes;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.Application.CommandHandlers.Features.Event;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Common.UnitOfWork;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.SetMaxNumberOfGuestsTest;

public class UpdateEventMaxNoOfGuestsHandlerTest 
{
    private readonly IUnitOfWork _unitOfWork = new UnitOfWork();
    
    [Fact]
    public async Task HandleUpdateEventMaxNoOfGuestsCommand_WithValidData_ShouldReturnSuccess()
    {
        // Arrange event and add to the list of events
        var viaEvent = ViaEventTestFactory.CreateActiveEvent();
        
        var eventRepo = new EventRepository();
        eventRepo.Add(viaEvent);
        
        var setMaxNoOfGuests = MaxNumberOfGuests.Create(5);
        viaEvent.SetMaxNumberOfGuests(setMaxNoOfGuests.Payload);
        
        var command = UpdateEventMaxNoOfGuestsCommand.Create(viaEvent._eventId.Value.ToString(), 10).Payload;
        var handler = new UpdateEventMaxNoOfGuestsHandler(eventRepo, _unitOfWork);

        // Act
        Result result = await handler.Handle(command);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(10, viaEvent._MaxNumberOfGuests.Value);

    }
    
    
    [Fact]
    public async Task HandleUpdateEventMaxNoOfGuestsCommand_WithInvalidData_ShouldReturnFailure()
    {
        // Arrange event and add to the list of events
        var viaEvent = ViaEventTestFactory.CreateActiveEvent();
        
        var eventRepo = new EventRepository();
        eventRepo.Add(viaEvent);
        
        var setMaxNoOfGuests = MaxNumberOfGuests.Create(5);
        viaEvent.SetMaxNumberOfGuests(setMaxNoOfGuests.Payload);

        Result<UpdateEventMaxNoOfGuestsCommand> command = UpdateEventMaxNoOfGuestsCommand.Create(viaEvent._eventId.Value.ToString(), 0);
        
        var handler = new UpdateEventMaxNoOfGuestsHandler(eventRepo, _unitOfWork);

        // Act
        Result result = await handler.Handle(command.Payload);

        // Assert
        Assert.False(result.IsSuccess);
       // Assert.Contains(ErrorMessage.InvalidMaxNoOfGuests, result.Error!.Messages);
    }
}