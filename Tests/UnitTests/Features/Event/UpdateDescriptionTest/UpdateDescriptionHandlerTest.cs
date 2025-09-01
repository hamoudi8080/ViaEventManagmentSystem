using UnitTests.Common.Factories.EventFactory;
using UnitTests.Fakes;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.Application.CommandHandlers.Features.Event;
using ViaEventManagementSystem.Core.Domain.Common.UnitOfWork;

namespace UnitTests.Features.Event.UpdateDescriptionTest;

public class UpdateDescriptionHandlerTest
{
    
    private readonly IUnitOfWork _unitOfWork = new UnitOfWork();
    [Fact]
    public async Task UpdateDescriptionHandler_GivenValidCommand_Success()
    {
        // Arrange

        var viaEvent = ViaEventTestFactory.ReadyEvent();
        var description = "Test Description";
        
        var eventRepository = new EventRepository();
        eventRepository.Add(viaEvent);
        
        var command = UpdateDescriptionCommand.Create(viaEvent._eventId.Value.ToString(), description).Payload;
        var handler = new UpdateDescriptionHandler(eventRepository, _unitOfWork);

        // Act
        var result = await handler.Handle(command);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Single(eventRepository._Events);


        Assert.Equal(command.EventId, viaEvent._eventId);
        Assert.Equal(command.Description.Value.ToString(), viaEvent._Description.Value);
    }
}