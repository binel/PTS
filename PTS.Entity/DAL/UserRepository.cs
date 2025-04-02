namespace PTS.Entity.DAL;

using PTS.Entity.Domain;
using PTS.Entity.Util;
using Microsoft.Data.Sqlite;

public class UserRepository {

    private SqliteConnection _connection; 

    private const string ID_COL = "Id";
    private const string USERNAME_COL = "Username";
    private const string DISPLAY_NAME_COL = "DisplayName";
    private const string DESCRIPTION_COL = "Description";
    private const string CREATED_AT_COL = "CreatedAt";
    private const string PASSWORD_HASH_VERSION_COL = "PasswordHashVersion";
    private const string PASSWORD_HASH_COL = "PasswordHash";
    private const string PASSWORD_UPDATED_AT_COL = "PasswordUpdatedAt";
    private const string LAST_LOGIN_AT_COL = "LastLoginAt";

    public UserRepository(SqliteConnection connection) {
        _connection = connection;
    }

    public void AddUser(User user) {
        var insertCmd = _connection.CreateCommand();
        insertCmd.CommandText = 
        @"INSERT INTO Users (
            Username, 
            DisplayName,
            Description,
            CreatedAt,
            PasswordHashVersion,
            PasswordHash,
            PasswordUpdatedAt,
            LastLoginAt)
          VALUES (
           $username,
           $displayName,
           $description,
           $createdAt,
           $passwordHashVersion,
           $passwordHash,
           $passwordUpdatedAt,
           $lastLoginAt)";

        insertCmd.Parameters.AddWithValue("$username", user.Username);
        insertCmd.Parameters.AddWithValue("$displayName", user.DisplayName);
        insertCmd.Parameters.AddWithValue("$description", user.Description);
        insertCmd.Parameters.AddWithValue("$createdAt", DateTimeConverter.ToUnix(user.CreatedAt));
        insertCmd.Parameters.AddWithValue("$passwordHashVersion", user.PasswordHashVersion);
        insertCmd.Parameters.AddWithValue("$passwordHash", user.PasswordHash);
        insertCmd.Parameters.AddWithValue("$passwordUpdatedAt", DateTimeConverter.ToUnix(user.PasswordUpdatedAt));
        insertCmd.Parameters.AddWithValue("$lastLoginAt", DateTimeConverter.ToUnix(user.LastLoginAt));

