using CricketAPI.Helpers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CricketAPI.Repositories
{
    public class FixturesRepository
    {
        public List<Fixtures> GetFixtures(DataContext context)
        {
            List<Fixtures> fixtures = new List<Fixtures>();
            try
            {
                List<FixturesJson> fixturesJson = context.FixturesJson.FromSqlRaw("CALL `cric_Get_UpcomingFixtures`()").ToList();
                fixtures = JsonConvert.DeserializeObject<List<Fixtures>>(fixturesJson[0].Fixtures) ?? throw new ArgumentException();
            }
            catch (Exception)
            {
                throw;
            }
            return fixtures;
        }
    }
}
