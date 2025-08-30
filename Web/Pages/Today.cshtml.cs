using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Core.Services;

namespace Web.Pages;

public class TodayModel : PageModel
{
    private readonly ILogger<TodayModel> _logger;
    private readonly ITaskService _taskService;

    public IEnumerable<Core.Entity.Task> TodoTasks { get; private set; } = Enumerable.Empty<Core.Entity.Task>();

    public IEnumerable<Core.Entity.Task> CompleteTasks { get; private set; } = Enumerable.Empty<Core.Entity.Task>();

    public int TotalTodo { get; private set; } = 0;

    public int TotalComplete { get; private set; } = 0;

    public TodayModel(ILogger<TodayModel> logger, ITaskService taskService)
    {
        _logger = logger;
        _taskService = taskService;
    }

    public void OnGet()
    {
        var now = DateTimeOffset.Now;
        var startOfDay = now.Date;  
        var endOfDay = startOfDay.AddDays(1).AddTicks(-1);

        _logger.LogInformation("Getting Tasks for today page");
        TodoTasks = _taskService.GetTasksDueInRange(startOfDay, endOfDay).OrderBy(t => t.DueAt.HasValue ? t.DueAt : DateTimeOffset.MaxValue);
        TotalTodo = TodoTasks.Count();
        CompleteTasks = _taskService.GetTasksCompletedInRange(startOfDay, endOfDay).OrderBy(t => t.DueAt.HasValue ? t.DueAt : DateTimeOffset.MaxValue);
        TotalComplete = CompleteTasks.Count();
    }

    public IActionResult OnPostComplete(int id)
    {
        _logger.LogInformation("Completing task {id}", id);
        _taskService.CompleteTask(id);
        return RedirectToPage(); // reload list
    }

    public IActionResult OnPostDelete(int id)
    {
        _logger.LogInformation("Deleting task {id}", id);
        _taskService.DeleteTask(id);
        return RedirectToPage(); // reload list
    }
    
    public IActionResult OnPostUncomplete(int id)
    {
        _logger.LogInformation("Uncompleting task {id}", id);
        _taskService.UncompleteTask(id);
        return RedirectToPage(); // reload list
    }
}