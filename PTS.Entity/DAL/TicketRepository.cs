namespace PTS.Entity.DAL;

using PTS.Entity.Domain;
using Microsoft.Data.Sqlite;
using PTS.Entity.Util;

public class TicketRepository {
    private SqliteConnection _connection;

    public TicketRepository(SqliteConnection connection) {
        _connection = connection;
    }

    public void AddTicket(Ticket ticket) {
        var insertCmd = _connection.CreateCommand();
        
        if (ticket.ResolvedAt == null) {
            insertCmd.CommandText = GetInsertCommand();
        }
        else {
            insertCmd.CommandText = GetInsertCommandWithResolvedAt();
        }

        insertCmd.Parameters.AddWithValue("$identifier", ticket.Identifier);
        insertCmd.Parameters.AddWithValue("$title", ticket.Title);
        insertCmd.Parameters.AddWithValue("$description", ticket.Description);
        insertCmd.Parameters.AddWithValue("$priority", (int)ticket.Priority);
        insertCmd.Parameters.AddWithValue("$authorKey", ticket.AuthorId);
        insertCmd.Parameters.AddWithValue("$status", (int)ticket.Status);
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
            CreatedAt,
            UpdatedAt)
          VALUES (
           $identifier,
           $title,
           $description,
           $priority,
           $authorKey,
           $status,
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
            CreatedAt,
            UpdatedAt,
            ResolvedAt
          FROM Tickets
          WHERE Id=$ticketId";

        selectCmd.Parameters.AddWithValue("$ticketId", id);

        using var reader = selectCmd.ExecuteReader();
        
        Ticket ticket = ReadTicketFromReader(reader);

        return ticket; 
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
        var insertCmd = _connection.CreateCommand();
        insertCmd.CommandText = 
        @"UPDATE Tickets 
          SET Description = $description,
          UpdatedAt = $updatedAt
          WHERE Id=$ticketId
          ";

        DateTime updateTime = DateTime.UtcNow;

        insertCmd.Parameters.AddWithValue("$ticketId", id);
        insertCmd.Parameters.AddWithValue("$description", description);
        insertCmd.Parameters.AddWithValue("$updatedAt", DateTimeConverter.ToUnix(DateTime.UtcNow));

        insertCmd.ExecuteNonQuery();  
    }

    public void UpdatePriority(int id, Priority priority) {
        var insertCmd = _connection.CreateCommand();
        insertCmd.CommandText = 
        @"UPDATE Tickets 
          SET Priority = $priority,
          UpdatedAt = $updatedAt
          WHERE Id=$ticketId
          ";

        DateTime updateTime = DateTime.UtcNow;

        insertCmd.Parameters.AddWithValue("$ticketId", id);
        insertCmd.Parameters.AddWithValue("$priority", (int)priority);
        insertCmd.Parameters.AddWithValue("$updatedAt", DateTimeConverter.ToUnix(DateTime.UtcNow));

        insertCmd.ExecuteNonQuery();  
    }

    public void UpdateTicketStatus(int ticketId, Status status) {
        var insertCmd = _connection.CreateCommand();
        insertCmd.CommandText = 
        @"UPDATE Tickets 
          SET Status = $status,
          UpdatedAt = $updatedAt
          WHERE Id=$ticketId
          ";

        DateTime updateTime = DateTime.UtcNow;

        insertCmd.Parameters.AddWithValue("$ticketId", ticketId);
        insertCmd.Parameters.AddWithValue("$status", (int)status);
        insertCmd.Parameters.AddWithValue("$updatedAt", DateTimeConverter.ToUnix(DateTime.UtcNow));

        insertCmd.ExecuteNonQuery();
    }

    private Ticket ReadTicketFromReader(SqliteDataReader reader) {
        reader.Read();
        return ReadTicketWithoutAdvance(reader);
    }

    private List<Ticket> ReadTicketsFromReader(SqliteDataReader reader) {
        List<Ticket> tickets = new List<Ticket>();
        while (reader.Read()) {
            tickets.Add(ReadTicketWithoutAdvance(reader));
        }

        return tickets;
    }

    private Ticket ReadTicketWithoutAdvance(SqliteDataReader reader) {
        var ticket = new Ticket {
            Id = (long)reader["Id"],
            Identifier = (string)reader["Identifier"],
            Title = (string)reader["Title"],
            Description = (string)reader["Description"],
            Priority = (Priority)(int)(long)reader["Priority"],
            AuthorId = (long)reader["AuthorKey"],
            Status = (Status)(int)(long)reader["Status"],
            CreatedAt = DateTimeConverter.FromUnix((long)reader["CreatedAt"]),
            UpdatedAt = DateTimeConverter.FromUnix((long)reader["UpdatedAt"]),
            ResolvedAt = DateTimeConverter.FromUnix((long)reader["ResolvedAt"])
        };
        return ticket;
    }
}