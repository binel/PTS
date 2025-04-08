namespace PTS.Entity.Tests.DAL;

using PTS.Entity.Domain;
using PTS.Entity.DAL;
using PTS.Entity.Util; 

public class TagRepositoryTests {
    [Test]
    public void CreateTag()
    {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db.GetConnection());
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        TagRepository repo = new TagRepository(db.GetConnection());

        var tag = new Tag {
            Name = "Test Tag",
            CreatedAt = DateTime.UtcNow
        };

        repo.AddTag(tag);

        var tagCount = repo.GetTagCount();

        Assert.That(tagCount, Is.EqualTo(1));
    }

    [Test]
    public void GetTagById() {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db.GetConnection());
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        TagRepository repo = new TagRepository(db.GetConnection());

        var tag = new Tag {
            Name = "Test Tag",
            CreatedAt = DateTime.UtcNow
        };

        repo.AddTag(tag);

        var readTag = repo.GetTagById(1);

        Assert.That(tag.Name, Is.EqualTo(readTag.Name));        
    }

    [Test]
    public void GetTags() {
         Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db.GetConnection());
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        TagRepository repo = new TagRepository(db.GetConnection());

        var tag1 = new Tag {
            Name = "Test Tag 1",
            CreatedAt = DateTime.UtcNow
        };

        repo.AddTag(tag1);

        var tag2 = new Tag {
            Name = "Test Tag 2",
            CreatedAt = DateTime.UtcNow
        };

        repo.AddTag(tag2);        

        var readTags = repo.GetAllTags();

        Assert.That(2, Is.EqualTo(readTags.Count()));        
    }

    [Test]
    public void DeleteTag() {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db.GetConnection());
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        TagRepository repo = new TagRepository(db.GetConnection());

        var tag = new Tag {
            Name = "Test Tag",
            CreatedAt = DateTime.UtcNow
        };

        repo.AddTag(tag);

        repo.DeleteTag(1);

        var tagCount = repo.GetTagCount();

        Assert.That(tagCount, Is.EqualTo(0));        
    }
}