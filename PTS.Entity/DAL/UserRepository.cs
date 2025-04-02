namespace PTS.Entity.DAL;

using PTS.Entity.Domain;
using PTS.Entity.Util;
using Microsoft.Data.Sqlite;

public class UserRepository {

    private SqliteConnection _connection; 

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
        bool read = false;
        User user = null;
        while (reader.Read())
        {
            if (read == false) {
                read = true;
            }
            else {
                throw new InvalidOperationException($"Multiple users exist with user Id {userId}");
            }

            user = new User {
                Id = reader.GetInt32(0),
                Username = reader.GetString(1),
                Description = reader.GetString(2),
                CreatedAt = DateTimeConverter.FromUnix(reader.GetInt32(3)),
                PasswordHashVersion = reader.GetInt32(4),
                PasswordHash = reader.GetString(5),
                PasswordUpdatedAt = DateTimeConverter.FromUnix(reader.GetInt32(6)),
                LastLoginAt = DateTimeConverter.FromUnix(reader.GetInt32(7))
            };
        }

        return user; 
    }

    public List<User> GetAllUsers() {
        throw new NotImplementedException();
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
        bool read = false;
        User user = null;
        while (reader.Read())
        {
            if (read == false) {
                read = true;
            }
            else {
                throw new InvalidOperationException($"Multiple users exist with username {username}");
            }

            user = new User {
                Id = reader.GetInt32(0),
                Username = reader.GetString(1),
                Description = reader.GetString(2),
                CreatedAt = DateTimeConverter.FromUnix(reader.GetInt32(3)),
                PasswordHashVersion = reader.GetInt32(4),
                PasswordHash = reader.GetString(5),
                PasswordUpdatedAt = DateTimeConverter.FromUnix(reader.GetInt32(6)),
                LastLoginAt = DateTimeConverter.FromUnix(reader.GetInt32(7))
            };
        }

        return user; 
    }

    public void SetUserPassword(int userId, string hash) {
        throw new NotImplementedException(); 
    }

    public void SetUserLastLoginToNow(int userId) {
        throw new NotImplementedException();
    }

    public void SetUserDisplayName(int userId, string displayName) {
        throw new NotImplementedException();
    }

    public void SetUserUsername(int userId, string username) {
        throw new NotImplementedException();
    }

    public void DeleteUser(int userId) {
        throw new NotImplementedException();
    }
}