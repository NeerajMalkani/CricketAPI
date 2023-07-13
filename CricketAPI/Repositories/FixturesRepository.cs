using CricketAPI.Helpers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CricketAPI.Repositories
{
    public class FixturesRepository
    {
        #region Fixtures
        public List<Fixtures> GetFixtures(DataContext context, string type)
        {
            List<Fixtures> fixtures = new List<Fixtures>();
            try
            {
                switch (type)
                {
                    case "upcoming":
                        List<FixturesJson> fixturesUpcomingJson = context.FixturesJson.FromSqlRaw("CALL `cric_Get_UpcomingFixtures`()").ToList();
                        if (!fixturesUpcomingJson[0].Fixtures.Equals("[]"))
                        {
                            fixtures = JsonConvert.DeserializeObject<List<Fixtures>>(fixturesUpcomingJson[0].Fixtures) ?? throw new ArgumentException();
                        }
                        break;
                    case "recent":
                        List<FixturesJson> fixturesRecentJson = context.FixturesJson.FromSqlRaw("CALL `cric_Get_RecentFixtures`()").ToList();
                        if (!fixturesRecentJson[0].Fixtures.Equals("[]"))
                        {
                            fixtures = JsonConvert.DeserializeObject<List<Fixtures>>(fixturesRecentJson[0].Fixtures) ?? throw new ArgumentException();
                        }
                        break;
                    case "live":
                        List<FixturesJson> fixturesLiveJson = context.FixturesJson.FromSqlRaw("CALL `cric_Get_LiveFixtures`()").ToList();
                        if (!fixturesLiveJson[0].Fixtures.Equals("[]"))
                        {
                            fixtures = JsonConvert.DeserializeObject<List<Fixtures>>(fixturesLiveJson[0].Fixtures) ?? throw new ArgumentException();
                        }
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return fixtures;
        }

        public List<Fixtures> GetFixtureByID(DataContext context, long fixture_id)
        {
            List<Fixtures> fixtures = new List<Fixtures>();
            try
            {
                List<FixturesJson> fixturesUpcomingJson = context.FixturesJson.FromSqlRaw("CALL `cric_Get_FixtureByID`(" + fixture_id + ")").ToList();
                if (!fixturesUpcomingJson[0].Fixtures.Equals("[]"))
                {
                    fixtures = JsonConvert.DeserializeObject<List<Fixtures>>(fixturesUpcomingJson[0].Fixtures) ?? throw new ArgumentException();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return fixtures;
        }
        #endregion

        #region Scorecard
        public Scoreboard? GetScorecard(DataContext context, long fixture_id)
        {
            Scoreboard scorecard = new Scoreboard();
            try
            {
                List<ScorecardJson> scorecardJson = context.ScorecardJson.FromSqlRaw("CALL `cric_Get_FixtureScorecard`(" + fixture_id + ")").ToList();
                if (scorecardJson.Count > 0)
                {
                    scorecard = JsonConvert.DeserializeObject<Scoreboard>(scorecardJson[0].FixtureScoreboard) ?? throw new ArgumentException();
                    if (scorecard != null && scorecard.scorecard != null && scorecard.scorecard.Count > 0)
                    {
                        scorecard.scorecard.RemoveAll(item => item == null);
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
            return scorecard;
        }
        #endregion
    }
}