        insertCmd.ExecuteNonQuery();
    }

    public User GetUser(int userId) {
        var selectCmd = _connection.CreateCommand();
        selectCmd.CommandText = 
        @"SELECT Id, 
            Username,
            DisplayName,
            Description,
            CreatedAt,
            PasswordHashVersion,
            PasswordHash,
            PasswordUpdatedAt,
            LastLoginAt
          FROM Users
          WHERE Id=$userId";

        selectCmd.Parameters.AddWithValue("$userId", userId);

        using var reader = selectCmd.ExecuteReader();
        
        User user = ReadUserFromReader(reader);

        return user; 
    }

    public int GetUserCount() {
        var selectCmd = _connection.CreateCommand();
        selectCmd.CommandText = 
        @"SELECT COUNT(*) FROM Users";

        using var reader = selectCmd.ExecuteReader();
        while (reader.Read())
        {
            return reader.GetInt32(0);
        }
        throw new InvalidOperationException("Could not get count of users");
    }

    public List<User> GetAllUsers() {
        var selectCmd = _connection.CreateCommand();
        selectCmd.CommandText = 
        @"SELECT Id, 
            Username,
            DisplayName,
            Description,
            CreatedAt,
            PasswordHashVersion,
            PasswordHash,
            PasswordUpdatedAt,
            LastLoginAt
          FROM Users";

        using var reader = selectCmd.ExecuteReader();

        var users = ReadUsersFromReader(reader);

        return users; 
    }

    public User GetUserByUsername(string username) {
        var selectCmd = _connection.CreateCommand();
        selectCmd.CommandText = 
        @"SELECT Id, 
            Username,
            DisplayName,
            Description,
            CreatedAt,
            PasswordHashVersion,
            PasswordHash,
            PasswordUpdatedAt,
            LastLoginAt
          FROM Users
          WHERE Username=$username";

        selectCmd.Parameters.AddWithValue("$username", username);

        using var reader = selectCmd.ExecuteReader();

        var user = ReadUserFromReader(reader);

        return user; 
    }

    public void SetUserPassword(int userId, string hash, long hashVersion) {
        var insertCmd = _connection.CreateCommand();
        insertCmd.CommandText = 
        @"UPDATE Users 
          SET PasswordHash = $passwordHash,
            PasswordHashVersion = $passwordHashVersion,
            PasswordUpdatedAt = $passwordUpdatedAt
          WHERE Id=$userId
          ";

        DateTime updateTime = DateTime.UtcNow;

        insertCmd.Parameters.AddWithValue("$userId", userId);
        insertCmd.Parameters.AddWithValue("$passwordHashVersion", hashVersion);
        insertCmd.Parameters.AddWithValue("$passwordHash", hash);
        insertCmd.Parameters.AddWithValue("$passwordUpdatedAt", DateTimeConverter.ToUnix(updateTime));

        insertCmd.ExecuteNonQuery();        
    }

    public void SetUserLastLoginToNow(int userId) {
        var insertCmd = _connection.CreateCommand();
        insertCmd.CommandText = 
        @"UPDATE Users 
          SET LastLoginAt = $lastLoginAt
          WHERE Id=$userId
          ";

        DateTime updateTime = DateTime.UtcNow;

        insertCmd.Parameters.AddWithValue("$userId", userId);
        insertCmd.Parameters.AddWithValue("$lastLoginAt", DateTimeConverter.ToUnix(updateTime));

        insertCmd.ExecuteNonQuery();  
    }

    public void SetUserDisplayName(int userId, string displayName) {
        var insertCmd = _connection.CreateCommand();
        insertCmd.CommandText = 
        @"UPDATE Users 
          SET DisplayName = $displayName
          WHERE Id=$userId
          ";

        DateTime updateTime = DateTime.UtcNow;

        insertCmd.Parameters.AddWithValue("$userId", userId);
        insertCmd.Parameters.AddWithValue("$displayName", displayName);

        insertCmd.ExecuteNonQuery();  
    }

    public void SetUserUsername(int userId, string username) {
        var insertCmd = _connection.CreateCommand();
        insertCmd.CommandText = 
        @"UPDATE Users 
          SET Username = $username
          WHERE Id=$userId
          ";

        DateTime updateTime = DateTime.UtcNow;

        insertCmd.Parameters.AddWithValue("$userId", userId);
        insertCmd.Parameters.AddWithValue("$username", username);

        insertCmd.ExecuteNonQuery();  
    }

    private User ReadUserFromReader(SqliteDataReader reader) {
        reader.Read();
        return ReadUserWithoutAdvance(reader);
    }

    private List<User> ReadUsersFromReader(SqliteDataReader reader) {
        List<User> users = new List<User>();
        while (reader.Read()) {
            users.Add(ReadUserWithoutAdvance(reader));
        }

        return users;
    }

    private User ReadUserWithoutAdvance(SqliteDataReader reader) {
        var user = new User {
            Id = (long)reader[ID_COL],
            Username = (string)reader[USERNAME_COL],
            DisplayName = (string)reader[DISPLAY_NAME_COL],
            Description = (string)reader[DESCRIPTION_COL],
            CreatedAt = DateTimeConverter.FromUnix((long)reader[CREATED_AT_COL]),
            PasswordHashVersion = (long)reader[PASSWORD_HASH_VERSION_COL],
            PasswordHash = (string)reader[PASSWORD_HASH_COL],
            PasswordUpdatedAt = DateTimeConverter.FromUnix((long)reader[PASSWORD_UPDATED_AT_COL]),
            LastLoginAt = DateTimeConverter.FromUnix((long)reader[LAST_LOGIN_AT_COL])
        };
        return user;
    }
}