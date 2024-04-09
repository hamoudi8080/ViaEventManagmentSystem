using ViaEventManagmentSystem.Core.AppEntry.Commands;

namespace ViaEventManagmentSystem.Core.AppEntry.Dispatcher;

public interface ICommandDispatcher
{
    Task<Result> DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand;
}