namespace PTS.Entity.Tests.DAL;

using PTS.Entity.Domain;
using PTS.Entity.DAL;
using PTS.Entity.Util; 

public class StatusHistoryRepositoryTests {
    [Test]
    public void CreateHistory(){
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db.GetConnection());
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        StatusHistoryRepository repo = new StatusHistoryRepository(db.GetConnection());

        var history = new StatusHistory {
            TicketId = 1,
            MoverId = 1,
            FromStatus = Status.Someday,
            ToStatus = Status.Todo,
            CreatedAt = DateTime.UtcNow
        };

        repo.AddStatusHistory(history);

        var transitions = repo.GetCountStatusTransitions();

        Assert.That(transitions, Is.EqualTo(1));
    }

    [Test]
    public void GetHistoryForTicket() {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db.GetConnection());
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        StatusHistoryRepository repo = new StatusHistoryRepository(db.GetConnection());

        var history = new StatusHistory {
            TicketId = 1,
            MoverId = 1,
            FromStatus = Status.Someday,
            ToStatus = Status.Todo,
            CreatedAt = DateTime.UtcNow
        };

        repo.AddStatusHistory(history);

        var transitions = repo.GetStatusHistoryForTicket(1);

        Assert.That(transitions.Count(), Is.EqualTo(1));
        Assert.That(transitions[0].FromStatus, Is.EqualTo(Status.Someday));
        Assert.That(transitions[0].ToStatus, Is.EqualTo(Status.Todo));
    }
}