using CricketAPI.Entites;
using CricketAPI.Helpers;
using CricketAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.RegularExpressions;

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

        [HttpPut]
        [Route("userteam")]
        public async Task<Response> UpdateUserContestWithTeamAsync([FromBody] UserContestMapping userContestWithTeamRequest)
        {
            Response response = new Response();
            try
            {
                int rowsAffected = await new ContestsRepository().UpdateUserContestWithTeam(_db, userContestWithTeamRequest);
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

        [HttpDelete]
        [Route("userteam")]
        public async Task<Response> DeleteUserTeamForContest([FromBody] UserContestMapping userContestMapping)
        {
            Response response = new Response();
            try
            {
                int rowsAffected = await new ContestsRepository().DeleteUserTeamForContest(_db, userContestMapping);
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
        [Route("home")]
        public Response GetContestHome([FromQuery] ContestRequest contestRequest)
        {
            Response response = new Response();
            try
            {
                List<ContestsHome> contests = new ContestsRepository().GetContestsHome(_db, contestRequest);
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
        [Route("user/matches/list")]
        public Response GetFixtures([FromQuery] ContestFixtuesRequest contestFixtues)
        {
            Response response = new Response();
            try
            {
                List<Fixtures> fixtures = new ContestsRepository().GetContestFixtures(_db, contestFixtues);
                if (fixtures.Any())
                {
                    fixtures = contestFixtues.type == "upcoming" ? fixtures.OrderBy(fix => fix.match_info?.starting_at).Take(12).ToList() : fixtures.OrderByDescending(fix => fix.match_info?.starting_at).Take(12).ToList();
                    foreach (Fixtures fixture in fixtures)
                    {
                        if (contestFixtues.type == "recent" || contestFixtues.type == "live")
                        {
                            int? score = fixture.match_score?[fixture.match_score.Count - 1].score;
                            List<string>? overs = fixture.match_score?[fixture.match_score.Count - 1]?.overs?.Split(".").ToList();
                            if (overs != null)
                            {
                                int? over = (Convert.ToInt16(overs?[0]) * 6) + (overs != null && overs?.Count > 1 ? Convert.ToInt16(overs?[1]) : 0);
                                if (fixture.match_info != null)
                                {
                                    fixture.match_info.rpc_overs = Math.Round((Convert.ToDecimal(score) / Convert.ToDecimal(over)) * 6, 2).ToString();
                                }
                            }
                        }
                        if (fixture.match_info?.note?.Contains("Target") == true && (fixture.match_score?.Count > 1 && Convert.ToDouble(fixture.match_score?[fixture.match_score.Count - 1].overs) > 0))
                        {
                            int target = Convert.ToInt16(Regex.Match(fixture.match_info.note, @"\d+").Value);
                            int? current_score = fixture.match_score?[fixture.match_score.Count - 1].score;
                            int? remaining_score = target - current_score;
                            List<string>? current_overs_lst = fixture.match_score?[fixture.match_score.Count - 1]?.overs?.Split(".").ToList();
                            int? current_overs = (Convert.ToInt16(current_overs_lst?[0]) * 6) + (current_overs_lst != null && current_overs_lst?.Count > 1 ? Convert.ToInt16(current_overs_lst?[1]) : 0);
                            int? remaining_overs = (fixture.match_info?.total_overs_played * 6) - current_overs;
                            if (fixture.match_info != null)
                            {
                                if (remaining_score > 0 && remaining_overs > 0)
                                {
                                    fixture.match_info.rpc_target = Math.Round((Convert.ToDecimal(remaining_score) / Convert.ToDecimal(remaining_overs)) * 6, 2).ToString();
                                }
                                string? battingTeam = fixture.match_score?[fixture.match_score.Count - 1].team_id == fixture.match_info.localteam?.id ? fixture.match_info.localteam?.name : fixture.match_info.visitorteam?.name;
                                fixture.match_info.note = battingTeam + " requires " + remaining_score + (remaining_overs == 1 ? " run" : " runs") + " in " + remaining_overs + (remaining_overs == 1 ? " ball" : " balls");
                            }
                        }
                        else if (string.IsNullOrEmpty(fixture.match_info?.note) && fixture.match_info != null)
                        {
                            if (fixture.match_info.toss_won_team_id != null && fixture.match_info.toss_won_team_id > 0)
                            {
                                string? tossWinningTeam = fixture.match_info.toss_won_team_id == fixture.match_info.localteam?.id ? fixture.match_info.localteam?.name : fixture.match_info.visitorteam?.name;
                                fixture.match_info.note = tossWinningTeam + " won the toss and elected " + fixture.match_info.elected;
                            }
                            else
                            {
                                fixture.match_info.note = "The match has not started yet";
                            }
                        }
                    }
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

        [HttpGet]
        [Route("user/list")]
        public Response GetUserContests([FromQuery] ContestRequest contestRequest)
        {
            Response response = new Response();
            try
            {
                List<GetContestResponse> contests = new ContestsRepository().GetUserContests(_db, contestRequest);
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
        [Route("user/winnings")]
        public Response GetUserWinnings([FromQuery] long contest_id)
        {
            Response response = new Response();
            try
            {
                Contests? contest = new ContestsRepository().GetUserContest(_db, contest_id);
                if (contest != null)
                {
                    PrizePoolRequest prizePoolRequest = new PrizePoolRequest()
                    {
                        entryFees = contest.entry_fees,
                        spots = contest.spots
                    };
                    List<PrizePool> prizePools = new ContestsRepository().CalculatePrizePool(prizePoolRequest);
                    if (prizePools.Any())
                    {
                        prizePools = prizePools.Where(el => el.breakPoint == contest.number_of_winners).ToList();
                        Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, prizePools);
                    }
                    else
                    {
                        Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, prizePools);
                    }
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, new List<Contests>());
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }

        [HttpGet]
        [Route("user/contest")]
        public Response GetUserContest([FromQuery] long contest_id)
        {
            Response response = new Response();
            try
            {
                Contests? contest = new ContestsRepository().GetUserContest(_db, contest_id);
                if (contest != null)
                {
                    List<Contests> contests = new List<Contests>();
                    contests.Add(contest);
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, contests);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, new List<Contests>());
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }

        [HttpGet]
        [Route("user/leaderboard")]
        public Response GetUserContestsLeaderboard([FromQuery] ContestLeaderboardRequest contestLeaderboardRequest)
        {
            Response response = new Response();
            try
            {
                List<ContestLeaderboard> contestLeaderboards = new List<ContestLeaderboard>();
                ContestLeaderboard contestsLeaderboard = new ContestsRepository().GetUserContestLeaderboard(_db, contestLeaderboardRequest);
                if (contestsLeaderboard != null)
                {
                    contestsLeaderboard.leaderboard = contestsLeaderboard.leaderboard?.OrderByDescending(el => el.team_points).ToList();
                    contestLeaderboards.Add(contestsLeaderboard);
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, contestLeaderboards);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, contestLeaderboards);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }

        [HttpGet]
        [Route("user/stats")]
        public Response GetUserContestsStats([FromQuery] ContestUserStatsRequest contestLeaderboardRequest)
        {
            Response response = new Response();
            try
            {
                List<UserTeamStats> userTeamStats = new List<UserTeamStats>();
                UserTeamStats userTeamStat = new ContestsRepository().GetUserContestUserStats(_db, contestLeaderboardRequest);
                if (userTeamStat != null)
                {
                    userTeamStats.Add(userTeamStat);
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, userTeamStats);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, userTeamStats);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }

        [HttpGet]
        [Route("user/team/player/stats")]
        public Response GetUserTeamPlayerStats([FromQuery] UserTeamRequest userTeamRequest)
        {
            Response response = new Response();
            try
            {
                List<UserTeamStats> userTeamStats = new List<UserTeamStats>();
                UserTeamStats userTeamStat = new ContestsRepository().GetUserTeamPlayerStats(_db, userTeamRequest);
                if (userTeamStat != null)
                {
                    userTeamStats.Add(userTeamStat);
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, userTeamStats);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, userTeamStats);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }

        [HttpGet]
        [Route("user/team/graphs")]
        public Response GetUserTeamStats([FromQuery] ContestUserTeamStatsRequest contestUserTeamStats)
        {
            Response response = new Response();
            try
            {
                List<UserTeamPointsStats>? userTeamStats = new ContestsRepository().GetUserTeamStats(_db, contestUserTeamStats);
                if (userTeamStats != null)
                {
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, userTeamStats);
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
