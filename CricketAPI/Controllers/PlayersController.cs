using CricketAPI.Helpers;
using CricketAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CricketAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly DataContext _db;

        public PlayersController(DataContext dbContext)
        {
            _db = dbContext;
        }

        #region Get Players
        [HttpGet]
        [Route("getplayers")]
        public Response GetContractorMyServices()
        {
            Response response = new Response();
            try
            {
                List<Continents> continents = new PlayersRepository().GetPlayers(_db);
                if (continents.Any())
                {
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, continents);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, continents);
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