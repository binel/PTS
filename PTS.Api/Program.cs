using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using PTS.Api.Middleware;
using PTS.Entity.DAL;

var builder = WebApplication.CreateBuilder(args);

// allow the database connection string to be passed in 
if (args.Length == 1) {
    Console.WriteLine($"Using database connection override {args[0]}");
    builder.Services.AddSingleton<Database>((sp) => {
        return new Database(args[0]);
    });
} else {
    builder.Services.AddSingleton<Database>();
}

builder.Services.AddScoped<IdentifierRepository>();

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddProvider(new FileLoggerProvider("Logs/ptsapi.log"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
