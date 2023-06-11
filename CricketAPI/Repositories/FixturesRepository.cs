using CricketAPI.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CricketAPI.Repositories
{
    public class FixturesRepository
    {
        public List<Fixtures> GetFixtures(DataContext context)
        {
            List<Fixtures> fixtures = new List<Fixtures>();
            try
            {
                fixtures = context.Fixtures.FromSqlRaw("CALL `cric_Get_UpcomingFixtures`()").ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return fixtures;
        }
    }
}
