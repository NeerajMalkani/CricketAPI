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
app.UseMiddleware<ApiKeyMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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
    Response response = new Response();
    try
    {
        List<Fixtures> arrfixtures = new List<Fixtures>();
        MySqlConnection connection = MySQLHelper.EstablishConnection(app.Configuration);
        MySqlCommand command = MySQLHelper.ExecuteCommand(connection, "GetFixtures", CommandType.StoredProcedure);
        MySqlDataReader reader = command.ExecuteReader();
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
    }
    catch (Exception ex)
    {
        Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
    }
    return response;
})
.WithName("GetFixtures");
#endregion

app.Run();