using System;

namespace PTS.Installer;

using PTS.Entity.DAL;
using PTS.Entity.Domain;
using PTS.Core;

internal class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0) {
            Console.WriteLine("Database connection string is required");
        }

        Database db = new Database(args[0]);
        DatabaseCreator creator = new DatabaseCreator(db);
        creator.CreateDatabase(1);


        CreateTestUser(db);
    }

    static void PrintUsage() {
        Console.WriteLine("Usage: PTS.Insaller <db connection string>");
    }

    static void CreateTestUser(Database db) {
        UserRepository userRepository = new UserRepository(db);

        var hash = PasswordHash.Encrypt("autotest", 1);

        User user = new User {
            Username = "Test",
            DisplayName = "Test",
            Description = "User for automated tests",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            PasswordHashVersion = 1,
            PasswordHash = hash,
            PasswordUpdatedAt = DateTime.UtcNow,
            LastLoginAt = DateTime.UtcNow
        };

        userRepository.AddUser(user);
    }
}