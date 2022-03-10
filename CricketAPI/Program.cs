using CricketAPI;
using MySqlConnector;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<MySqlConnection>(_ => new MySqlConnection(builder.Configuration["ConnectionStrings:Default"]));


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
       new WeatherForecast
       (
           DateTime.Now.AddDays(index),
           Random.Shared.Next(-20, 55),
           summaries[Random.Shared.Next(summaries.Length)]
       ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapGet("/getcontinents", () =>
{
    using var connection = new MySqlConnection(app.Configuration.GetConnectionString("Default"));
    connection.Open();

    using var command = new MySqlCommand("SELECT id, name FROM Continents;", connection);
    using var reader = command.ExecuteReader();
    List<Continents> arrcontinents = new List<Continents>();
    while (reader.Read())
    {
        Continents continents = new Continents();
        continents.id = Convert.ToInt64(reader["id"]);
        continents.name = Convert.ToString(reader["name"]);
        arrcontinents.Add(continents);
    }
    return arrcontinents;
})
.WithName("GetContinents");



app.MapGet("/getfixtures", () =>
{
    using var connection = new MySqlConnection(app.Configuration.GetConnectionString("Default"));
    connection.Open();

    using var command = new MySqlCommand("GetFixtures", connection);
    command.CommandType = CommandType.StoredProcedure;
    using var reader = command.ExecuteReader();
    List<Fixtures> arrfixtures = new List<Fixtures>();
    while (reader.Read())
    {
        Fixtures fixtures = Common.ConvertToObject<Fixtures>(reader);
        arrfixtures.Add(fixtures);
    }
    return arrfixtures;
})
.WithName("GetFixtures");



app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}