namespace PTS.Entity.DAL;

using Microsoft.Data.Sqlite;

public class Database {
    private string _connectionString = "DataSource=:memory:";

    private SqliteConnection _connection;

    public Database() {}
    
    public Database(string connectionOverride) {
        _connectionString = connectionOverride;
    }

    public SqliteConnection GetConnection() {
        if (_connection == null) {
            _connection = new SqliteConnection(_connectionString);
        }

        if (_connection.State == System.Data.ConnectionState.Closed) {
            _connection.Open();
        }

        return _connection;
    }

}