using ViaEventManagmentSystem.Core.AppEntry.Commands;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;

namespace UnitTests.Common.Dispatcher;

public class MockTitleHandler : ICommandHandler<UpdateEventTitleCommand>
{
    public Task<Result> Handle(UpdateEventTitleCommand tcommand)
    {
        WasCalled = true;
        return Task.FromResult(Result.Success());
    }

    public bool WasCalled { get; set; }
}