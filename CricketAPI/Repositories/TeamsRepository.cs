using CricketAPI.Helpers;

namespace CricketAPI.Repositories
{
    public class TeamsRepository
    {
        public List<Teams> GetTeams(DataContext context)
        {
            List<Teams> teams = new List<Teams>();
            try
            {
                teams = context.Teams.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return teams;
        }
    }
}
