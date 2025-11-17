/*
public class CreateEvent
{

   [Fact]
   public async Task CreateEvent_ShouldReturnOk()
   {
       await using WebApplicationFactory< Program> webAppFac = new WebApplicationFactory<Program>();
       HttpClient client = webAppFac.CreateClient();

       // act

       var eid = Guid.NewGuid();
       var id = EventId.Create(eid.ToString());
       string eventTitle = "Test Event";
       DateTime startDateTime = DateTime.Now.AddDays(1);
       DateTime endDateTime = startDateTime.AddHours(2);
       int maxNumberOfGuests = 33;
       string eventDescription = "Test Event Description";

       // Act
       var result = CreateEventCommand.Create(id.Payload.Value.ToString(), eventTitle, startDateTime, endDateTime, maxNumberOfGuests, eventDescription);

     //  var newEvent = new {  };
        HttpResponseMessage response = await client.PostAsync("/api/events/create", JsonContent.Create(result.Payload));

        // assert part
        CreateEventResponse createEventResponse = (await response.Content.ReadFromJsonAsync<CreateEventResponse>())!;

        // Add assertions here to verify the properties of createEventResponse
        // Assert the HTTP response
        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

    }



}
*/

