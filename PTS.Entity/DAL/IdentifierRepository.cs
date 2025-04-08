namespace PTS.Entity.DAL;

using PTS.Entity.Domain;
using Microsoft.Data.Sqlite;
using PTS.Entity.Util;

public class IdentifierRepository {
    private SqliteConnection _connection;

    public IdentifierRepository(SqliteConnection connection) {
        _connection = connection;
    }

    public void AddIdentifier(Identifier identifier) {
        var insertCmd = _connection.CreateCommand();
        insertCmd.CommandText = 
        @"INSERT INTO Identifiers (
            Text,
            HighestValue,
            CreatedAt,
            UpdatedAt)
          VALUES (
           $text,
           $highestValue,
           $createdAt,
           $updatedAt)";

        insertCmd.Parameters.AddWithValue("$text", identifier.Text);
        insertCmd.Parameters.AddWithValue("$highestValue", identifier.HighestValue);
        insertCmd.Parameters.AddWithValue("$createdAt", DateTimeConverter.ToUnix(identifier.CreatedAt));
        insertCmd.Parameters.AddWithValue("$updatedAt", DateTimeConverter.ToUnix(identifier.UpdatedAt));

        insertCmd.ExecuteNonQuery();
    }

    public List<Identifier> GetAllIdentifiers() {
        var selectCmd = _connection.CreateCommand();
        selectCmd.CommandText = 
        @"SELECT Id, 
            Text,
            HighestValue,
            CreatedAt,
            UpdatedAt
          FROM Identifiers";

        using var reader = selectCmd.ExecuteReader();
        
        List<Identifier> identifiers = ReadIdentifiersFromReader(reader);

        return identifiers; 
    }

    public void UpdateHighestValue(long identifierId, long value) {
        var insertCmd = _connection.CreateCommand();
        insertCmd.CommandText = 
        @"UPDATE Identifiers 
          SET UpdatedAt = $updatedAt,
          HighestValue = $highestValue,
          WHERE Id=$identifierId
          ";

        DateTime updateTime = DateTime.UtcNow;

        insertCmd.Parameters.AddWithValue("$highestValue", value);
        insertCmd.Parameters.AddWithValue($"identifierId", identifierId);
        insertCmd.Parameters.AddWithValue("$updatedAt", DateTimeConverter.ToUnix(DateTime.UtcNow));

        insertCmd.ExecuteNonQuery(); 
    }


    private Identifier ReadIdentifierFromReader(SqliteDataReader reader) {
        reader.Read();
        return ReadIdentifierWithoutAdvance(reader);
    }

    private List<Identifier> ReadIdentifiersFromReader(SqliteDataReader reader) {
        List<Identifier> identifiers = new List<Identifier>();
        while (reader.Read()) {
            identifiers.Add(ReadIdentifierWithoutAdvance(reader));
        }

        return identifiers;
    }

    private Identifier ReadIdentifierWithoutAdvance(SqliteDataReader reader) {
        var identifier = new Identifier {
            Id = (long)reader["Id"],
            Text = (string)reader["Text"],
            HighestValue = (long)reader["HighestValue"],
            CreatedAt = DateTimeConverter.FromUnix((long)reader["CreatedAt"]),
            UpdatedAt = DateTimeConverter.FromUnix((long)reader["UpdatedAt"])
        };

        return identifier;
    }   

}