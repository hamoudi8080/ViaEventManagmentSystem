using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Infrastracure.SqliteDataWrite;
using Xunit;

namespace IntegrationTests;

public class AppDbContextTest
{
    [Fact]
    public async Task StrongIdAsFk_ValidTarget()
    {
        /*[Fact]
  public async Task StrongIdAsFk_ValidTarget()
  {
      await using MyDbContext ctx = SetupContext();
      EntityY entityY = new (YId.Create());
      await SaveAndClearAsync(entityY, ctx);

      EntityX entityX = new(Guid.NewGuid());
      entityX.SetFk(entityY.Id);

      await SaveAndClearAsync(entityX, ctx);

      EntityX retrievedX = ctx.EntityXs.Single(x => x.Id == entityX.Id);

      EntityY? retrievedY = ctx.EntityYs
          .SingleOrDefault(y => y.Id == retrievedX.foreignKeyToY);

      Assert.NotNull(retrievedY);*/
    }
}