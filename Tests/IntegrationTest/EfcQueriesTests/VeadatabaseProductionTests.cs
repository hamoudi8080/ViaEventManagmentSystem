using Microsoft.EntityFrameworkCore;
using ViaEventManagmentSystem.Infrastructure.EfcQueries;
using ViaEventManagmentSystem.Infrastructure.EfcQueries.SeedFactories;

namespace IntegrationTest.EfcQueriesTests;

public class VeadatabaseProductionTests
{
    
    [Fact]
    public async Task SeedMethodTest()
    {
        var setupReadyContext = VeadatabaseProductionContext.SetupReadContext();
        
        var seededContext = VeadatabaseProductionContext.Seed(setupReadyContext);
        
        Assert.NotEmpty(seededContext.Guests);
        Assert.Equal(50, seededContext.Guests.Count());
        
        Assert.Equal(28, seededContext.ViaEvents.Count());
    }

 
}