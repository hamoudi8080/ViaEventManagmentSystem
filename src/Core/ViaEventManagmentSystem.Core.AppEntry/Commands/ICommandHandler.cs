﻿namespace ViaEventManagmentSystem.Core.AppEntry.Commands;

public interface ICommandHandler <in TCommand> where TCommand : ICommand
{
 
   public Task<Result> Handle(TCommand tcommand);
   
}