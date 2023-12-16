using CricketAPI.Helpers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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

        #region Series Lineup
        public FixturesTeamLineup? GetLineup(DataContext context, long fixture_id)
        {
            FixturesTeamLineup fixturesTeamLineup = new FixturesTeamLineup();
            try
            {
                List<LineupJson> lineupJson = context.LineupJson.FromSqlRaw("CALL `cric_Get_SeriesTeamsLineup`(" + fixture_id + ")").ToList();
                if (lineupJson.Count > 0)
                {
                    fixturesTeamLineup = JsonConvert.DeserializeObject<FixturesTeamLineup>(lineupJson[0].Fixtures_TeamLineup) ?? throw new ArgumentException();
                }

            }
            catch (Exception)
            {
                fixturesTeamLineup = new FixturesTeamLineup();
            }
            return fixturesTeamLineup;
        }
        #endregion
    }
}
