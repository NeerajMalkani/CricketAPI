using CricketAPI.Entites;
using CricketAPI.Helpers;
using CricketAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CricketAPI.Controllers
{
    [Route("api/contests")]
    [ApiController]
    public class ContestsController : ControllerBase
    {
        private readonly DataContext _db;
        public ContestsController(DataContext dbContext)
        {
            _db = dbContext;
        }

        #region Prize Pool
        [HttpGet]
        [Route("prizepool")]
        public Response GetPrizePool([FromQuery] PrizePoolRequest prizePoolRequest)
        {
            Response response = new Response();
            try
            {
                List<PrizePool> prizePools = new ContestsRepository().CalculatePrizePool(prizePoolRequest);
                if (prizePools.Any())
                {
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, prizePools);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, prizePools);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }
        #endregion

        #region Contests
        [HttpPost]
        [Route("add")]
        public async Task<Response> InsertUserTeamAsync([FromBody] Contests contests)
        {
            Response response = new Response();
            try
            {
                long insertedContestID = await new ContestsRepository().InsertContests(_db, contests);
                if (insertedContestID > 0)
                {
                    List<ContestResponse> contestResponses = new List<ContestResponse>();
                    contestResponses.Add(new ContestResponse()
                    {
                        contest_id = insertedContestID,
                    });
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, contestResponses);
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

        [HttpPost]
        [Route("user/add")]
        public async Task<Response> InsertUserContestAsync([FromBody] UserJoinedContests userJoinedContests)
        {
            Response response = new Response();
            try
            {
                int rowsAffected = await new ContestsRepository().InsertUserContests(_db, userJoinedContests);
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
        [Route("list")]
        public Response GetContests([FromQuery] long fixture_id)
        {
            Response response = new Response();
            try
            {
                List<Contests> contests = new ContestsRepository().GetContests(_db, fixture_id);
                if (contests.Any())
                {
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, contests);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, contests);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }

        [HttpGet]
        [Route("user/list")]
        public Response GetUserContests([FromQuery] ContestRequest contestRequest)
        {
            Response response = new Response();
            try
            {
                List<Contests> contests = new ContestsRepository().GetUserContests(_db, contestRequest);
                if (contests.Any())
                {
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, contests);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, contests);
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
