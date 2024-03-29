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

        public static List<Lineup> ApplyPointsToPlayers(List<Lineup>? teamlineup)
        {
            List<Lineup> lineups = new List<Lineup>();

            List<Lineup> lineupsWk = new List<Lineup>();
            List<Lineup> lineupsBat = new List<Lineup>();
            List<Lineup> lineupsAll = new List<Lineup>();
            List<Lineup> lineupsBowl = new List<Lineup>();
            if (teamlineup != null)
            {
                foreach (Lineup lineup in teamlineup)
                {
                    switch (lineup.player_position_name)
                    {
                        case "Wicketkeeper":
                            lineupsWk.Add(lineup);
                            break;
                        case "Batsman":
                            lineupsBat.Add(lineup);
                            break;
                        case "Allrounder":
                            lineupsAll.Add(lineup);
                            break;
                        case "Bowler":
                            lineupsBowl.Add(lineup);
                            break;
                    }
                }

                lineupsWk = lineupsWk.OrderByDescending(pl => pl.player_runs).ToList();
                foreach (Lineup lineup1 in lineupsWk)
                {
                    lineup1.player_points = CalculatePlayerPoints(lineupsWk[0].player_runs == null ? 1 : lineupsWk[0].player_runs, lineup1.player_runs);
                }
                lineupsBat = lineupsBat.OrderByDescending(pl => pl.player_runs).ToList();
                foreach (Lineup lineup2 in lineupsBat)
                {
                    lineup2.player_points = CalculatePlayerPoints(lineupsBat[0].player_runs == null ? 1 : lineupsBat[0].player_runs, lineup2.player_runs);
                }
                List<Lineup> lineupsAllBat = lineupsAll.OrderByDescending(pl => pl.player_runs).ToList();
                List<Lineup> lineupsAllBowl = lineupsAll.OrderByDescending(pl => pl.player_wickets).ToList();
                foreach (Lineup lineup3 in lineupsAll)
                {
                    lineup3.player_points = CalculatePlayerPoints((lineupsAllBat[0].player_runs == null ? 1 : lineupsAllBat[0].player_runs) + ((lineupsAllBowl[0].player_wickets == null ? 1 : lineupsAllBowl[0].player_wickets) * 25), lineup3.player_runs + (lineup3.player_wickets * 25));
                }
                lineupsBowl = lineupsBowl.OrderByDescending(pl => pl.player_wickets).ToList();
                foreach (Lineup lineup4 in lineupsBowl)
                {
                    lineup4.player_points = CalculatePlayerPoints(lineupsBowl[0].player_wickets == null ? 1 : lineupsBowl[0].player_wickets, lineup4.player_wickets);
                }
                lineups = lineupsWk.Concat(lineupsBat).Concat(lineupsAll).Concat(lineupsBowl).ToList();
            }

            return lineups;
        }

        public static decimal CalculatePlayerPoints(int? maxValue, int? playerValue)
        {
            decimal calculatedValue = 0;
            calculatedValue = Math.Round(Convert.ToDecimal((Convert.ToDouble(playerValue) / Convert.ToDouble(maxValue) * 9).ToString("N1")) + Convert.ToDecimal(0.5)) - Convert.ToDecimal(0.5);
            if (calculatedValue < 5)
            {
                decimal[] pointValues = new decimal[4] { Convert.ToDecimal(8.0), Convert.ToDecimal(7.5), Convert.ToDecimal(7.0), Convert.ToDecimal(6.5) };
                Random random = new Random();
                int randomIndex = random.Next(0, pointValues.Length);
                calculatedValue = pointValues[randomIndex];
            }
            return calculatedValue;
        }
        #endregion
    }
}
