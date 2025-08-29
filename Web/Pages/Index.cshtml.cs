using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Core.Services;

namespace Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ITaskService _taskService;

    public IEnumerable<Core.Entity.Task> Tasks { get; private set; } = Enumerable.Empty<Core.Entity.Task>();

    public int TotalTodo { get; private set; } = 0;

    public IndexModel(ILogger<IndexModel> logger, ITaskService taskService)
    {
        _logger = logger;
        _taskService = taskService;
    }

    public void OnGet()
    {
        _logger.LogInformation("Getting Todo Tasks for list page");
        Tasks = _taskService.GetTodoTasks().OrderBy(t => t.DueAt.HasValue ? t.DueAt : DateTimeOffset.MaxValue);
        TotalTodo = Tasks.Count();
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
}