namespace Core.Services;

using Core.Entity;

public interface IStatsService
{
    public IEnumerable<TaskStats> GetTaskStats(DateOnly start, DateOnly end);
}

internal class StatsService : IStatsService
{
    private readonly ITaskService _taskService;

    public StatsService(ITaskService taskService)
    {
        _taskService = taskService;
    }

    public IEnumerable<TaskStats> GetTaskStats(DateOnly start, DateOnly end)
    {
        return null;
    }
}