using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using UnitTests.Common.Factories.EventFactory;
using ViaEventManagmentSystem.Core.AppEntry.Commands;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.AppEntry.Dispatcher;
using ViaEventManagmentSystem.Core.AppEntry.Dispatcher.Decorator;

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