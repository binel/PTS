namespace PTS.Entity.Tests.DAL;

using PTS.Entity.DAL;
using PTS.Entity.Domain;

class UserRepositoryTests {

    [Test]
    public void CreateAndReadUser()
    {
        DatabaseCreator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        UserRepository repo = new UserRepository(Database.GetConnection());

        User user = new User {
            Username = "TestUser", 
            Description = "Unit Test User",
            DisplayName = "Test User",
            PasswordHashVersion = 1,
            PasswordHash = "xlkt"
        };

        repo.AddUser(user);

        var readUser = repo.GetUserByUsername("TestUser");
        Assert.That(readUser, Is.Not.Null);
        Assert.That(user.Username, Is.EqualTo(readUser.Username));
    }
}