namespace PTS.Entity.DAL;

using PTS.Entity.Domain;
using Microsoft.Data.Sqlite;
using PTS.Entity.Util;

public class TicketRepository {
    // TODO complete 

    private class TicketDto {
        public long Id {get; set;}
        public string Identifier {get; set;}
        public string Title {get; set;}
        public string Description {get; set;}
        public Priority Priority {get; set;}
        public long AuthorKey {get; set;}
        public Status Status {get; set;}
        public long HasComments {get; set;}
        public long HasRelationships {get; set;}
        public long HasTags {get; set;}
        public long HasWorkHistory {get; set;}
        public long? ProjectKey {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}
        public DateTime? ResolvedAt {get; set;}

        public Ticket ToTicket() {
            Ticket ticket =  new Ticket {
                Id = Id,
                Identifier = Identifier,
                Title = Title,
                Description = Description,
                Priority = Priority,
                Author = new User {
                    Id = AuthorKey,
                    // TODO resolve 
                },
                Status = Status,
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt,
                ResolvedAt = ResolvedAt
            };

            if (HasComments == 1) {
                // TODO resolve 
            }

            if (HasRelationships == 1) {
                // TODO resolve 
            }

            if (HasTags == 1) {
                // TODO resolve 
            }

            if (HasWorkHistory == 1) {
                // TODO resolve
            }

            if (ProjectKey != null) {
                // TODO resolve 
            }

            return ticket;
        }
    }

    private SqliteConnection _connection;

    public TicketRepository(SqliteConnection connection) {
        _connection = connection;
    }

    public void AddTicket(Ticket ticket) {
        var insertCmd = _connection.CreateCommand();
        
        if (ticket.Project == null && ticket.ResolvedAt == null) {
            insertCmd.CommandText = GetInsertCommand();
        }
        else if (ticket.Project != null && ticket.ResolvedAt == null) {
            insertCmd.CommandText = GetInsertCommandWithProjectKey();
        }
        else if (ticket.Project == null && ticket.ResolvedAt != null) {
            insertCmd.CommandText = GetInsertCommandWithResolvedAt();
        }
        else {
            insertCmd.CommandText = GetInsertCommandWithAllColumns();
        }

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

    private string GetInsertCommand() {
        return @"INSERT INTO Tickets (
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
            CreatedAt,
            UpdatedAt)
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
           $createdAt,
           $updatedAt)";
    }

    private string GetInsertCommandWithProjectKey() {
        return @"INSERT INTO Tickets (
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
            UpdatedAt)
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
           $updatedAt)";
    }

    private string GetInsertCommandWithResolvedAt() {
        return @"INSERT INTO Tickets (
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
           $createdAt,
           $updatedAt,
           $resolvedAt)";
    }

    private string GetInsertCommandWithAllColumns() {
        return @"INSERT INTO Tickets (
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

    public Ticket GetTicketById(int id) {
        var selectCmd = _connection.CreateCommand();
        selectCmd.CommandText = 
        @"SELECT Id, 
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
            ResolvedAt
          FROM Tickets
          WHERE Id=$ticketId";

        selectCmd.Parameters.AddWithValue("$ticketId", id);

        using var reader = selectCmd.ExecuteReader();
        
        TicketDto ticket = ReadTicketFromReader(reader);

        return ticket.ToTicket(); 
    }

    public List<Ticket> GetAllTicketsInProject(int projectId) {
        throw new NotImplementedException();
    }

    public List<Ticket> GetTicketsByStatus(Status status) {
        throw new NotImplementedException();
    }

    public void UpdateTicketTitle(int id, string title) {
        var insertCmd = _connection.CreateCommand();
        insertCmd.CommandText = 
        @"UPDATE Tickets 
          SET Title = $title,
          UpdatedAt = $updatedAt
          WHERE Id=$ticketId
          ";

        DateTime updateTime = DateTime.UtcNow;

        insertCmd.Parameters.AddWithValue("$ticketId", id);
        insertCmd.Parameters.AddWithValue("$title", title);
        insertCmd.Parameters.AddWithValue("$updatedAt", DateTimeConverter.ToUnix(DateTime.UtcNow));

        insertCmd.ExecuteNonQuery();   
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

    private TicketDto ReadTicketFromReader(SqliteDataReader reader) {
        reader.Read();
        return ReadTicketWithoutAdvance(reader);
    }

    private List<TicketDto> ReadTicketsFromReader(SqliteDataReader reader) {
        List<TicketDto> users = new List<TicketDto>();
        while (reader.Read()) {
            users.Add(ReadTicketWithoutAdvance(reader));
        }

        return users;
    }

    private TicketDto ReadTicketWithoutAdvance(SqliteDataReader reader) {
        var ticket = new TicketDto {
            Id = (long)reader["Id"],
            Identifier = (string)reader["Identifier"],
            Title = (string)reader["Title"],
            Description = (string)reader["Description"],
            Priority = (Priority)(int)(long)reader["Priority"],
            AuthorKey = (long)reader["AuthorKey"],
            Status = (Status)(int)(long)reader["Status"],
            HasComments = (long)reader["HasComments"],
            HasRelationships = (long)reader["HasRelationships"],
            HasTags = (long)reader["HasTags"],
            HasWorkHistory = (long)reader["HasWorkHistory"],
            ProjectKey = (long?)reader["ProjectKey"],
            CreatedAt = DateTimeConverter.FromUnix((long)reader["CreatedAt"]),
            UpdatedAt = DateTimeConverter.FromUnix((long)reader["UpdatedAt"]),
            ResolvedAt = DateTimeConverter.FromUnix((long)reader["ResolvedAt"])
        };
        return ticket;
    }
}