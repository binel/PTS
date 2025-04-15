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


builder.Services.AddSingleton<DatabaseCreator>();
builder.Services.AddScoped<IdentifierRepository>();

builder.Services.AddOpenApi();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
