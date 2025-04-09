namespace PTS.Entity.DAL;

using PTS.Entity.Domain;
using Microsoft.Data.Sqlite;
using PTS.Entity.Util;

public class TagMappingRepository {
    private SqliteConnection _connection;

    public TagMappingRepository(Database database) {
        _connection = database.GetConnection();
    }

    public void AddTagAssociation(long ticketId, long tagId) {
        var insertCmd = _connection.CreateCommand();
        insertCmd.CommandText = 
        @"INSERT INTO TagMapping (
            TicketKey,
            TagKey,
            CreatedAt)
          VALUES (
           $ticketKey,
           $tagKey,
           $createdAt)";

        insertCmd.Parameters.AddWithValue("$ticketKey", ticketId);
        insertCmd.Parameters.AddWithValue("$tagKey", tagId);
        insertCmd.Parameters.AddWithValue("$createdAt", DateTimeConverter.ToUnix(DateTime.UtcNow));

        insertCmd.ExecuteNonQuery();
    }

    public void RemoveTagAssociation(long ticketId, long tagId) {
       var insertCmd = _connection.CreateCommand();
        insertCmd.CommandText = 
        @"DELETE FROM TagMapping
          WHERE TicketKey=$ticketId
          AND TagKey=$tagId
          ";

        insertCmd.Parameters.AddWithValue("$ticketId", ticketId);
        insertCmd.Parameters.AddWithValue("$tagId", tagId);

        insertCmd.ExecuteNonQuery();  
    }

    public List<long> GetTagsOnTicket(long ticketId) {
        var selectCmd = _connection.CreateCommand();
        selectCmd.CommandText = 
        @"SELECT TagKey
          FROM TagMapping
          WHERE TicketKey=$ticketId";
        
        selectCmd.Parameters.AddWithValue("$ticketId", ticketId);

        List<long> tagIds = new List<long>();
        using var reader = selectCmd.ExecuteReader();
        while (reader.Read()) {
            tagIds.Add((long)reader["TagKey"]);
        }

        return tagIds;
    }

    public int GetTagAssociationCount() {
        var selectCmd = _connection.CreateCommand();
        selectCmd.CommandText = 
        @"SELECT COUNT(*) FROM TagMapping";

        using var reader = selectCmd.ExecuteReader();
        while (reader.Read())
        {
            return reader.GetInt32(0);
        }
        throw new InvalidOperationException("Could not get count of tag mappings");
    }
}