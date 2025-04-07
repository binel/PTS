namespace PTS.Entity.Tests.DAL;

using PTS.Entity.Domain;
using PTS.Entity.DAL;
using PTS.Entity.Util; 

public class TicketRepositoryTests {

    [Test]
    public void CreateTicketAllColumns() {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db.GetConnection());
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        TicketRepository repo = new TicketRepository(db.GetConnection());

        Ticket ticket = new Ticket {
            Identifier = "TEST-1",
            Title = "Test Ticket",
            Description = "This is a test ticket",
            Priority = Priority.None,
            Author = new User {
                Id = 1,
                Username = "TestUser",
                DisplayName = "Test User",
                Description = "Unit Testing User",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                PasswordHashVersion = 1,
                PasswordHash = "xltr",
                PasswordUpdatedAt = DateTime.UtcNow,
                LastLoginAt = DateTime.UtcNow
            },
            Project = new Project {
                Id = 1
            },
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
    public void CreateTicketNoProjectKey() {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db.GetConnection());
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        TicketRepository repo = new TicketRepository(db.GetConnection());

        Ticket ticket = new Ticket {
            Identifier = "TEST-1",
            Title = "Test Ticket",
            Description = "This is a test ticket",
            Priority = Priority.None,
            Author = new User {
                Id = 1,
                Username = "TestUser",
                DisplayName = "Test User",
                Description = "Unit Testing User",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                PasswordHashVersion = 1,
                PasswordHash = "xltr",
                PasswordUpdatedAt = DateTime.UtcNow,
                LastLoginAt = DateTime.UtcNow
            },
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
        DatabaseCreator creator = new DatabaseCreator(db.GetConnection());
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        TicketRepository repo = new TicketRepository(db.GetConnection());

        Ticket ticket = new Ticket {
            Identifier = "TEST-1",
            Title = "Test Ticket",
            Description = "This is a test ticket",
            Priority = Priority.None,
            Author = new User {
                Id = 1,
                Username = "TestUser",
                DisplayName = "Test User",
                Description = "Unit Testing User",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                PasswordHashVersion = 1,
                PasswordHash = "xltr",
                PasswordUpdatedAt = DateTime.UtcNow,
                LastLoginAt = DateTime.UtcNow
            },
            Project = new Project {
                Id = 1
            },
            Status = Status.Someday,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        repo.AddTicket(ticket);

        var ticketCount = repo.GetTicketCount();

        Assert.That(ticketCount, Is.EqualTo(1));  

    }

    [Test]
    public void CreateTicketOnlyRequiredFields() {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db.GetConnection());
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        TicketRepository repo = new TicketRepository(db.GetConnection());

        Ticket ticket = new Ticket {
            Identifier = "TEST-1",
            Title = "Test Ticket",
            Description = "This is a test ticket",
            Priority = Priority.None,
            Author = new User {
                Id = 1,
                Username = "TestUser",
                DisplayName = "Test User",
                Description = "Unit Testing User",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                PasswordHashVersion = 1,
                PasswordHash = "xltr",
                PasswordUpdatedAt = DateTime.UtcNow,
                LastLoginAt = DateTime.UtcNow
            },
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
        DatabaseCreator creator = new DatabaseCreator(db.GetConnection());
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        TicketRepository repo = new TicketRepository(db.GetConnection());

        Ticket ticket = GetTicketWithAllFields();
        repo.AddTicket(ticket);  

        var readTicket = repo.GetTicketById(1);

        Assert.That(ticket.Identifier, Is.EqualTo(readTicket.Identifier));
        Assert.That(ticket.Title, Is.EqualTo(readTicket.Title));
        Assert.That(ticket.Description, Is.EqualTo(readTicket.Description));
        Assert.That(ticket.Priority, Is.EqualTo(readTicket.Priority));
        Assert.That(ticket.Author.Id, Is.EqualTo(readTicket.Author.Id));
        // TODO rest of author 
        Assert.That(ticket.Status, Is.EqualTo(readTicket.Status));
        // TODO comments, tags, relationship, work history, project
        Assert.That(DateTimeConverter.StripToSeconds(ticket.CreatedAt), Is.EqualTo(readTicket.CreatedAt));
        Assert.That(DateTimeConverter.StripToSeconds(ticket.UpdatedAt), Is.EqualTo(readTicket.UpdatedAt));
        Assert.That(DateTimeConverter.StripToSeconds(ticket.ResolvedAt.Value), Is.EqualTo(readTicket.ResolvedAt));
    }

    [Test]
    public void UpdateTitle() {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db.GetConnection());
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        TicketRepository repo = new TicketRepository(db.GetConnection());

        Ticket ticket = GetTicketWithAllFields();
        repo.AddTicket(ticket);

        repo.UpdateTicketTitle(1, "New Test Title");

        var readTicket = repo.GetTicketById(1);

        Assert.That(readTicket.Title, Is.EqualTo("New Test Title")); 
    }

    private Ticket GetTicketWithAllFields() {
        return new Ticket {
            Identifier = "TEST-1",
            Title = "Test Ticket",
            Description = "This is a test ticket",
            Priority = Priority.None,
            Author = new User {
                Id = 1,
                Username = "TestUser",
                DisplayName = "Test User",
                Description = "Unit Testing User",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                PasswordHashVersion = 1,
                PasswordHash = "xltr",
                PasswordUpdatedAt = DateTime.UtcNow,
                LastLoginAt = DateTime.UtcNow
            },
            Project = new Project {
                Id = 1
            },
            Status = Status.Someday,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            ResolvedAt = DateTime.UtcNow
        };       
    }
}