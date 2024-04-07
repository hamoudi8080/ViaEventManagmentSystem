using UnitTests.Common.Factories.GuestFactory;
using UnitTests.Fakes;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Guest;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagmentSystem.Core.Domain.Common.UnitOfWork;

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
        
       
        CreateGuestCommand createGuestCommand = CreateGuestCommand.Create(id.Payload.Value.ToString(),firstName,lastname,email).Payload;;
        
        CreateGuestHandler handler = new(guestRepo, _unitOfWork);
        
        // Act
      
        var result = await  handler.Handle(createGuestCommand);

        // Assert
        Assert.True(result.IsSuccess);
       
        
    }
    
}