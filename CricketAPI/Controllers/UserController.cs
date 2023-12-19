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
        public async Task<Response> InsertUserTeamAsync([FromBody] List<UserTeam> userTeams)
        {
            Response response = new Response();
            try
            {
                int rowsAffected = await new UserRepository().InsertUserTeam(_db, userTeams);
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

        [HttpGet]
        [Route("teams/list")]
        public Response GetUserTeam([FromQuery] UserTeamRequest userTeamRequest)
        {
            Response response = new Response();
            try
            {
                UserTeamLineup? userTeamLineup = new UserRepository().GetUserTeam(_db, userTeamRequest);
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
        public async Task<Response> UpdateUserTeamAsync([FromBody] UserTeam userTeam)
        {
            Response response = new Response();
            try
            {
                int rowsAffected = await new UserRepository().UpdateUserTeam(_db, userTeam);
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
