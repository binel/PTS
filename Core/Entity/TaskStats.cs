namespace Core.Entity;

public class TaskStats
{
    public DateOnly Date { get; set; }

    public int TasksCreated { get; set; } = 0;

    public int TasksCompleted { get; set; } = 0;
}