﻿using CricketAPI.Helpers;
using CricketAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CricketAPI.Controllers
{
    [Route("api/teams")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly DataContext _db;

        public TeamsController(DataContext dbContext)
        {
            _db = dbContext;
        }

        #region Get Teams
        [HttpGet]
        [Route("list")]
        public Response GetTeams()
        {
            Response response = new Response();
            try
            {
                List<Teams> teams = new TeamsRepository().GetTeams(_db);
                if (teams.Any())
                {
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, teams);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, teams);
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
        public Response GetTeamInfo(long id)
        {
            Response response = new Response();
            try
            {
                List<Teams> teams = new TeamsRepository().GetTeamInfo(_db, id);
                if (teams.Any())
                {
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, teams);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, teams);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }

        [HttpGet]
        [Route("search")]
        public Response GetSearchTeam(string team_name)
        {
            Response response = new Response();
            try
            {
                List<Teams> teams = new TeamsRepository().GetSearchTeam(_db, team_name);
                if (teams.Any())
                {
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, teams);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, teams);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }
        #endregion

        #region Get Series Team Lineup
        [HttpGet]
        [Route("serieslineup")]
        public Response GetLineup([FromQuery] long fixture_id)
        {
            Response response = new Response();
            try
            {
                FixturesTeamLineup? fixturesTeamLineup = new TeamsRepository().GetLineup(_db, fixture_id);
                List<FixturesTeamLineup> fixturesTeamLineups = new List<FixturesTeamLineup>();
                if (fixturesTeamLineup != null && fixturesTeamLineup.teamlineup != null && fixturesTeamLineup.teamlineup.Count > 1)
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
    }
}