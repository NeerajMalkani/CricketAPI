using CricketAPI.Entites;
using CricketAPI.Helpers;
using CricketAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CricketAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _db;

        public UserController(DataContext dbContext)
        {
            _db = dbContext;
        }

        #region User teams
        [HttpPost]
        [Route("teams")]
        public async Task<Response> InsertUserTeamAsync([FromBody] UserSaveTeamRequest userSaveTeamRequest)
        {
            Response response = new Response();
            try
            {
                UserSaveTeamResponse userSaveTeamResponse = await new UserRepository().InsertUserTeam(_db, userSaveTeamRequest);
                List<UserSaveTeamResponse> userSaveTeamResponses = new List<UserSaveTeamResponse>();
                if (userSaveTeamResponse.rowsAffected > 0)
                {
                    userSaveTeamResponses.Add(userSaveTeamResponse);
                    response.Data = userSaveTeamResponses;
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, userSaveTeamResponses);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, userSaveTeamResponses);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }

        [HttpGet]
        [Route("teams")]
        public Response GetUserTeamPlayers([FromQuery] UserTeamRequest userTeamRequest)
        {
            Response response = new Response();
            try
            {
                UserTeamList? userTeamList = new UserRepository().GetUserTeam(_db, userTeamRequest);
                List<UserTeamList> userTeamLists = new List<UserTeamList>();
                if (userTeamList != null && userTeamList.teams != null)
                {
                    userTeamLists.Add(userTeamList);
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, userTeamLists);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, userTeamLists);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }

        [HttpGet]
        [Route("teamswithplayers")]
        public Response GetUserTeamWithPlayers([FromQuery] UserTeamRequest userTeamRequest)
        {
            Response response = new Response();
            try
            {
                UserTeamWithPlayers? userTeamWithPlayers = new UserRepository().GetUserTeamWithPlayers(_db, userTeamRequest);
                List<UserTeamWithPlayers> userTeamsWithPlayers = new List<UserTeamWithPlayers>();
                if (userTeamWithPlayers != null && userTeamWithPlayers.teams != null)
                {
                    userTeamsWithPlayers.Add(userTeamWithPlayers);
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, userTeamsWithPlayers);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, userTeamsWithPlayers);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }

        [HttpGet]
        [Route("teams/players")]
        public Response GetUserTeam([FromQuery] UserTeamRequest userTeamRequest)
        {
            Response response = new Response();
            try
            {
                UserTeamLineup? userTeamLineup = new UserRepository().GetUserTeamPlayers(_db, userTeamRequest);
                List<UserTeamLineup> userTeamLineups = new List<UserTeamLineup>();
                if (userTeamLineup != null && userTeamLineup.teamlineup != null)
                {
                    userTeamLineups.Add(userTeamLineup);
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, userTeamLineups);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, userTeamLineups);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }

        [HttpPut]
        [Route("teams")]
        public async Task<Response> UpdateUserTeamAsync([FromBody] UserUpdateTeamRequest userUpdateTeamPlayers)
        {
            Response response = new Response();
            try
            {
                int rowsAffected = await new UserRepository().UpdateUserTeam(_db, userUpdateTeamPlayers.userTeamPlayers);
                if (rowsAffected > 0)
                {
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response);
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
