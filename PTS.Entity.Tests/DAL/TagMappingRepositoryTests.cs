namespace PTS.Entity.Tests.DAL;

using PTS.Entity.Domain;
using PTS.Entity.DAL;
using PTS.Entity.Util; 

public class TagMappingRepositoryTests { 
    [Test]
    public void AddMapping() {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db.GetConnection());
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        TagMappingRepository repo = new TagMappingRepository(db.GetConnection());

        repo.AddTagAssociation(1, 1);

        var associationCount = repo.GetTagAssociationCount();

        Assert.That(associationCount, Is.EqualTo(1)); 
    }

    [Test]
    public void RemoveMapping() {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db.GetConnection());
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        TagMappingRepository repo = new TagMappingRepository(db.GetConnection());

        repo.AddTagAssociation(1, 1);
        repo.RemoveTagAssociation(1, 1);
        var associationCount = repo.GetTagAssociationCount();

        Assert.That(associationCount, Is.EqualTo(0));
    }

    [Test]
    public void GetTagsOnTicket() {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db.GetConnection());
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        TagMappingRepository repo = new TagMappingRepository(db.GetConnection());

        repo.AddTagAssociation(1, 1);

        var tags = repo.GetTagsOnTicket(1);

        Assert.That(tags.Count(), Is.EqualTo(1));
        Assert.That(tags[0], Is.EqualTo(1));
    }
}