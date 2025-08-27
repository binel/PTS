namespace Core.Services;
using System.Data;
using Microsoft.Data.Sqlite;
using Dapper;
using Core.Entity;

/// <summary>
/// Interal service for persisting tasks
/// </summary>
internal interface ITaskStore
{
    /// <summary>
    /// Adds the task to the store
    /// </summary>
    /// <param name="t">The task to be added</param>
    void AddTask(Task t);

    /// <summary>
    /// Removes the task from the store
    /// </summary>
    /// <param name="t">The task to be removed</param>
    void DeleteTask(Task t);

    /// <summary>
    /// Update an existing task in the store
    /// </summary>
    /// <param name="t">The task to be updated</param>
    void UpdateTask(Task t);

    /// <summary>
    /// Returns the task with the given identifier
    /// </summary>
    /// <returns>Task if it can be found, null otherwise</returns>
    Task? GetTask(int taskIdentifier);

    /// <summary>
    /// Returns all tasks in the store
    /// </summary>
    /// <returns></returns>
    IEnumerable<Task> GetAllTasks();
}

public class SqliteTaskStore : ITaskStore
{
    private readonly string _connStr;

    public SqliteTaskStore(string dbPath)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);
        _connStr = $"Data Source={dbPath};Cache=Shared";
        Init();
    }

    private void Init()
    {
        using var conn = Open();
        conn.Execute(@"
            PRAGMA journal_mode=WAL;
            PRAGMA busy_timeout=3000;
            CREATE TABLE IF NOT EXISTS Tasks (
                Identifier INTEGER PRIMARY KEY AUTOINCREMENT,
                Description TEXT NOT NULL,
                CreatedAt TEXT NOT NULL,
                DueAt TEXT NULL,
                CompletedAt TEXT NULL,
                Status INTEGER NOT NULL
            );
        ");
    }

    private IDbConnection Open() => new SqliteConnection(_connStr);

    public IEnumerable<Task> GetAllTasks()
    {
        using var conn = Open();
        return conn.Query<Task>("SELECT * FROM Tasks ORDER BY Identifier DESC");
    }

    public Task? GetTask(int id)
    {
        using var conn = Open();
        return conn.QuerySingleOrDefault<Task>(
            "SELECT * FROM Tasks WHERE Identifier = @id", new { id });
    }

    public void AddTask(Task t)
    {
        using var conn = Open();
        var id = conn.ExecuteScalar<long>(@"
            INSERT INTO Tasks (Description, CreatedAt, DueAt, CompletedAt, Status)
            VALUES (@Description, @CreatedAt, @DueAt, @CompletedAt, @Status);
            SELECT last_insert_rowid();", t);
        t.Identifier = (int)id;
    }

    public void UpdateTask(Task t)
    {
        using var conn = Open();
        conn.Execute(@"
            UPDATE Tasks SET
              Description=@Description,
              CreatedAt=@CreatedAt,
              DueAt=@DueAt,
              CompletedAt=@CompletedAt,
              Status=@Status
            WHERE Identifier=@Identifier;", t);
    }

    public void DeleteTask(Task t)
    {
        using var conn = Open();
        conn.Execute("DELETE FROM Tasks WHERE Identifier=@Identifier", t);
    }
}

/// <summary>
/// An in-memory non-persistent task store - useful for testing
/// </summary>
internal class InMemoryTaskStore : ITaskStore
{
    private readonly Dictionary<int, Task> _tasks;
    private int _nextId;
    public InMemoryTaskStore()
    {
        _tasks = new Dictionary<int, Task>();
        _nextId = 0;
    }

    public void AddTask(Task t)
    {
        t.Identifier = _nextId;
        _tasks[_nextId] = t;
        _nextId++;
    }

    public void DeleteTask(Task t)
    {
        _tasks.Remove(t.Identifier);
    }

    public void UpdateTask(Task t)
    {
        _tasks[t.Identifier] = t;
    }

    public Task? GetTask(int taskIdentifier)
    {
        if (_tasks.ContainsKey(taskIdentifier))
        {
            return _tasks[taskIdentifier];
        }
        else
        {
            return null;
        }
    }

    public IEnumerable<Task> GetAllTasks()
    {
        List<Task> deepCopy = new List<Task>();
        foreach (var k in _tasks.Keys)
        {
            var t = _tasks[k];
            deepCopy.Add(new Task
            {
                Identifier = t.Identifier,
                Description = t.Description,
                Status = t.Status,
                CreatedAt = t.CreatedAt,
                DueAt = t.DueAt,
                CompletedAt = t.CompletedAt
            });
        }
        return deepCopy;
    }
}