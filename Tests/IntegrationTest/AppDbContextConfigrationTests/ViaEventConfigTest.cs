using UnitTests.Common.Factories.EventFactory;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;

namespace IntegrationTest.AppDbContextConfigrationTests;


public class ViaEventConfigTest
{
   
        [Fact]
        public async Task TestEventCreationAndRetrievalByGuid()
        {
            await using var ctx = AppDbContextTest.InitializeDatabaseContext();
            Console.WriteLine(Path.GetFullPath("TestDatabase.db"));
            var eventId = ViaEventTestFactory.ValidEventId();

            var viaEvent = ViaEvent.Create(eventId);
            await AppDbContextTest.AddEntityAndSaveChangesAsync(viaEvent, ctx);
            var retrieved = ctx.ViaEvents.SingleOrDefault(x => x.Id == viaEvent.Payload.Id);
            Assert.NotNull(retrieved);
            
       
        }
      
    
    
 
}