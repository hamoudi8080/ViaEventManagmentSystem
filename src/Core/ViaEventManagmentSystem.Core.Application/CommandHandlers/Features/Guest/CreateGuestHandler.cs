using ViaEventManagmentSystem.Core.AppEntry.Commands;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Guest;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests;
using ViaEventManagmentSystem.Core.Domain.Common.UnitOfWork;
using ViaEventManagmentSystem.Core.Tools.OperationResult;


public class CreateGuestHandler : ICommandHandler<CreateGuestCommand>
{
    private readonly IGuestRepository _guestRepository;
    private readonly IUnitOfWork _unitOfWork;


    public CreateGuestHandler(IGuestRepository guestRepository, IUnitOfWork unitOfWork)
        => (_guestRepository, _unitOfWork) = (guestRepository, unitOfWork);

    public async Task<Result> Handle(CreateGuestCommand command)
    {
        var guest = _guestRepository.Add(command.Guest);

        if (guest == null)
        {
            return Result.Failure(Error.AddCustomError("Failed to create guest"));
        }

        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}