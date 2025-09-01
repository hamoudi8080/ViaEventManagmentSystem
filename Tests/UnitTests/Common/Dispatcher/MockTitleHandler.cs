using ViaEventManagementSystem.Core.AppEntry.Commands;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

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