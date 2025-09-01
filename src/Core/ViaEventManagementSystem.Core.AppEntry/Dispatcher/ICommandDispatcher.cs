﻿using ViaEventManagementSystem.Core.AppEntry.Commands;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.AppEntry.Dispatcher;

public interface ICommandDispatcher
{
    Task<Result> DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand;
}