namespace Core.Services;

using Core.Entity;
using Core.Exceptions;

/// <summary>
/// Public API for working with tasks 
/// </summary>
public interface ITaskService
{
    /// <summary>
    /// Returns all stored tasks
    /// </summary>
    IEnumerable<Task> GetAllTasks();

    /// <summary>
    /// Returns all tasks in the "Todo" status
    /// </summary>
    IEnumerable<Task> GetTodoTasks();

    /// <summary>
    /// Returns all tasks in the "Complete" status
    /// </summary>
    IEnumerable<Task> GetCompletedTasks();

    /// <summary>
    /// Creates and saves the provided task. Note that the identifier 
    /// will be automatically set
    /// </summary>
    /// <returns>The task that was saved with relevant fields set</returns>
    Task AddTask(Task t);

    /// <summary>
    /// Marks a task as complete
    /// </summary>
    /// <returns>The task that was saved with all relevant fields</returns>
    Task CompleteTask(int taskIdentifier);

    /// <summary>
    /// Transitions a task back from complete to todo
    /// </summary>
    /// <returns>The task that was saved with all relevant fields</returns>
    Task UncompleteTask(int taskIdentifier);

    /// <summary>
    /// Updates the due date on the given task
    /// </summary>
    /// <returns>The task that was saved with all relevant fields</returns>
    Task UpdateTaskDueDate(int taskIdentifier, DateTimeOffset dueDate);

    /// <summary>
    /// Updates the description on the given task
    /// </summary>
    /// <returns>The task that was saved with all relevant fields</returns>
    Task UpdateTaskDescription(int taskIdentifier, string description);

    /// <summary>
    /// Deletes the given task completely
    /// </summary>
    /// <returns>True if the task was successfully deleted</returns>
    bool DeleteTask(int taskIdentifier);
}

/// <summary>
/// Public API for working with tasks 
/// </summary>
internal class TaskService: ITaskService
{
    private readonly ITaskStore _taskStore;

    public TaskService(ITaskStore taskStore)
    {
        _taskStore = taskStore;
    }

    public IEnumerable<Task> GetAllTasks()
    {
        return _taskStore.GetAllTasks();
    }

    public IEnumerable<Task> GetTodoTasks()
    {
        return _taskStore.GetAllTasks().Where(t => t.Status == TaskStatus.Todo);
    }

    public IEnumerable<Task> GetCompletedTasks()
    {
        return _taskStore.GetAllTasks().Where(t => t.Status == TaskStatus.Complete);
    }

    public Task AddTask(Task t)
    {
        t.CreatedAt = DateTimeOffset.Now;

        _taskStore.AddTask(t);

        var savedTask = _taskStore.GetTask(t.Identifier);

        if (savedTask == null)
        {
            throw new TaskStoreException("An error occured saving the task");
        }

        return savedTask;
    }

    public Task CompleteTask(int taskIdentifier)
    {
        var t = _taskStore.GetTask(taskIdentifier);

        if (t == null)
        {
            throw new TaskStoreException($"Task with identifier {taskIdentifier} could not be found");
        }

        t.Status = TaskStatus.Complete;
        t.CompletedAt = DateTimeOffset.Now;

        _taskStore.UpdateTask(t);

        var savedTask = _taskStore.GetTask(t.Identifier);

        if (savedTask == null)
        {
            throw new TaskStoreException("An error occured saving the task");
        }

        return savedTask;
    }

    public Task UncompleteTask(int taskIdentifier)
    {
        var t = _taskStore.GetTask(taskIdentifier);

        if (t == null)
        {
            throw new TaskStoreException($"Task with identifier {taskIdentifier} could not be found");
        }

        t.Status = TaskStatus.Todo;
        t.CompletedAt = null;

        _taskStore.UpdateTask(t);

        var savedTask = _taskStore.GetTask(t.Identifier);

        if (savedTask == null)
        {
            throw new TaskStoreException("An error occured saving the task");
        }

        return savedTask;
    }

    public Task UpdateTaskDueDate(int taskIdentifier, DateTimeOffset dueDate)
    {
        var t = _taskStore.GetTask(taskIdentifier);

        if (t == null)
        {
            throw new TaskStoreException($"Task with identifier {taskIdentifier} could not be found");
        }

        t.DueAt = dueDate;

        _taskStore.UpdateTask(t);

        var savedTask = _taskStore.GetTask(t.Identifier);

        if (savedTask == null)
        {
            throw new TaskStoreException("An error occured saving the task");
        }

        return savedTask;
    }

    public Task UpdateTaskDescription(int taskIdentifier, string description)
    {
        var t = _taskStore.GetTask(taskIdentifier);

        if (t == null)
        {
            throw new TaskStoreException($"Task with identifier {taskIdentifier} could not be found");
        }

        t.Description = description;

        _taskStore.UpdateTask(t);

        var savedTask = _taskStore.GetTask(t.Identifier);

        if (savedTask == null)
        {
            throw new TaskStoreException("An error occured saving the task");
        }

        return savedTask;
    }

    public bool DeleteTask(int taskIdentifier)
    {
        var t = _taskStore.GetTask(taskIdentifier);

        if (t == null)
        {
            return false;
        }

        _taskStore.DeleteTask(t);

        return true;
    }
}