namespace PTS.Entity.DAL;

using PTS.Entity.Domain;
using Microsoft.Data.Sqlite;
using PTS.Entity.Util;

public class WorkHistoryRepository {
    private SqliteConnection _connection;

    public WorkHistoryRepository(Database database) {
        _connection = database.GetConnection();
    }

    public void AddWorkHistory(WorkHistory history) {
        var insertCmd = _connection.CreateCommand();
        insertCmd.CommandText = 
        @"INSERT INTO WorkHistory (
            TicketKey,
            StartedAt,
            EndedAt,
            CreatedAt)
          VALUES (
           $ticketKey,
           $startedAt,
           $endedAt,
           $createdAt)";

        insertCmd.Parameters.AddWithValue("$ticketKey", history.TicketId);
        insertCmd.Parameters.AddWithValue("$startedAt", DateTimeConverter.ToUnix(history.StartedAt));
        insertCmd.Parameters.AddWithValue("$endedAt", DateTimeConverter.ToUnix(history.EndedAt));
        insertCmd.Parameters.AddWithValue("$createdAt", DateTimeConverter.ToUnix(history.CreatedAt));

        insertCmd.ExecuteNonQuery();
    }

    public void DeleteWorkHistory(long historyId) {
        var insertCmd = _connection.CreateCommand();
        insertCmd.CommandText = 
        @"DELETE FROM WorkHistory 
          WHERE Id=$historyId
          ";

        insertCmd.Parameters.AddWithValue("$historyId", historyId);

        insertCmd.ExecuteNonQuery();     
    }

    public int GetCountWorkHistories() {
        var selectCmd = _connection.CreateCommand();
        selectCmd.CommandText = 
        @"SELECT COUNT(*) FROM WorkHistory";

        using var reader = selectCmd.ExecuteReader();
        while (reader.Read())
        {
            return reader.GetInt32(0);
        }
        throw new InvalidOperationException("Could not get count of work histories");
    }

    public List<WorkHistory> GetWorkHistoryForTicket(long ticketId) {
        var selectCmd = _connection.CreateCommand();
        selectCmd.CommandText = 
        @"SELECT Id, 
            TicketKey,
            StartedAt,
            EndedAt,
            CreatedAt
          FROM WorkHistory
          WHERE TicketKey=$ticketKey";

        selectCmd.Parameters.AddWithValue("$ticketKey", ticketId);

        using var reader = selectCmd.ExecuteReader();
        
        List<WorkHistory> histories = ReadWorkHistoriesFromReader(reader);

        return histories; 
    }

    private WorkHistory ReadWorkHistoryFromReader(SqliteDataReader reader) {
        reader.Read();
        return ReadWorkHistoryWithoutAdvance(reader);
    }

    private List<WorkHistory> ReadWorkHistoriesFromReader(SqliteDataReader reader) {
        List<WorkHistory> histories = new List<WorkHistory>();
        while (reader.Read()) {
            histories.Add(ReadWorkHistoryWithoutAdvance(reader));
        }

        return histories;
    }

    private WorkHistory ReadWorkHistoryWithoutAdvance(SqliteDataReader reader) {
        var history = new WorkHistory {
            Id = (long)reader["Id"],
            TicketId = (long)reader["TicketKey"],
            StartedAt = DateTimeConverter.FromUnix((long)reader["StartedAt"]),
            EndedAt = DateTimeConverter.FromUnix((long)reader["EndedAt"]),
            CreatedAt = DateTimeConverter.FromUnix((long)reader["CreatedAt"])
        };
        return history;
    }    
}