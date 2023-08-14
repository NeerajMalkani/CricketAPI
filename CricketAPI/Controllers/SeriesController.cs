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
        #endregion

        #region Get Series Fixtures
        [HttpGet]
        [Route("fixtures")]
        public Response GetSeriesFixtures([FromQuery] long series_id, string type)
        {
            Response response = new Response();
            try
            {
                List<Fixtures> fixtures = new SeriesRepository().GetSeriesFixtures(_db, series_id, type);
                if (fixtures.Any())
                {
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
        public Response GetStandings(long series_id)
        {
            Response response = new Response();
            try
            {
                List<Standings> standings = new SeriesRepository().GetStandings(_db, series_id);
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
        #endregion
    }
}
