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
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<TicketRepository>();

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddProvider(new FileLoggerProvider("Logs/ptsapi.log"));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000") // temporary
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
/*
app.Use(async (context, next) =>
{
    var userRepo = context.RequestServices.GetRequiredService<UserRepository>();
    var basicAuth = new BasicAuth(next, userRepo);
    await basicAuth.Invoke(context);
});
*/
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.UseCors();

app.Run();
