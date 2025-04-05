namespace PTS.Entity.Tests.DAL;

using PTS.Entity.Domain;
using PTS.Entity.DAL;

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

        var userCount = repo.GetTicketCount();

        Assert.That(userCount, Is.EqualTo(1));        
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

        var userCount = repo.GetTicketCount();

        Assert.That(userCount, Is.EqualTo(1));  

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

        var userCount = repo.GetTicketCount();

        Assert.That(userCount, Is.EqualTo(1));  

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

        var userCount = repo.GetTicketCount();

        Assert.That(userCount, Is.EqualTo(1));  

    }
}