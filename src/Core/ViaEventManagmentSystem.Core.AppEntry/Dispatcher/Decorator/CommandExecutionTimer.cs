using System.Diagnostics;
using ViaEventManagmentSystem.Core.AppEntry.Commands;

namespace ViaEventManagmentSystem.Core.AppEntry.Dispatcher.Decorator;

public class CommandExecutionTimer(ICommandDispatcher next) : ICommandDispatcher
{
    
    public float Milliseconds { get; set; }
    public async Task<Result> DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var result = await next.DispatchAsync(command);

        Milliseconds = stopwatch.ElapsedMilliseconds;

        Console.WriteLine($"Command {typeof(TCommand).Name} executed in {Milliseconds} ms");
        return result;
    }
}