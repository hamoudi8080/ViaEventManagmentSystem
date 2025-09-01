using ViaEventManagementSystem.Infrastructure.EfcQueries;
using ViaEventManagementSystem.Infrastructure.EfcQueries.Queries;
using ViaEventManagementSytem.Core.QueryContracts.Contracts;
using ViaEventManagementSytem.Core.QueryContracts.Queries;
using ViaEventManagmentSytem.Core.QueryContracts.Contracts;
using ViaEventManagmentSytem.Core.QueryContracts.Queries;

namespace IntegrationTest.EfcQueriesTests;

public class UpcomingEventQueryTest
{
    [Fact]
    public async void GetUpcomingEvents()
    {
        
        //Arrange
        var setupReadyContext = VeadatabaseProductionContext.SetupReadContext();
        
        var seededContext = VeadatabaseProductionContext.Seed(setupReadyContext);
        
        var query = new UpcomingEventsPage.Query(2,5,"");
        IQueryHandler<UpcomingEventsPage.Query, UpcomingEventsPage.Answer> handler1 = new UpcomingEventsPageQueryHandler(seededContext);

        //Act
        var answer = await handler1.HandleAsync(query);
        
        //Assert
        Assert.NotNull(answer);
        Assert.NotNull(answer.Events);
        Assert.True(answer.Events.Count > 0); // Check that at least one event is returned
        Assert.NotNull(answer.Events[0].EventTitle); // Check that the first event has a title
        Assert.True(answer.MaxPageNum >= 0); // Check that the maximum page number is not negative
    }
}