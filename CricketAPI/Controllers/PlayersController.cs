using CricketAPI.Entites;
using CricketAPI.Helpers;
using CricketAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CricketAPI.Controllers
{
    [Route("api/players")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly DataContext _db;

        public PlayersController(DataContext dbContext)
        {
            _db = dbContext;
        }

        #region Get Player info
        [HttpGet]
        [Route("info")]
        public Response GetTeamInfo(long id)
        {
            Response response = new Response();
            try
            {
                List<Players> players = new PlayersRepository().GetPlayerInfo(_db, id);
                if (players.Any())
                {
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, players);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, players);
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
        public Response GetSearchPlayer(string player_name)
        {
            Response response = new Response();
            try
            {
                List<Players> players = new PlayersRepository().GetSearchPlayer(_db, player_name);
                if (players.Any())
                {
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, players);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, players);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }
        #endregion

        #region Batting Stats
        [HttpGet]
        [Route("battingstats")]
        public Response GetPlayerBattingStats(long series_id, long player_id)
        {
            Response response = new Response();
            try
            {
                List<PlayerBattingStats> playerBattingStats = new PlayersRepository().GetPlayerBattingStats(_db, series_id, player_id);
                if (playerBattingStats.Any())
                {
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, playerBattingStats);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, playerBattingStats);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }
        #endregion

        #region Bowling Stats
        [HttpGet]
        [Route("bowlingstats")]
        public Response GetPlayerBowlingStats(long series_id, long player_id)
        {
            Response response = new Response();
            try
            {
                List<PlayerBowlingStats> playerBowlingStats = new PlayersRepository().GetPlayerBowlingStats(_db, series_id, player_id);
                if (playerBowlingStats.Any())
                {
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, playerBowlingStats);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, playerBowlingStats);
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
