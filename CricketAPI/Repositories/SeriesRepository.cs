using CricketAPI.Entites;
using CricketAPI.Helpers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CricketAPI.Repositories
{
    public class SeriesRepository
    {
        public List<Series> GetSeries(DataContext context)
        {
            List<Series> seriesLst = new List<Series>();
            try
            {
                List<SeriesJson> seriesJsons = context.SeriesJson.FromSqlRaw("CALL `cric_Get_Series`()").ToList();
                if (seriesJsons.Count > 0)
                {
                    seriesLst = JsonConvert.DeserializeObject<List<Series>>(seriesJsons[0].Series) ?? throw new ArgumentException();
                }

            }
            catch (Exception)
            {
                throw;
            }
            return seriesLst;
        }

        public List<Fixtures> GetSeriesFixtures(DataContext context, long series_id)
        {
            List<Fixtures> fixtures = new List<Fixtures>();
            try
            {
                List<FixturesJson> fixturesJson = context.FixturesJson.FromSqlRaw("CALL `cric_Get_SeriesFixtures`(" + series_id + ")").ToList();
                if (!fixturesJson[0].Fixtures.Equals("[]"))
                {
                    fixtures = JsonConvert.DeserializeObject<List<Fixtures>>(fixturesJson[0].Fixtures) ?? throw new ArgumentException();
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
