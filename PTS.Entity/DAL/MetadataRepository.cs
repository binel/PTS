namespace PTS.Entity.DAL;

using Microsoft.Data.Sqlite;

public class MetadataRepository {
    private SqliteConnection _connection;

    public MetadataRepository(SqliteConnection connection) {
        _connection = connection;
    }

    public int GetDbVersion() {
        var selectCmd = _connection.CreateCommand();
        selectCmd.CommandText = 
        @"SELECT Value
          FROM Metadata
          WHERE Key=$key";

        selectCmd.Parameters.AddWithValue("$key", "db_version");

        using var reader = selectCmd.ExecuteReader();
        
        reader.Read();

        string dbVersion = (string)reader["Value"];

        return int.Parse(dbVersion);         
    }
}