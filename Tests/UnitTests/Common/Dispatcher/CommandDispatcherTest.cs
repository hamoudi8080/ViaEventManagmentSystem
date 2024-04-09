using ViaEventManagmentSystem.Core.AppEntry.Commands;
using ViaEventManagmentSystem.Core.AppEntry.Dispatcher;
using Microsoft.Extensions.DependencyInjection;
using UnitTests.Common.Factories.EventFactory;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;

namespace UnitTests.Common.Dispatcher;

public class CommandDispatcherTest
{
    [Fact]
    public async Task DispatchAsync_DispatchesCommandToHandler()
    {
           
        // Arrange
        var readyevent = ViaEventTestFactory.ReadyEvent();

        string validEventTitle = "FootBallHall";
        var updateEventTitleCommand =
            UpdateEventTitleCommand.Create(ViaEventTestFactory.ValidEventId().Value.ToString(), validEventTitle);
        
        
        var services = new ServiceCollection();
        
        services.AddScoped<ICommandDispatcher, CommandDispatcher>();
        services.AddScoped<ICommandHandler<UpdateEventTitleCommand>, MockTitleHandler>();
        
        var serviceProvider = services.BuildServiceProvider();

        var commandDispatcher = serviceProvider.GetRequiredService<ICommandDispatcher>();


        // Act
        var result = await commandDispatcher.DispatchAsync(updateEventTitleCommand.Payload!);

        // Assert
        MockTitleHandler handler = (MockTitleHandler) serviceProvider.GetService<ICommandHandler<UpdateEventTitleCommand>>()!;
        Assert.True(result.IsSuccess);
        Assert.True(handler.WasCalled);
    }
}