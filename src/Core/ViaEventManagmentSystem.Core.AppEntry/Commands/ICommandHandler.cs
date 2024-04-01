namespace ViaEventManagmentSystem.Core.AppEntry.Commands;

public interface ICommandHandler <in TCommand> where TCommand : ICommand
{
   //TODO: Why onle one interface?
   public Task<Result> Handle(TCommand tcommand);
}