namespace PTS.Entity.DAL;

using PTS.Entity.Domain;
using Microsoft.Data.Sqlite;
using PTS.Entity.Util;

public class TagRepository {
    private SqliteConnection _connection;

    public TagRepository(SqliteConnection connection) {
        _connection = connection;
    }

    public void AddTag(Tag tag) {
        var insertCmd = _connection.CreateCommand();
        insertCmd.CommandText = 
        @"INSERT INTO Tags (
            Name,
            CreatedAt)
          VALUES (
           $name,
           $createdAt)";

        insertCmd.Parameters.AddWithValue("$name", tag.Name);
        insertCmd.Parameters.AddWithValue("$createdAt", DateTimeConverter.ToUnix(tag.CreatedAt));

        insertCmd.ExecuteNonQuery();
    }

    public void DeleteTag(long tagId) {
        var insertCmd = _connection.CreateCommand();
        insertCmd.CommandText = 
        @"DELETE FROM Tags 
          WHERE Id=$tagId
          ";

        insertCmd.Parameters.AddWithValue("$tagId", tagId);

        insertCmd.ExecuteNonQuery();         
    }

    public Tag GetTagById(long tagId) {
        var selectCmd = _connection.CreateCommand();
        selectCmd.CommandText = 
        @"SELECT Id, 
            Name,
            CreatedAt
          FROM Tags
          WHERE Id=$tagId";

        selectCmd.Parameters.AddWithValue("$tagId", tagId);

        using var reader = selectCmd.ExecuteReader();
        
        Tag tag = ReadTagFromReader(reader);

        return tag; 
    }

    public int GetTagCount() {
        var selectCmd = _connection.CreateCommand();
        selectCmd.CommandText = 
        @"SELECT COUNT(*) FROM Tags";

        using var reader = selectCmd.ExecuteReader();
        while (reader.Read())
        {
            return reader.GetInt32(0);
        }
        throw new InvalidOperationException("Could not get count of tags");
    }

    public List<Tag> GetAllTags() {
        var selectCmd = _connection.CreateCommand();
        selectCmd.CommandText = 
        @"SELECT Id, 
            Name,
            CreatedAt
          FROM Tags";

        using var reader = selectCmd.ExecuteReader();
        
        List<Tag> tags = ReadTagsFromReader(reader);

        return tags; 
    }

    private Tag ReadTagFromReader(SqliteDataReader reader) {
        reader.Read();
        return ReadTagWithoutAdvance(reader);
    }

    private List<Tag> ReadTagsFromReader(SqliteDataReader reader) {
        List<Tag> tags = new List<Tag>();
        while (reader.Read()) {
            tags.Add(ReadTagWithoutAdvance(reader));
        }

        return tags;
    }

    private Tag ReadTagWithoutAdvance(SqliteDataReader reader) {
        var tag = new Tag {
            Id = (long)reader["Id"],
            Name = (string)reader["Name"],
            CreatedAt = DateTimeConverter.FromUnix((long)reader["CreatedAt"])
        };
        return tag;
    }    
}