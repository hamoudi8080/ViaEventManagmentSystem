using Microsoft.Extensions.DependencyInjection;
using UnitTests.Common.Factories.EventFactory;
using ViaEventManagementSystem.Core.AppEntry.Commands;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.AppEntry.Dispatcher;

namespace UnitTests.Common.Dispatcher;

public class CommandDispatcherTest
{
    [Fact]
    public async Task DispatchAsync_DispatchesCommandToHandler()
    {
        // Arrange
        var readyevent = ViaEventTestFactory.ReadyEvent();

        var validEventTitle = "FootBallHall";
        var updateEventTitleCommand =
            UpdateEventTitleCommand.Create(ViaEventTestFactory.ValidEventId().Value.ToString(), validEventTitle);


        var services = new ServiceCollection();

        services.AddScoped<ICommandDispatcher, CommandDispatcher>();
        services.AddScoped<ICommandHandler<UpdateEventTitleCommand>, MockTitleHandler>();

        var serviceProvider = services.BuildServiceProvider();

        var commandDispatcher = serviceProvider.GetRequiredService<ICommandDispatcher>();
        var handler = (MockTitleHandler)serviceProvider.GetService<ICommandHandler<UpdateEventTitleCommand>>()!;

        // Act
        var result = await commandDispatcher.DispatchAsync(updateEventTitleCommand.Payload!);

        // Assert

        Assert.True(result.IsSuccess);
        Assert.True(handler.WasCalled);
    }
}