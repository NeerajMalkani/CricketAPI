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
                switch (type)
                {
                    case "upcoming":
                        fixtures = context.Fixtures.FromSqlRaw("CALL `cric_Get_UpcomingFixtures`()").ToList();
                        break;
                    case "recent":
                        fixtures = context.Fixtures.FromSqlRaw("CALL `cric_Get_RecentFixtures`()").ToList();
                        break;
                    case "live":
                        fixtures = context.Fixtures.FromSqlRaw("CALL `cric_Get_LiveFixtures`()").ToList();
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return fixtures;
        }
    }
}
