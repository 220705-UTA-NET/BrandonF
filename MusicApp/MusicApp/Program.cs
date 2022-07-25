using MusicApp.Database;

string connectionString = Environment.GetEnvironmentVariable("dbURL");
Console.Write(connectionString);
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSingleton<IRepository>();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IRepository>(
    sp => new SQLRepository(connectionString, sp.GetRequiredService<ILogger<SQLRepository>>())
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
