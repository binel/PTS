namespace PTS.Entity.Tests.DAL;

using PTS.Entity.DAL;
using PTS.Entity.Domain;

class UserRepositoryTests {

    [Test]
    public void CreateUser()
    {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db.GetConnection());
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        UserRepository repo = new UserRepository(db.GetConnection());

        var user = GetBasicUserWithoutId();

        repo.AddUser(user);

        var userCount = repo.GetUserCount();

        Assert.That(userCount, Is.EqualTo(1));
    }

    [Test]
    public void GetAllUsers() {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db.GetConnection());
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        UserRepository repo = new UserRepository(db.GetConnection());

        var user1 = GetBasicUserWithoutId();
        var user2 = GetBasicUserWithoutId();

        repo.AddUser(user1);
        repo.AddUser(user2);

        var allUsers = repo.GetAllUsers();
        Assert.That(allUsers, Is.Not.Null);
        Assert.That(allUsers.Count(), Is.EqualTo(2));
        Assert.That(allUsers[0].Id, Is.EqualTo(1));
        Assert.That(allUsers[1].Id, Is.EqualTo(2));     
    }

    [Test]
    public void ReadUserByUsername() {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db.GetConnection());
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        UserRepository repo = new UserRepository(db.GetConnection());

        var user = GetBasicUserWithoutId();

        repo.AddUser(user);

        var readUser = repo.GetUserByUsername("TestUser");
        Assert.That(readUser, Is.Not.Null);
        Assert.That(readUser.Id, Is.EqualTo(1));
        AssertUserFieldsEqualExceptId(user, readUser);
    }

    [Test]
    public void ReadUserById() {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db.GetConnection());
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        UserRepository repo = new UserRepository(db.GetConnection());

        var user = GetBasicUserWithoutId();

        repo.AddUser(user);

        var readUser = repo.GetUser(1);
        Assert.That(readUser, Is.Not.Null);
        Assert.That(readUser.Id, Is.EqualTo(1));
        AssertUserFieldsEqualExceptId(user, readUser);
    }

    [Test]
    public void UpdatePassword() {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db.GetConnection());
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        UserRepository repo = new UserRepository(db.GetConnection());

        var user = GetBasicUserWithoutId();

        repo.AddUser(user);

        repo.SetUserPassword(1, "newhash", 1);

        var readUser = repo.GetUser(1);
        Assert.That(readUser, Is.Not.Null);
        Assert.That(readUser.PasswordHash, Is.EqualTo("newhash"));
        Assert.That(readUser.PasswordUpdatedAt, Is.GreaterThan(user.PasswordUpdatedAt));       
    }

    [Test]
    public void SetLastLoginTime() {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db.GetConnection());
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        UserRepository repo = new UserRepository(db.GetConnection());

        var user = GetBasicUserWithoutId();

        repo.AddUser(user);

        repo.SetUserLastLoginToNow(1);

        var readUser = repo.GetUser(1);
        Assert.That(readUser, Is.Not.Null);
        Assert.That(readUser.LastLoginAt, Is.GreaterThan(user.LastLoginAt));          
    }

    [Test]
    public void SetUsername() {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db.GetConnection());
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        UserRepository repo = new UserRepository(db.GetConnection());

        var user = GetBasicUserWithoutId();

        repo.AddUser(user);

        repo.SetUserUsername(1, "NewUsername");

        var readUser = repo.GetUser(1);
        Assert.That(readUser, Is.Not.Null);
        Assert.That(readUser.Username, Is.EqualTo("NewUsername"));          
    }

    [Test]
    public void SetDisplayName() {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db.GetConnection());
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        UserRepository repo = new UserRepository(db.GetConnection());

        var user = GetBasicUserWithoutId();

        repo.AddUser(user);

        repo.SetUserDisplayName(1, "New Display Name");

        var readUser = repo.GetUser(1);
        Assert.That(readUser, Is.Not.Null);
        Assert.That(readUser.DisplayName, Is.EqualTo("New Display Name"));  
    }

    private User GetBasicUserWithoutId() {
        return new User {
            Username = "TestUser", 
            DisplayName = "Test User",
            Description = "Unit Test User",
            CreatedAt = new DateTime(2025, 04, 01, 12, 0, 0, DateTimeKind.Utc),
            PasswordHashVersion = 1,
            PasswordHash = "xlkt",
            PasswordUpdatedAt = new DateTime(2025, 04, 01, 12, 0, 0, DateTimeKind.Utc),
            LastLoginAt = new DateTime(2025, 04, 01, 12, 0, 0, DateTimeKind.Utc)
        };
    }

    private void AssertUserFieldsEqualExceptId(User user, User readUser) {
        Assert.That(user.Username, Is.EqualTo(readUser.Username));
        Assert.That(user.DisplayName, Is.EqualTo(readUser.DisplayName));
        Assert.That(user.Description, Is.EqualTo(readUser.Description));
        Assert.That(user.CreatedAt, Is.EqualTo(readUser.CreatedAt));
        Assert.That(user.PasswordHashVersion, Is.EqualTo(readUser.PasswordHashVersion));
        Assert.That(user.PasswordUpdatedAt, Is.EqualTo(readUser.PasswordUpdatedAt));
        Assert.That(user.LastLoginAt, Is.EqualTo(readUser.LastLoginAt));  
    }
}