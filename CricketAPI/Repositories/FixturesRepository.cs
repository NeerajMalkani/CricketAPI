using CricketAPI.Helpers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CricketAPI.Repositories
{
    public class FixturesRepository
    {
        public List<Fixtures> GetFixtures(DataContext context, string type)
        {
            List<Fixtures> fixtures = new List<Fixtures>();
            try
            {
                List<FixturesJson> fixturesJson = type == "upcoming" ? 
                    context.FixturesJson.FromSqlRaw("CALL `cric_Get_UpcomingFixtures`()").ToList() :
                    type == "live" ?
                    context.FixturesJson.FromSqlRaw("CALL `cric_Get_LiveFixtures`()").ToList() :
                    context.FixturesJson.FromSqlRaw("CALL `cric_Get_RecentFixtures`()").ToList();
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
