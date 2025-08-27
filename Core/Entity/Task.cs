namespace Core.Entity;

using System;

/// <summary>
/// Represents a task the user wants to track
/// </summary>
public class Task
{
    /// <summary>
    /// Numeric identifier for the task 
    /// </summary>
    public int Identifier { get; set; }

    /// <summary>
    /// User-facing description of the task 
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The current status of the task
    /// </summary>
    public TaskStatus Status { get; set; } = TaskStatus.Todo;

    /// <summary>
    /// When the task was orginally created - Local time 
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// When the task is due - Local time
    /// </summary>
    public DateTimeOffset? DueAt { get; set; }

    /// <summary>
    /// When the task was completed - Local time
    /// </summary>
    public DateTimeOffset? CompletedAt { get; set; }
}