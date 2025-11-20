using ViaEventManagementSystem.Core.AppEntry.Commands;
using ViaEventManagementSystem.Core.AppEntry.Commands.Guest;
using ViaEventManagementSystem.Core.Domain.Aggregates.Guests;
using ViaEventManagementSystem.Core.Domain.Common.UnitOfWork;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;


public class CreateGuestHandler : ICommandHandler<CreateGuestCommand>
{
    private readonly IGuestRepository _guestRepository;
    private readonly IUnitOfWork _unitOfWork;


    public CreateGuestHandler(IGuestRepository guestRepository, IUnitOfWork unitOfWork)
        => (_guestRepository, _unitOfWork) = (guestRepository, unitOfWork);

    public async Task<Result> Handle(CreateGuestCommand command)
    {
       // var guest = _guestRepository.Add(command.Guest);
        
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}