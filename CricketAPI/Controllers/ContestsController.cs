﻿using CricketAPI.Entites;
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
                int rowsAffected = await new ContestsRepository().InsertContests(_db, contests);
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
