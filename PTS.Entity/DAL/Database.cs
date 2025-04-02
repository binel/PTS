namespace PTS.Entity.DAL;

using Microsoft.Data.Sqlite;

public static class Database {
    private static string _connectionString = "DataSource=:memory:";

    public static SqliteConnection Connection;

    public static SqliteConnection GetConnection() {
        if (Connection == null) {
            Connection = new SqliteConnection(_connectionString);
        }

        if (Connection.State == System.Data.ConnectionState.Closed) {
            Connection.Open();
        }

        return Connection;
    }

}