using CricketAPI.Helpers;
using CricketAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CricketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FixturesController : ControllerBase
    {
        private readonly DataContext _db;

        public FixturesController(DataContext dbContext)
        {
            _db = dbContext;
        }

        #region Get Fixtures
        [HttpGet]
        [Route("getfixtures")]
        public Response GetFixtures([FromQuery] string type)
        {
            Response response = new Response();
            try
            {
                List<Fixtures> fixtures = new FixturesRepository().GetFixtures(_db, type);
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

        [HttpGet]
        [Route("getfixturebyid")]
        public Response GetFixtureByID([FromQuery] long fixture_id)
        {
            Response response = new Response();
            try
            {
                List<Fixtures> fixtures = new FixturesRepository().GetFixtureByID(_db, fixture_id);
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

        #region Get Scorecard
        [HttpGet]
        [Route("getscorecard")]
        public Response GetScorecard([FromQuery] long fixture_id)
        {
            Response response = new Response();
            try
            {
                Scoreboard? scorecard = new FixturesRepository().GetScorecard(_db, fixture_id);
                List<Scoreboard> scores = new List<Scoreboard>();
                if (scorecard != null)
                {
                    scores.Add(scorecard);
                }
                Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, scores);
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }
        #endregion

        #region Get Team Lineup

        #endregion
    }
}
