using UnitTests.Common.Factories.GuestFactory;
using ViaEventManagementSystem.Core.Domain.Aggregates.Guests;
using ViaEventManagementSystem.Core.Domain.Aggregates.Guests.ValueObjects;


namespace IntegrationTest.AppDbContextConfigrationTests;

public class GuestConfigTest
{
    [Fact] 
    public async Task TestGuestCreationAndRetrievalByGuid()
    {
        await using var ctx = AppDbContextTest.InitializeDatabaseContext();
        var guestId = GuestFactory.ValidGuestId();
        var guest = ViaGuest.Create(guestId);
        var firstName = FirstName.Create("Hamo");
        var lastName = LastName.Create("Resho");
        
        var email = Email.Create("John@via.dk");
        if (email.IsSuccess)
        {
            guest.Payload.UpdateEmail(email.Payload);
        }
        guest.Payload.UpdateFirstName(firstName.Payload);
        guest.Payload.UpdateLastName(lastName.Payload);
        guest.Payload.UpdateEmail(email.Payload);
       // await AppDbContextTest.SaveAndClearAsync(  ctx);
        var retrieved = ctx.Guests.SingleOrDefault(x => x.Id == guest.Payload.Id);
        Assert.NotNull(retrieved);
    }
    
}