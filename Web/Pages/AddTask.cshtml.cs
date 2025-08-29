using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Core.Services;
using Core.Entity;

namespace Web.Pages;

public class AddTaskModel : PageModel
{
    private readonly ITaskService _taskService;

    [BindProperty]
    public string Description { get; set; } = string.Empty;

    [BindProperty]
    public DateTimeOffset? DueAt { get; set; }

    public AddTaskModel(ITaskService taskService)
    {
        _taskService = taskService;
    }

    public void OnGet()
    {
        // Just show the form
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _logger.LogInformation("Creating new task {description}", Description);

        var newTask = new Core.Entity.Task
        {
            Description = Description,
            DueAt = DueAt,
            Status = Core.Entity.TaskStatus.Todo
        };

        _taskService.AddTask(newTask);

        // After adding, redirect back to the main list page
        return RedirectToPage("/Index");
    }
}