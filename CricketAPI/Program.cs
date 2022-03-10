using CricketAPI;
using CricketAPI.Helpers;
using MySqlConnector;
using System.Data;
using System.Net;

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

#region Continents
app.MapGet("/getcontinents", () =>
{
    using var connection = new MySqlConnection(app.Configuration.GetConnectionString("Default"));
    connection.Open();

    using var command = new MySqlCommand("SELECT id, name FROM Continents;", connection);
    using var reader = command.ExecuteReader();
    List<Continents> arrcontinents = new List<Continents>();
    while (reader.Read())
    {
        Continents continents = Common.ConvertToObject<Continents>(reader);
        arrcontinents.Add(continents);
    }
    return arrcontinents;
})
.WithName("GetContinents");
#endregion

#region Fixtures
app.MapGet("/getfixtures", () =>
{
    MySqlConnection connection = MySQLHelper.EstablishConnection(app.Configuration);
    MySqlCommand command = MySQLHelper.ExecuteCommand(connection, "GetFixtures", CommandType.StoredProcedure);
    MySqlDataReader reader = command.ExecuteReader();
    Response response = new Response();
    List<Fixtures> arrfixtures = new List<Fixtures>();
    while (reader.Read())
    {
        FixtureDetails fixturedetails = Common.ConvertToObject<FixtureDetails>(reader);
        FixtureToss fixturetoss = Common.ConvertToObject<FixtureToss>(reader);
        Fixtures? fixtures = Common.ConvertToObject<Fixtures>(reader);
        fixtures.fixture_details = fixturedetails;
        fixtures.fixture_toss = fixturetoss;
        arrfixtures.Add(fixtures);
    }
    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, arrfixtures);
    return response;
})
.WithName("GetFixtures");
#endregion

app.Run();