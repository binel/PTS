using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Core.Services;

namespace Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ITaskService _taskService;

    public IEnumerable<Core.Entity.Task> Tasks { get; private set; } = Enumerable.Empty<Core.Entity.Task>();

    public IndexModel(ILogger<IndexModel> logger, ITaskService taskService)
    {
        _logger = logger;
        _taskService = taskService;
    }

    public void OnGet()
    {
        Tasks = _taskService.GetTodoTasks().OrderBy(t => t.DueAt.HasValue ? t.DueAt : DateTimeOffset.MaxValue);
    }

    public IActionResult OnPostComplete(int id)
    {
        _taskService.CompleteTask(id);
        return RedirectToPage(); // reload list
    }

    public IActionResult OnPostDelete(int id)
    {
        _taskService.DeleteTask(id);
        return RedirectToPage(); // reload list
    }
}