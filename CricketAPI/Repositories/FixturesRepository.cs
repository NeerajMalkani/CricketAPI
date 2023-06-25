﻿using CricketAPI.Helpers;
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
    }
}
