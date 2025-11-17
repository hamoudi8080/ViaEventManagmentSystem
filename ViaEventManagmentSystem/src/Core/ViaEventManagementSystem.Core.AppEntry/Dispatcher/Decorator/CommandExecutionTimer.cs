using System.Diagnostics;
using ViaEventManagementSystem.Core.AppEntry.Commands;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.AppEntry.Dispatcher.Decorator;

public class CommandExecutionTimer(ICommandDispatcher next) : ICommandDispatcher
{
 
    public double Milliseconds { get; private set; }

    public async Task<Result> DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand
    {
        var sw = Stopwatch.StartNew();
        var result = await next.DispatchAsync(command);
        sw.Stop();

        Milliseconds = sw.Elapsed.TotalMilliseconds;
        Console.WriteLine($"Command {typeof(TCommand).Name} executed in {Milliseconds} ms");
        return result;
    }
}