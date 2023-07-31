using CricketAPI.Helpers;
using CricketAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CricketAPI.Controllers
{
    [Route("api/fixtures")]
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
        [Route("list")]
        public Response GetFixtures([FromQuery] string type)
        {
            Response response = new Response();
            try
            {
                List<Fixtures> fixtures = new FixturesRepository().GetFixtures(_db, type);
                if (fixtures.Any())
                {
                    fixtures = type == "upcoming" ? fixtures.OrderBy(fix => fix.match_info?.starting_at).Take(12).ToList() : fixtures.OrderByDescending(fix => fix.match_info?.starting_at).Take(12).ToList();
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
        [Route("info")]
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
        [Route("scorecard")]
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
        [HttpGet]
        [Route("lineup")]
        public Response GetLineup([FromQuery] long fixture_id)
        {
            Response response = new Response();
            try
            {
                FixturesTeamLineup? fixturesTeamLineup = new FixturesRepository().GetLineup(_db, fixture_id);
                List<FixturesTeamLineup> fixturesTeamLineups = new List<FixturesTeamLineup>();
                if (fixturesTeamLineup != null)
                {
                    fixturesTeamLineups.Add(fixturesTeamLineup);
                }
                Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, fixturesTeamLineups);
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }
        #endregion

        #region Get Balls
        [HttpGet]
        [Route("balls")]
        public Response GetBalls([FromQuery] long fixture_id, string innings_id)
        {
            Response response = new Response();
            try
            {
                Fixtures_Balls? fixturesBalls = new FixturesRepository().GetBalls(_db, fixture_id, innings_id);
                List<Fixtures_Balls> fixturesBallsLst = new List<Fixtures_Balls>();
                if (fixturesBalls != null)
                {
                    fixturesBallsLst.Add(fixturesBalls);
                }
                Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, fixturesBallsLst);
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
