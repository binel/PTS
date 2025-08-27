namespace Core.DependencyInjection;

using Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Dapper;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
    {
        services.AddSingleton<ITaskService, TaskService>();
        services.AddSingleton<ITaskStore>(_ => 
            new SqliteTaskStore(Path.Combine(AppContext.BaseDirectory, "data", "tasks.db")));

        SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());

        return services;
    }
}