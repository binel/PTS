namespace Core.Exceptions;

using System;

public class TaskStoreException : Exception
{
    public TaskStoreException() { }

    public TaskStoreException(string message) 
        : base(message) { }

    public TaskStoreException(string message, Exception inner) 
        : base(message, inner) { }
}