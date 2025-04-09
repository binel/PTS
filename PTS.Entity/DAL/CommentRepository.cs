namespace PTS.Entity.DAL;

using PTS.Entity.Domain;
using Microsoft.Data.Sqlite;
using PTS.Entity.Util;

public class CommentRepository {
    private SqliteConnection _connection;

    public CommentRepository(Database database) {
        _connection = database.GetConnection();
    }

    public void AddComment(Comment comment) {
        var insertCmd = _connection.CreateCommand();
        insertCmd.CommandText = 
        @"INSERT INTO Comments (
            AuthorKey, 
            TicketKey,
            Content,
            CreatedAt,
            UpdatedAt)
          VALUES (
           $authorKey,
           $ticketKey,
           $content,
           $createdAt,
           $updatedAt)";

        insertCmd.Parameters.AddWithValue("$authorKey", comment.AuthorId);
        insertCmd.Parameters.AddWithValue("$ticketKey", comment.TicketId);
        insertCmd.Parameters.AddWithValue("$content", comment.Content);
        insertCmd.Parameters.AddWithValue("$createdAt", DateTimeConverter.ToUnix(comment.CreatedAt));
        insertCmd.Parameters.AddWithValue("$updatedAt", DateTimeConverter.ToUnix(comment.UpdatedAt));

        insertCmd.ExecuteNonQuery();
    }

    public void DeleteComment(int commentId) {
        var insertCmd = _connection.CreateCommand();
        insertCmd.CommandText = 
        @"DELETE FROM Comments 
          WHERE Id=$commentId
          ";

        DateTime updateTime = DateTime.UtcNow;

        insertCmd.Parameters.AddWithValue("$commentId", commentId);

        insertCmd.ExecuteNonQuery(); 
    }

    public void UpdateComment(int commentId, string text) {
        var insertCmd = _connection.CreateCommand();
        insertCmd.CommandText = 
        @"UPDATE Comments 
          SET Content = $content,
          UpdatedAt = $updatedAt
          WHERE Id=$commentId
          ";

        DateTime updateTime = DateTime.UtcNow;

        insertCmd.Parameters.AddWithValue("$content", text);
        insertCmd.Parameters.AddWithValue("$commentId", commentId);
        insertCmd.Parameters.AddWithValue("$updatedAt", DateTimeConverter.ToUnix(DateTime.UtcNow));

        insertCmd.ExecuteNonQuery();  
    }

    public Comment GetCommentById(int commentId) {
        var selectCmd = _connection.CreateCommand();
        selectCmd.CommandText = 
        @"SELECT Id, 
            AuthorKey,
            TicketKey,
            Content,
            CreatedAt,
            UpdatedAt
          FROM Comments
          WHERE Id=$commentId";

        selectCmd.Parameters.AddWithValue("$commentId", commentId);

        using var reader = selectCmd.ExecuteReader();
        
        Comment comment = ReadCommentFromReader(reader);

        return comment; 
    }

    public int GetCountComments() {
        var selectCmd = _connection.CreateCommand();
        selectCmd.CommandText = 
        @"SELECT COUNT(*) FROM Comments";

        using var reader = selectCmd.ExecuteReader();
        while (reader.Read())
        {
            return reader.GetInt32(0);
        }
        throw new InvalidOperationException("Could not get count of comments");     
    }

    public List<Comment> GetCommentsForTicket(int ticketId) {
        var selectCmd = _connection.CreateCommand();
        selectCmd.CommandText = 
        @"SELECT Id, 
            AuthorKey,
            TicketKey,
            Content,
            CreatedAt,
            UpdatedAt
          FROM Comments
          WHERE TicketKey=$ticketId";

        selectCmd.Parameters.AddWithValue("$ticketId", ticketId);

        using var reader = selectCmd.ExecuteReader();
        
        List<Comment> comments = ReadCommentsFromReader(reader);

        return comments; 
    }

    private Comment ReadCommentFromReader(SqliteDataReader reader) {
        reader.Read();
        return ReadCommentWithoutAdvance(reader);
    }

    private List<Comment> ReadCommentsFromReader(SqliteDataReader reader) {
        List<Comment> comments = new List<Comment>();
        while (reader.Read()) {
            comments.Add(ReadCommentWithoutAdvance(reader));
        }

        return comments;
    }

    private Comment ReadCommentWithoutAdvance(SqliteDataReader reader) {
        var comment = new Comment {
            Id = (long)reader["Id"],
            AuthorId = (long)reader["AuthorKey"],
            TicketId = (long)reader["TicketKey"],
            Content = (string)reader["Content"],
            CreatedAt = DateTimeConverter.FromUnix((long)reader["CreatedAt"]),
            UpdatedAt = DateTimeConverter.FromUnix((long)reader["UpdatedAt"])
        };

        return comment;
    }    
}