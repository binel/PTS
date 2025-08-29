using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Core.Services;

namespace Web.Pages;

public class CompletedTasksModel : PageModel
{
    private readonly ILogger<CompletedTasksModel> _logger;
    private readonly ITaskService _taskService;

    public IEnumerable<Core.Entity.Task> Tasks { get; private set; } = Enumerable.Empty<Core.Entity.Task>();

    public int TotalComplete { get; private set; } = 0;

    public CompletedTasksModel(ILogger<CompletedTasksModel> logger, ITaskService taskService)
    {
        _logger = logger;
        _taskService = taskService;
    }

    public void OnGet()
    {
        _logger.LogInformation("Getting completed tasks for done page");
        Tasks = _taskService.GetCompletedTasks().OrderByDescending(t => t.CompletedAt.HasValue ? t.CompletedAt : DateTimeOffset.MinValue);
        TotalComplete = Tasks.Count();
    }

    public IActionResult OnPostUncomplete(int id)
    {
        _logger.LogInformation("Uncompleting task {id}", id);
        _taskService.UncompleteTask(id);
        return RedirectToPage(); // reload list
    }
}