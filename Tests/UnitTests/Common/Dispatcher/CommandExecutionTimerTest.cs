using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using UnitTests.Common.Factories.EventFactory;
using ViaEventManagementSystem.Core.AppEntry.Commands;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.AppEntry.Dispatcher;
using ViaEventManagementSystem.Core.AppEntry.Dispatcher.Decorator;

namespace UnitTests.Common.Dispatcher;

public class CommandExecutionTimerTest
{
    [Fact]
    public async Task DispatchAsync_ExecutesCommandAndLogsExecutionTime()
    {
        // Arrange
        string validEventTitle = "FootBallHall";
        var updateEventTitleCommand =
            UpdateEventTitleCommand.Create(ViaEventTestFactory.ValidEventId().Value.ToString(), validEventTitle);


        var services = new ServiceCollection();

        services.AddScoped<ICommandDispatcher, CommandDispatcher>();
        services.AddScoped<ICommandHandler<UpdateEventTitleCommand>, MockTitleHandler>();

        var serviceProvider = services.BuildServiceProvider();

        var commandDispatcher = serviceProvider.GetRequiredService<ICommandDispatcher>();

        // Create an instance of CommandExecutionTimer and pass the commandDispatcher to its constructor
        var timingCommandDispatcher = new CommandExecutionTimer(commandDispatcher);

        // Act
        var result = await timingCommandDispatcher.DispatchAsync(updateEventTitleCommand.Payload!);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(timingCommandDispatcher.Milliseconds > 0);
    }



}