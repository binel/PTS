namespace PTS.Entity.DAL;

public static class DatabaseCreator {
    const int MIN_DB_VERSION = 1;
    const int MAX_DB_VERSION = 1; 

    public static void CreateDatabase(int version) {
        if (version < MIN_DB_VERSION) {
            throw new ArgumentException($"Cannot create database with version less than {MIN_DB_VERSION}");
        }

        if (version > MAX_DB_VERSION) {
            throw new ArgumentException($"Cannot create database with version greater than {MAX_DB_VERSION}");
        }

        CreateTicketTable(version);
    }

    public static void CreateTicketTable(int version) {
        Database.GetConnection().Open();
        var tableCmd = Database.GetConnection().CreateCommand();

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
}