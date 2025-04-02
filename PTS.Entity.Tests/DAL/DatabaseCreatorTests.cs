namespace PTS.Entity.Tests.DAL;

using PTS.Entity.DAL;

public class Tests
{
    [Test]
    public void BasicCreationTest()
    {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db.GetConnection());
        creator.CreateDatabase(1);
    }
}