using ViaEventManagementSystem.Core.AppEntry.Commands;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace UnitTests.Common.Dispatcher;

public class MockTitleHandler : ICommandHandler<UpdateEventTitleCommand>
{
    public bool WasCalled { get; set; }

    public Task<Result> Handle(UpdateEventTitleCommand tcommand)
    {
        WasCalled = true;
        return Task.FromResult(Result.Success());
    }
}