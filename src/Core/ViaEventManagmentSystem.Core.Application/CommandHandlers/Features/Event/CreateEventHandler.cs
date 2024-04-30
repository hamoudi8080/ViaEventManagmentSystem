using ViaEventManagmentSystem.Core.AppEntry.Commands;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Common.UnitOfWork;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.Application.CommandHandlers.Features.Event;

public class CreateEventHandler : ICommandHandler<CreateEventCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IViaEventRepository _eventRepository;
    
    public CreateEventHandler(IViaEventRepository eventRepository, IUnitOfWork unitOfWork)
        => (_eventRepository, _unitOfWork) = (eventRepository, unitOfWork);
    

    public Task<Result> Handle(CreateEventCommand tcommand)
    {
       var  repoevent= _eventRepository.Add(tcommand.ViaEvent);

       if ( repoevent == null)
       {
             Result.Failure(Error.AddCustomError("Failed to create guest"));
          
       }
       _unitOfWork.SaveChangesAsync();
       return Task.FromResult(Result.Success());
         


    }
}