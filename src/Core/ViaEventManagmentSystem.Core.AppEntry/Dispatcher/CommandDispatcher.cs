using ViaEventManagmentSystem.Core.AppEntry.Commands;

namespace ViaEventManagmentSystem.Core.AppEntry.Dispatcher;


public class CommandDispatcher (IServiceProvider serviceProvider) : ICommandDispatcher
{
    public async Task<Result> DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand
    {
        // Here you should implement the logic to dispatch the command to its appropriate handler.
        // This is a placeholder implementation and should be replaced with your actual logic.
        
        
        Type serviceType = typeof(ICommandHandler<TCommand>);
        var service = serviceProvider.GetService(serviceType);

        if (service  ==null)
        {
            throw new InvalidOperationException(nameof(ICommandHandler<TCommand>));
            
        }
        ICommandHandler<TCommand> handler = (ICommandHandler<TCommand>)service;
        return await handler.Handle(command);
    }
}