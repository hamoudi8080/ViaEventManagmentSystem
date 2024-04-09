using System.Diagnostics;
using ViaEventManagmentSystem.Core.AppEntry.Commands;

namespace ViaEventManagmentSystem.Core.AppEntry.Dispatcher.Decorator;

public class CommandExecutionTimer(ICommandDispatcher next) : ICommandDispatcher
{
    public Task<Result> DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var result = next.DispatchAsync(command);

        TimeSpan timeSpan = stopwatch.Elapsed;

        Console.WriteLine($"Command {typeof(TCommand).Name} executed in {timeSpan.TotalMilliseconds} ms");
        return result;
    }
}