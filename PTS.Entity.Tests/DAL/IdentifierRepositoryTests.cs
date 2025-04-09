namespace PTS.Entity.Tests.DAL;

using PTS.Entity.Domain;
using PTS.Entity.DAL;
using PTS.Entity.Util; 

public class IdentifierRepositoryTests { 
    [Test]
    public void AddAndGetIdentifiers() {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db);
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        IdentifierRepository repo = new IdentifierRepository(db);

        var identifier = new Identifier {
            Text = "TEST",
            HighestValue = 1,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        repo.AddIdentifier(identifier);

        var identifiers = repo.GetAllIdentifiers();

        Assert.That(identifiers.Count(), Is.EqualTo(1));
        Assert.That(identifiers[0].Text, Is.EqualTo("TEST"));
        Assert.That(identifiers[0].HighestValue, Is.EqualTo(1));
    }

    [Test]
    public void UpdateHighestValue() {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db);
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        IdentifierRepository repo = new IdentifierRepository(db);

        var identifier = new Identifier {
            Text = "TEST",
            HighestValue = 1,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        repo.AddIdentifier(identifier);
        repo.UpdateHighestValue(1, 2);

        var identifiers = repo.GetAllIdentifiers();

        Assert.That(identifiers.Count(), Is.EqualTo(1));
        Assert.That(identifiers[0].Text, Is.EqualTo("TEST"));
        Assert.That(identifiers[0].HighestValue, Is.EqualTo(2));
    }
}