namespace PTS.Entity.Tests.DAL;

using PTS.Entity.Domain;
using PTS.Entity.DAL;
using PTS.Entity.Util; 

public class MetadataRepositoryTests { 

    [Test]
    public void GetDbVersion() {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db);
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        MetadataRepository repo = new MetadataRepository(db);

        var version = repo.GetDbVersion();

        Assert.That(version, Is.EqualTo(1));
    }
}