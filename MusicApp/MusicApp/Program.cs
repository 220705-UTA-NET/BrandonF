using MusicApp.Database;

//string connectionString = await File.ReadAllTextAsync("C:/Users/brand/Revature/connection.txt");
//string connectionString = Environment.GetCommandLineArgs()[0];

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration["connectionString"];
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
