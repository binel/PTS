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
        CreateCommentTable(version);
        CreateTagTable(version);
        CreateTagMappingTable(version);
        CreateUserTable(version);
    }

    public void CreateMetadataTable(int version) {
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
            ProjectKey INTEGER,
            CreatedAt INTEGER NOT NULL,
            UpdatedAt INTEGER NOT NULL,
            ResolvedAt INTEGER
        )";

        tableCmd.ExecuteNonQuery();
    }

    public void CreateCommentTable(int version) {
        var tableCmd = _connection.CreateCommand();

        tableCmd.CommandText = 
        @"
        CREATE TABLE IF NOT EXISTS Comments (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            AuthorKey INTEGER NOT NULL,
            TicketKey INTEGER NOT NULL,
            Content TEXT NOT NULL,
            CreatedAt INTEGER NOT NULL,
            UpdatedAt INTEGER NOT NULL
        )";

        tableCmd.ExecuteNonQuery();
    }

    public void CreateProjectTable(int version) {
        throw new NotImplementedException();
    }

    public void CreateTagTable(int version) {
      var tableCmd = _connection.CreateCommand();

        tableCmd.CommandText = 
        @"
        CREATE TABLE IF NOT EXISTS Tags (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Name TEXT NOT NULL,
            CreatedAt INTEGER NOT NULL
        )";

        tableCmd.ExecuteNonQuery();
    }

    public void CreateTagMappingTable(int version) {
      var tableCmd = _connection.CreateCommand();

        tableCmd.CommandText = 
        @"
        CREATE TABLE IF NOT EXISTS TagMapping (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            TicketKey INTEGER NOT NULL,
            TagKey INTEGER NOT NULL,
            CreatedAt INTEGER NOT NULL
        )";

        tableCmd.ExecuteNonQuery();
    }

    public void CreateStatusHistoryTable(int version) {
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

    public void CreateWorkHistoryTable(int version) {
        throw new NotImplementedException();
    }
}