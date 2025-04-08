namespace PTS.Entity.DAL;

using PTS.Entity.Domain;
using Microsoft.Data.Sqlite;
using PTS.Entity.Util;

public class StatusHistoryRepository {
    private SqliteConnection _connection;

    public StatusHistoryRepository(SqliteConnection connection) {
        _connection = connection;
    }

    public void AddStatusHistory(StatusHistory history) {
        var insertCmd = _connection.CreateCommand();
        insertCmd.CommandText = 
        @"INSERT INTO StatusHistory (
            MoverKey,
            TicketKey,
            FromStatus,
            ToStatus,
            CreatedAt)
          VALUES (
           $moverKey,
           $ticketKey,
           $fromStatus,
           $toStatus,
           $createdAt)";

        insertCmd.Parameters.AddWithValue("$moverKey", history.MoverId);
        insertCmd.Parameters.AddWithValue("$ticketKey", history.TicketId);
        insertCmd.Parameters.AddWithValue("$fromStatus", (int)history.FromStatus);
        insertCmd.Parameters.AddWithValue("$toStatus", (int)history.ToStatus);
        insertCmd.Parameters.AddWithValue("$createdAt", DateTimeConverter.ToUnix(history.CreatedAt));

        insertCmd.ExecuteNonQuery();
    }

    public int GetCountStatusTransitions() {
        var selectCmd = _connection.CreateCommand();
        selectCmd.CommandText = 
        @"SELECT COUNT(*) FROM StatusHistory";

        using var reader = selectCmd.ExecuteReader();
        while (reader.Read())
        {
            return reader.GetInt32(0);
        }
        throw new InvalidOperationException("Could not get count of status transitions");
    }

    public List<StatusHistory> GetStatusHistoryForTicket(long ticketId) {
      var selectCmd = _connection.CreateCommand();
        selectCmd.CommandText = 
        @"SELECT Id, 
            TicketKey,
            MoverKey,
            FromStatus,
            ToStatus,
            CreatedAt
          FROM StatusHistory
          WHERE TicketKey=$ticketKey";

        selectCmd.Parameters.AddWithValue("$ticketKey", ticketId);

        using var reader = selectCmd.ExecuteReader();
        
        List<StatusHistory> histories = ReadStatusHistoriesFromReader(reader);

        return histories; 
    }

    private StatusHistory ReadStatusHistoryFromReader(SqliteDataReader reader) {
        reader.Read();
        return ReadStatusHistoryWithoutAdvance(reader);
    }

    private List<StatusHistory> ReadStatusHistoriesFromReader(SqliteDataReader reader) {
        List<StatusHistory> histories = new List<StatusHistory>();
        while (reader.Read()) {
            histories.Add(ReadStatusHistoryWithoutAdvance(reader));
        }

        return histories;
    }

    private StatusHistory ReadStatusHistoryWithoutAdvance(SqliteDataReader reader) {
        var history = new StatusHistory {
            Id = (long)reader["Id"],
            TicketId = (long)reader["TicketKey"],
            MoverId = (long)reader["MoverKey"],
            FromStatus = (Status)(int)(long)reader["FromStatus"],
            ToStatus = (Status)(int)(long)reader["ToStatus"],
            CreatedAt = DateTimeConverter.FromUnix((long)reader["CreatedAt"]),
        };
        return history;
    }

}