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
                //switch (type)
                //{
                //    case "upcoming":
                //        fixtures = context.Fixtures.FromSqlRaw("CALL `cric_Get_UpcomingFixtures`()").ToList();
                //        break;
                //    case "recent":
                //        fixtures = context.Fixtures.FromSqlRaw("CALL `cric_Get_RecentFixtures`()").ToList();
                //        break;
                //    case "live":
                //        fixtures = context.Fixtures.FromSqlRaw("CALL `cric_Get_LiveFixtures`()").ToList();
                //        break;
                //}

                var fullEntries = (from f in context.Fixtures
                                   join l in context.Leagues
                                   on f.league_id equals l.id
                                   join s in context.Seasons
                                   on f.season_id equals s.id
                                   join st in context.Stages
                                   on f.stage_id equals st.id
                                   join lt in context.Teams
                                   on f.localteam_id equals lt.id
                                   join vt in context.Teams
                                   on f.visitorteam_id equals vt.id
                                   join v in context.Venues
                                   on f.venue_id equals v.id
                                   join fu in context.Officials
                                   on f.first_umpire_id equals fu.id
                                   join su in context.Officials
                                   on f.second_umpire_id equals su.id
                                   join tvu in context.Officials
                                   on f.tv_umpire_id equals tvu.id
                                   join re in context.Officials
                                   on f.referee_id equals re.id
                                   join mom in context.Players
                                   on f.man_of_match_id equals mom.id
                                   join mos in context.Players
                                   on f.man_of_series_id equals mos.id
                                   select new
                                   {
                                       id = f.id,
                                       league_name = l.name,
                                       season_name = s.name,
                                       stage_name = st.name,
                                       type = f.type,
                                       round = f.round,

                                   }).ToList();

            }
            catch (Exception)
            {
                throw;
            }
            return fixtures;
        }
    }
}
