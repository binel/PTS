namespace PTS.Entity.DAL;

using Microsoft.Data.Sqlite;

public class DatabaseCreator {
    
    private SqliteConnection _connection;

    public DatabaseCreator(SqliteConnection connection) {
        _connection = connection;
    }

    /*
    * Version Summaries 
    *
    * Version 1: Initial Version 
    */ 
    public const int MIN_DB_VERSION = 1;
    public const int CURRENT_DB_VERSION = 1; 

    public void CreateDatabase(int version) {
        if (version < MIN_DB_VERSION) {
            throw new ArgumentException($"Cannot create database with version less than {MIN_DB_VERSION}");
        }

        if (version > CURRENT_DB_VERSION) {
            throw new ArgumentException($"Cannot create database with version greater than {CURRENT_DB_VERSION}");
        }

        CreateTicketTable(version);

        CreateUserTable(version);
    }

    public static void CreateMetadataTable(int version) {
        throw new NotImplementedException();
    }

    public void CreateTicketTable(int version) {
        var tableCmd = _connection.CreateCommand();

        tableCmd.CommandText = 
        @"
        CREATE TABLE IF NOT EXISTS Tickets (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Identifier TEXT NOT NULL,
            Title TEXT NOT NULL,
            Description TEXT NOT NULL,
            Priority INTEGER NOT NULL,
            AuthorKey INTEGER NOT NULL,
            Status INTEGER NOT NULL,
            HasComments INTEGER NOT NULL,
            HasRelationships INTEGER NOT NULL,
            HasTags INTEGER NOT NULL,
            HasWorkHistory INTEGER NOT NULL,
            ProjectKey INTEGER,
            CreatedAt INTEGER NOT NULL,
            UpdatedAt INTEGER NOT NULL,
            ResolvedAt INTEGER NOT NULL
        )";

        tableCmd.ExecuteNonQuery();
    }

    public static void CreateCommentTable(int version) {
        throw new NotImplementedException();
    }

    public static void CreateProjectTable(int version) {
        throw new NotImplementedException();
    }

    public static void CreateProjectMappingTable(int version) {
        throw new NotImplementedException();
    }

    public static void CreateTagTable(int version) {
        throw new NotImplementedException();
    }

    public static void CreateTagMappingTable(int version) {
        throw new NotImplementedException();
    }

    public static void CreateStatusHistoryTable(int version) {
        throw new NotImplementedException();
    }

    public void CreateUserTable(int version) {
      var tableCmd = _connection.CreateCommand();

        tableCmd.CommandText = 
        @"
        CREATE TABLE IF NOT EXISTS Users (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Username TEXT NOT NULL,
            DisplayName TEXT NOT NULL,
            Description TEXT,
            CreatedAt INTEGER NOT NULL,
            PasswordHashVersion INTEGER NOT NULL,
            PasswordHash TEXT NOT NULL,
            PasswordUpdatedAt INTEGER NOT NULL,
            LastLoginAt INTEGER
        )";

        tableCmd.ExecuteNonQuery();
    }

    public static void CreateWorkHistoryTable(int version) {
        throw new NotImplementedException();
    }

    public static void CreateRelationshipTable(int version) {
        throw new NotImplementedException();
    }

    public static void CreateRelationshipMappingTable(int version) {
        throw new NotImplementedException();
    }

}