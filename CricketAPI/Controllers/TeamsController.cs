using CricketAPI.Helpers;
using CricketAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CricketAPI.Controllers
{
    [Route("api/[controller]")]
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
        [Route("getteams")]
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
        #endregion
    }
}