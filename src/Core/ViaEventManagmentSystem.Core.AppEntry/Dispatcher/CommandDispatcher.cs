using ViaEventManagmentSystem.Core.AppEntry.Commands;

namespace ViaEventManagmentSystem.Core.AppEntry.Dispatcher;


public class CommandDispatcher (IServiceProvider serviceProvider) : ICommandDispatcher
{
    public async Task<Result> DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand
    {
        //the point of this method is to dispatch a command to the appropriate command handler.
        
        /*The typeof operator in C# is used to get the System.Type instance for a type.
         TCommand is a placeholder for any type that implements the ICommand interface.
         So, serviceType will hold the type information of the specific command handler interface for the command being dispatched.*/
        
        //Getting the type of the handler interface for the specific command being dispatched.
        var serviceType = typeof(ICommandHandler<TCommand>);
 
        //  Requesting an instance of that  handler from the dependency injection container.
        var service = serviceProvider.GetService(serviceType);

        if (service==null)
        {
            throw new InvalidOperationException(nameof(ICommandHandler<TCommand>));
            
        }
        //Casting the retrieved service to the  handler interface.
        ICommandHandler<TCommand> handler = (ICommandHandler<TCommand>)service;
        
        //  Calling the Handle method on the command handler with the provided command and returning the result.
        return await handler.Handle(command);
    }
}