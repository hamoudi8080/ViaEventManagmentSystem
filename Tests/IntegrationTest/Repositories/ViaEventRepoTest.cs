
namespace IntegrationTest.Repositories;


public class ViaEventRepoTest
{
    /*
    private readonly AppDbContext _context;
    private readonly IViaEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public ViaEventRepoTest()
    {
        var factory = new DesignTimeContextFactory();
        _context = factory.CreateDbContext(new string[] { });
        _context.Database.EnsureDeleted(); // Delete the database
        _context.Database.EnsureCreated(); // Create a new database
       _context.Database.Migrate();

       // _eventRepository = new ViaEventRepoEfc(_context);
        _unitOfWork = new SqliteUnitOfWork(_context);
    }
    
    
    
    [Fact]
    public async Task AddViaEventToDatabase()
    {
        
        // Start a transaction
        using var transaction = _context.Database.BeginTransaction();

        // Arrange
        var viaEvent = ViaEventTestFactory.ReadyEvent();

        // Act
        await _eventRepository.Add(viaEvent);
        await _unitOfWork.SaveChangesAsync();

        // Assert
        var retrievedEvent = await _eventRepository.GetById(viaEvent.Id);
        Assert.NotNull(retrievedEvent);
        Assert.Equal(viaEvent.Id, retrievedEvent.Id);
        // Add more assertions here based on what you expect

        // Commit the transaction
        transaction.Commit();
        
        
    }
    
    */
    
}