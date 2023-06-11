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

        #region Get Teams
        [HttpGet]
        [Route("getfixtures")]
        public Response GetFixtures()
        {
            Response response = new Response();
            try
            {
                List<Fixtures> fixtures = new FixturesRepository().GetFixtures(_db);
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
    }
}
