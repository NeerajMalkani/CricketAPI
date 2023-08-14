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
                teams = new List<Teams>();
            }
            return teams;
        }

        public List<Teams> GetTeamInfo(DataContext context, long id)
        {
            List<Teams> teams = new List<Teams>();
            try
            {
                teams = context.Teams.Where(t => t.id == id).ToList();
            }
            catch (Exception)
            {
                teams = new List<Teams>();
            }
            return teams;
        }

        public List<Teams> GetSearchTeam(DataContext context, string teamname)
        {
            List<Teams> teams = new List<Teams>();
            try
            {
                teams = context.Teams.Where(t => (t.name != null && t.name.Contains(teamname))).ToList();
            }
            catch (Exception)
            {
                teams = new List<Teams>();
            }
            return teams;
        }
    }
}
