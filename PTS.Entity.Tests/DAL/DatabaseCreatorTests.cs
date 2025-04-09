namespace PTS.Entity.Tests.DAL;

using PTS.Entity.DAL;

public class Tests
{
    // TODO improve 
    [Test]
    public void BasicCreationTest()
    {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db);
        creator.CreateDatabase(1);
    }
}