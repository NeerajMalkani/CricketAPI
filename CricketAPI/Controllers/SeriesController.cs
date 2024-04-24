using CricketAPI.Entites;
using CricketAPI.Helpers;
using CricketAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CricketAPI.Controllers
{
    [Route("api/series")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly DataContext _db;

        public SeriesController(DataContext dbContext)
        {
            _db = dbContext;
        }

        #region Get series
        [HttpGet]
        [Route("list")]
        public Response GetSeries()
        {
            Response response = new Response();
            try
            {
                List<Series> series = new SeriesRepository().GetSeries(_db);
                if (series.Any())
                {
                    series = series.OrderBy(x => x.starting_at).ToList();
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, series);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, series);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }

        [HttpGet]
        [Route("trending/list")]
        public Response GetTrendingSeries()
        {
            Response response = new Response();
            try
            {
                List<Series> series = new SeriesRepository().GetTrendingSeries(_db);
                if (series.Any())
                {
                    series = series.Where(x => x.starting_at > DateTime.Now.AddDays(-30)).ToList();
                    series = series.OrderBy(x => x.starting_at).ToList();
                    series = series.DistinctBy(x => x.league_id).ToList();
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, series);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, series);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }
        #endregion

        #region Get Series Fixtures
        [HttpGet]
        [Route("fixtures")]
        public Response GetSeriesFixtures(long series_id, long stage_id, bool onlyUpcoming = false)
        {
            Response response = new Response();
            try
            {
                List<Fixtures> fixtures = new SeriesRepository().GetSeriesFixtures(_db, series_id, stage_id);
                if (fixtures.Any())
                {
                    if (onlyUpcoming)
                    {
                        fixtures = fixtures.Where(el => el.match_info?.status == "NS").Take(5).ToList();
                    }
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, fixtures);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, fixtures);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }
        #endregion

        #region Series Standings
        [HttpGet]
        [Route("standings")]
        public Response GetStandings(long series_id, long stage_id)
        {
            Response response = new Response();
            try
            {
                List<Standings> standings = new SeriesRepository().GetStandings(_db, series_id, stage_id);
                if (standings.Any())
                {
                    standings[0].standings = standings[0]?.standings?.OrderBy(sta => sta.position).ToList();
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, standings);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, standings);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }
        #endregion

        #region Series Teams
        [HttpGet]
        [Route("teams")]
        public Response GetSeriesTeams(long series_id)
        {
            Response response = new Response();
            try
            {
                List<SeriesTeams> seriesTeams = new SeriesRepository().GetSeriesTeams(_db, series_id);
                if (seriesTeams.Any())
                {
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, seriesTeams);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, seriesTeams);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }

        [HttpGet]
        [Route("lastseries")]
        public Response GetLastSeries(long series_id)
        {
            Response response = new Response();
            try
            {
                List<LastSeries> lastSeries = new SeriesRepository().GetLastSeries(_db, series_id);
                if (lastSeries.Any())
                {
                    int top10_3 = Convert.ToInt32(Math.Floor(lastSeries.Count * 0.03));
                    int top9_5_7 = Convert.ToInt32(Math.Floor(lastSeries.Count * 0.10));
                    int top9_20 = Convert.ToInt32(Math.Floor(lastSeries.Count * 0.30));
                    int top8_5_30 = Convert.ToInt32(Math.Floor(lastSeries.Count * 0.50));
                    int top8_10 = Convert.ToInt32(Math.Floor(lastSeries.Count * 0.60));
                    int top7_5_10 = Convert.ToInt32(Math.Floor(lastSeries.Count * 0.70));
                    int top7_10 = Convert.ToInt32(Math.Floor(lastSeries.Count * 0.80));
                    int top6_5_5 = Convert.ToInt32(Math.Floor(lastSeries.Count * 0.85));
                    int top6_5 = Convert.ToInt32(Math.Floor(lastSeries.Count * 0.90));
                    int top5_5_5 = Convert.ToInt32(Math.Floor(lastSeries.Count * 0.95));
                    int top5_5 = lastSeries.Count;
                    List<UpdateSeriesPoints> series_points = new List<UpdateSeriesPoints>();
                    for (int i = 0; i < lastSeries.Count; i++)
                    {
                        UpdateSeriesPoints updateSeriesPoints = new UpdateSeriesPoints();
                        updateSeriesPoints.series_id = series_id;
                        updateSeriesPoints.player_id = lastSeries[i].player_id;
                        if (i <= top10_3)
                        {
                            updateSeriesPoints.points = Convert.ToDecimal(10.0);
                        }
                        else if (i <= top9_5_7)
                        {
                            updateSeriesPoints.points = Convert.ToDecimal(9.5);
                        }
                        else if (i <= top9_20)
                        {
                            updateSeriesPoints.points = Convert.ToDecimal(9.0);
                        }
                        else if (i <= top8_5_30)
                        {
                            updateSeriesPoints.points = Convert.ToDecimal(8.5);
                        }
                        else if (i <= top8_10)
                        {
                            updateSeriesPoints.points = Convert.ToDecimal(8.0);
                        }
                        else if (i <= top7_5_10)
                        {
                            updateSeriesPoints.points = Convert.ToDecimal(7.5);
                        }
                        else if (i <= top7_10)
                        {
                            updateSeriesPoints.points = Convert.ToDecimal(7.0);
                        }
                        else if (i <= top6_5_5)
                        {
                            updateSeriesPoints.points = Convert.ToDecimal(6.5);
                        }
                        else if (i <= top6_5)
                        {
                            updateSeriesPoints.points = Convert.ToDecimal(6.0);
                        }
                        else if (i <= top5_5_5)
                        {
                            updateSeriesPoints.points = Convert.ToDecimal(5.5);
                        }
                        else if (i <= top5_5)
                        {
                            updateSeriesPoints.points = Convert.ToDecimal(5.0);
                        }
                        series_points.Add(updateSeriesPoints);
                    }
                    int rowsAffected = new SeriesRepository().UpdateSeriesTeamPlayers(_db, series_points);

                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, series_points);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, lastSeries);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }
        #endregion
    }
}
