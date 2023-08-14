using CricketAPI.Entites;
using CricketAPI.Helpers;
using CricketAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CricketAPI.Controllers
{
    [Route("api/iccrankings")]
    [ApiController]
    public class ICCRankingsController : ControllerBase
    {
        private readonly DataContext _db;

        public ICCRankingsController(DataContext dbContext)
        {
            _db = dbContext;
        }

        #region ICC Rankings
        [HttpGet]
        [Route("list")]
        public Response GetICCRankings(string type, string gender)
        {
            Response response = new Response();
            try
            {
                List<ICCRankings> iccRankings = new ICCRankingsRepository().GetICCRankings(_db, type, gender);
                if (iccRankings.Any())
                {
                    iccRankings[0].rankings = iccRankings[0]?.rankings?.OrderBy(rank => rank.position).ToList();
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, iccRankings);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, iccRankings);
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
