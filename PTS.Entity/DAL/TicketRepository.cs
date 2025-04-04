namespace PTS.Entity.DAL;

using PTS.Entity.Domain;
using Microsoft.Data.Sqlite;
using PTS.Entity.Util;

public class TicketRepository {
    // TODO complete 

    private SqliteConnection _connection;

    public TicketRepository(SqliteConnection connection) {
        _connection = connection;
    }

    public void AddTicket(Ticket ticket) {
        var insertCmd = _connection.CreateCommand();
        insertCmd.CommandText = 
        @"INSERT INTO Tickets (
            Identifier, 
            Title,
            Description,
            Priority,
            AuthorKey,
            Status,
            HasComments,
            HasRelationships,
            HasTags,
            HasWorkHistory,
            ProjectKey,
            CreatedAt,
            UpdatedAt,
            ResolvedAt)
          VALUES (
           $identifier,
           $title,
           $description,
           $priority,
           $authorKey,
           $status,
           $hasComments,
           $hasRelationships,
           $hasTags,
           $hasWorkHistory,
           $projectKey,
           $createdAt,
           $updatedAt,
           $resolvedAt)";

        insertCmd.Parameters.AddWithValue("$identifier", ticket.Identifier);
        insertCmd.Parameters.AddWithValue("$title", ticket.Title);
        insertCmd.Parameters.AddWithValue("$description", ticket.Description);
        insertCmd.Parameters.AddWithValue("$priority", (int)ticket.Priority);
        insertCmd.Parameters.AddWithValue("$authorKey", ticket.Author.Id);
        insertCmd.Parameters.AddWithValue("$status", (int)ticket.Status);
        insertCmd.Parameters.AddWithValue("$hasComments", ticket.Comments.Count() > 0 ? 1 : 0);
        insertCmd.Parameters.AddWithValue("$hasRelationships", ticket.Relationships.Count() > 0 ? 1 : 0);
        insertCmd.Parameters.AddWithValue("$hasTags", ticket.Tags.Count() > 0 ? 1 : 0);
        insertCmd.Parameters.AddWithValue("$hasWorkHistory", ticket.WorkHistory.Count() > 0 ? 1 : 0);

        if (ticket.Project != null) {
            insertCmd.Parameters.AddWithValue("$projectKey", ticket.Project.Id);
        } else {
            insertCmd.Parameters.AddWithValue("$projectKey", null);
        }


        insertCmd.Parameters.AddWithValue("$createdAt", DateTimeConverter.ToUnix(ticket.CreatedAt));
        insertCmd.Parameters.AddWithValue("$updatedAt", DateTimeConverter.ToUnix(ticket.UpdatedAt));

        if (ticket.ResolvedAt.HasValue) {
            insertCmd.Parameters.AddWithValue("$resolvedAt", DateTimeConverter.ToUnix(ticket.ResolvedAt.Value));
        } else {
            insertCmd.Parameters.AddWithValue("$resolvedAt", null);         
        }

        try {
            insertCmd.ExecuteNonQuery();
        } catch (Exception e) {
            SqliteUtils.PrintInsertCommandParametersForError(e, insertCmd);
            SqliteUtils.PrintTableSchema(_connection, "Tickets");
            throw;      
        }
        
    }

    public int GetTicketCount() {
        var selectCmd = _connection.CreateCommand();
        selectCmd.CommandText = 
        @"SELECT COUNT(*) FROM Tickets";

        using var reader = selectCmd.ExecuteReader();
        while (reader.Read())
        {
            return reader.GetInt32(0);
        }
        throw new InvalidOperationException("Could not get count of tickets");       
    }

    public void UpdateTicketTitle(int id, string title) {
        throw new NotImplementedException();
    }

    public void UpdateTicketDescription(int id, string description) {
        throw new NotImplementedException();
    }

    public void UpdatePriority(int id, Priority priority) {
        throw new NotImplementedException();
    }

    public void AddComment(int ticketId, Comment comment) {
        throw new NotImplementedException();
    }

    public void DeleteComment(int ticketId, int commentId) {
        throw new NotImplementedException();
    }

    public void AddRelationship(int fromTicketId, int toTicketId) {
        throw new NotImplementedException();
    }

    public void AddTag(int ticketId, Tag tag) {
        throw new NotImplementedException();
    }

    public void RemoveTag(int ticketId, int tagId) {
        throw new NotImplementedException();
    }

    public void AddWorkHistory(int ticketId, WorkHistory workHistory) {
        throw new NotImplementedException();
    }

    public void RemoveWorkHistory(int ticketId, int workHistoryId) {
        throw new NotImplementedException();
    }

    public void AssociateWithProject(int ticketId, int projectId) {
        throw new NotImplementedException();
    }

    public void RemoveProjectAssociation(int ticketId, int projectId) {
        throw new NotImplementedException();
    }

    public string GetNextIdentifierForTicketInProject(int projectKey) {
        // Might not need this, maybe automatically set when ticket created?
        // Need to think more about how projects will work, b/c then removing 
        // project association would be weird 
        throw new NotImplementedException();
    }

    public void UpdateTicketStatus(int ticketId, Status status) {
        throw new NotImplementedException();
    }
}