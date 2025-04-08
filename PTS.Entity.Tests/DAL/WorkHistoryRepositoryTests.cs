namespace PTS.Entity.Tests.DAL;

using PTS.Entity.Domain;
using PTS.Entity.DAL;
using PTS.Entity.Util; 

public class WorkHistoryRepositoryTests {
    [Test]
    public void CreateWorkHistory() {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db.GetConnection());
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        WorkHistoryRepository repo = new WorkHistoryRepository(db.GetConnection());

        var history = new WorkHistory {
            TicketId = 1,
            StartedAt = DateTime.UtcNow,
            EndedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        repo.AddWorkHistory(history);

        var count = repo.GetCountWorkHistories();

        Assert.That(count, Is.EqualTo(1));
    }

    [Test]
    public void DeleteWorkHistory() {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db.GetConnection());
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        WorkHistoryRepository repo = new WorkHistoryRepository(db.GetConnection());

        var history = new WorkHistory {
            TicketId = 1,
            StartedAt = DateTime.UtcNow,
            EndedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        repo.AddWorkHistory(history);
        repo.DeleteWorkHistory(1);

        var count = repo.GetCountWorkHistories();

        Assert.That(count, Is.EqualTo(0));  
    }

    [Test]
    public void GetWorkHistoryForTicket() {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db.GetConnection());
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        WorkHistoryRepository repo = new WorkHistoryRepository(db.GetConnection());

        var history = new WorkHistory {
            TicketId = 1,
            StartedAt = DateTime.UtcNow,
            EndedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        repo.AddWorkHistory(history);

        var histories = repo.GetWorkHistoryForTicket(1);

        Assert.That(histories.Count(), Is.EqualTo(1));
    }
}