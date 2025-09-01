using UnitTests.Common.Factories.EventFactory;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.MakeEventPrivateTest;

public class MakeEventPrivateCommandTest
{
    [Fact]
    public void Create_ShouldReturnMakeEventPrivateCommand_WhenValidEventId()
    {
        // Arrange
       
        var id = ViaEventTestFactory.ValidEventId().Value.ToString();
        
        // Act
        var result = MakeEventPrivateCommand.Create(id);
        var command = result.Payload;
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(command.EventId);
        Assert.NotEmpty(command.EventId.ToString()!);
    }
    
    [Fact]
    public void Create_FallingTest()
    {
        // Arrange
        string invalidEventId = "";
        
        // Act
        Result<MakeEventPrivateCommand> result = MakeEventPrivateCommand.Create(invalidEventId);
        
        // Assert
        Assert.False(result.IsSuccess);
    }
    
}