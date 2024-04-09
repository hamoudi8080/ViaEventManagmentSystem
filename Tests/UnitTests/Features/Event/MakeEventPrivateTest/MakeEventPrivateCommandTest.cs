using UnitTests.Common.Factories.EventFactory;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;

namespace UnitTests.Features.Event.MakeEventPrivateTest;

public class MakeEventPrivateCommandTest
{
    [Fact]
    public void Create_ShouldReturnMakeEventPrivateCommand_WhenValidEventId()
    {
        // Arrange
       
        var id =ViaEventTestFactory.ValidEventId().Value.ToString();
        
        // Act
        Result<MakeEventPrivateCommand> result = MakeEventPrivateCommand.Create(id);
        MakeEventPrivateCommand command = result.Payload;
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(command.EventId);
        Assert.NotNull(command.EventId.ToString());
        Assert.NotEmpty(command.EventId.ToString());
    }
    
    
}