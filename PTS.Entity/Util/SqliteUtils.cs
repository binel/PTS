namespace PTS.Entity.Util;

using Microsoft.Data.Sqlite;

public static class SqliteUtils {
    public static void PrintInsertCommandParametersForError(Exception e, SqliteCommand command) {
        Console.WriteLine("SQL Execution failed:");
        Console.WriteLine($"Message: {e.Message}");
        Console.WriteLine("Parameters:");
        foreach (SqliteParameter p in command.Parameters)
        {
            Console.WriteLine($"{p.ParameterName} = {(p.Value ?? "NULL")} (Type: {p.Value?.GetType().Name ?? "null"})");
        }        
    }

    public static void PrintTableSchema(SqliteConnection connection, string tableName) {
        Console.WriteLine($"Printing table schema for table {tableName}");
        
        var queryCommand = connection.CreateCommand();
        queryCommand.CommandText = $"PRAGMA table_info({tableName})";
        using var reader = queryCommand.ExecuteReader();
        while (reader.Read())
        {
            var cid = reader.GetInt32(0);
            var name = reader.GetString(1);
            var type = reader.GetString(2);
            var notNull = reader.GetInt32(3) == 1 ? "NOT NULL" : "NULLABLE";
            var defaultValue = !reader.IsDBNull(4) ? reader.GetValue(4)?.ToString() : "NULL";
            var pk = reader.GetInt32(5) == 1 ? "PRIMARY KEY" : "";

            Console.WriteLine($"{cid}: {name} {type} {notNull} DEFAULT {defaultValue} {pk}");
        }
    }
}