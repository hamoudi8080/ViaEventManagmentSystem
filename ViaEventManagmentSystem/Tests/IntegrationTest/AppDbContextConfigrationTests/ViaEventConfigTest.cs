namespace IntegrationTest.AppDbContextConfigrationTests;

public class ViaEventConfigTest
{
    /*
        [Fact]
        public async Task TestEventCreationAndRetrievalByGuid()
        {
            await using var ctx = AppDbContextTest.InitializeDatabaseContext();
            Console.WriteLine(Path.GetFullPath("TestDatabase.db"));
            var eventId = ViaEventTestFactory.ValidEventId();

            var viaEvent = ViaEvent.Create(eventId).Payload;
            await AppDbContextTest.SaveAndClearAsync(viaEvent, ctx);
            var retrieved = ctx.ViaEvents.SingleOrDefault(x => x.Id == viaEvent.Id);
            Assert.NotNull(retrieved);

        }

    */
}