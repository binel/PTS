namespace PTS.Entity.Tests.DAL;

using PTS.Entity.DAL;

public class Tests
{
    [Test]
    public void BasicCreationTest()
    {
        DatabaseCreator.CreateDatabase(1);
    }
}