namespace PTS.Entity.Tests.DAL;

using PTS.Entity.Domain;
using PTS.Entity.DAL;
using PTS.Entity.Util; 

public class TicketRepositoryTests {

    [Test]
    public void CreateTicketAllColumns() {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db);
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        TicketRepository repo = new TicketRepository(db);

        Ticket ticket = new Ticket {
            Identifier = "TEST-1",
            Title = "Test Ticket",
            Description = "This is a test ticket",
            Priority = Priority.None,
            AuthorId = 1,
            Status = Status.Someday,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            ResolvedAt = DateTime.UtcNow
        };

        repo.AddTicket(ticket);

        var ticketCount = repo.GetTicketCount();

        Assert.That(ticketCount, Is.EqualTo(1));        
    }

    [Test]
    public void CreateTicketNoResolvedAt() {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db);
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        TicketRepository repo = new TicketRepository(db);

        Ticket ticket = new Ticket {
            Identifier = "TEST-1",
            Title = "Test Ticket",
            Description = "This is a test ticket",
            Priority = Priority.None,
            AuthorId = 1,
            Status = Status.Someday,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        repo.AddTicket(ticket);

        var ticketCount = repo.GetTicketCount();

        Assert.That(ticketCount, Is.EqualTo(1));  
    }

    [Test]
    public void GetTicketById() {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db);
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        TicketRepository repo = new TicketRepository(db);

        Ticket ticket = GetTicketWithAllFields();
        repo.AddTicket(ticket);  

        var readTicket = repo.GetTicketById(1);

        Assert.That(ticket.Identifier, Is.EqualTo(readTicket.Identifier));
        Assert.That(ticket.Title, Is.EqualTo(readTicket.Title));
        Assert.That(ticket.Description, Is.EqualTo(readTicket.Description));
        Assert.That(ticket.Priority, Is.EqualTo(readTicket.Priority));
        Assert.That(ticket.AuthorId, Is.EqualTo(readTicket.AuthorId));
        Assert.That(ticket.Status, Is.EqualTo(readTicket.Status));
        Assert.That(DateTimeConverter.StripToSeconds(ticket.CreatedAt), Is.EqualTo(readTicket.CreatedAt));
        Assert.That(DateTimeConverter.StripToSeconds(ticket.UpdatedAt), Is.EqualTo(readTicket.UpdatedAt));
        Assert.That(DateTimeConverter.StripToSeconds(ticket.ResolvedAt.Value), Is.EqualTo(readTicket.ResolvedAt));
    }

    [Test]
    public void UpdateTitle() {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db);
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        TicketRepository repo = new TicketRepository(db);

        Ticket ticket = GetTicketWithAllFields();
        repo.AddTicket(ticket);

        repo.UpdateTicketTitle(1, "New Test Title");

        var readTicket = repo.GetTicketById(1);

        Assert.That(readTicket.Title, Is.EqualTo("New Test Title")); 
    }

    [Test]
    public void UpdateDescription() {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db);
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        TicketRepository repo = new TicketRepository(db);

        Ticket ticket = GetTicketWithAllFields();
        repo.AddTicket(ticket);

        repo.UpdateTicketDescription(1, "New Test Description");

        var readTicket = repo.GetTicketById(1);

        Assert.That(readTicket.Description, Is.EqualTo("New Test Description"));         
    }

    [Test]
    public void UpdatePriority() {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db);
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        TicketRepository repo = new TicketRepository(db);

        Ticket ticket = GetTicketWithAllFields();
        repo.AddTicket(ticket);

        repo.UpdatePriority(1, Priority.High);

        var readTicket = repo.GetTicketById(1);

        Assert.That(readTicket.Priority, Is.EqualTo(Priority.High));          
    }

    [Test]
    public void UpdateStatus() {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db);
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        TicketRepository repo = new TicketRepository(db);

        Ticket ticket = GetTicketWithAllFields();
        repo.AddTicket(ticket);

        repo.UpdateTicketStatus(1, Status.InProgress);

        var readTicket = repo.GetTicketById(1);

        Assert.That(readTicket.Status, Is.EqualTo(Status.InProgress));          
    }

    private Ticket GetTicketWithAllFields() {
        return new Ticket {
            Identifier = "TEST-1",
            Title = "Test Ticket",
            Description = "This is a test ticket",
            Priority = Priority.None,
            AuthorId = 1,
            Status = Status.Someday,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            ResolvedAt = DateTime.UtcNow
        };       
    }
}