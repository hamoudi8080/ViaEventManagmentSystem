using UnitTests.Common.Factories.GuestFactory;
using UnitTests.Fakes;
using ViaEventManagementSystem.Core.AppEntry.Commands.Guest;
using ViaEventManagementSystem.Core.Application.CommandHandlers.Features.Guest;
using ViaEventManagementSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagementSystem.Core.Domain.Common.UnitOfWork;

namespace UnitTests.Features.GuestTests;

public class CreateGuestHandlerTest
{
    private readonly IUnitOfWork _unitOfWork = new UnitOfWork();

    [Fact]
    public async void CreateGuest_ReturnSuccess()
    {
        // Arrange
        var guest = GuestFactory.CreateGuest();
        var guestRepo = new GuestRepository();

        // Arrange
        var id = GuestId.Create();
        var email = "John@via.dk";
        var firstName = "john";
        var lastname = "resho";


        var createGuestCommand =
            CreateGuestCommand.Create(id.Payload.Value.ToString(), firstName, lastname, email).Payload;
        ;

        CreateGuestHandler handler = new(guestRepo, _unitOfWork);

        // Act

        var result = await handler.Handle(createGuestCommand);

        // Assert
        Assert.True(result.IsSuccess);
    }
}