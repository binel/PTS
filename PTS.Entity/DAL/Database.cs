namespace PTS.Entity.DAL;

using Microsoft.Data.Sqlite;

public static class Database {
    private static string _connectionString = "Data Source=pts.sqlite";

    public static SqliteConnection Connection;

    public static SqliteConnection GetConnection() {
        if (Connection == null) {
            Connection = new SqliteConnection(_connectionString);
        }

        return Connection;
    }

}