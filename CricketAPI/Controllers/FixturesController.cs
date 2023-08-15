using CricketAPI.Helpers;
using CricketAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.RegularExpressions;

namespace CricketAPI.Controllers
{
    [Route("api/fixtures")]
    [ApiController]
    public class FixturesController : ControllerBase
    {
        private readonly DataContext _db;

        public FixturesController(DataContext dbContext)
        {
            _db = dbContext;
        }

        #region Get Fixtures
        [HttpGet]
        [Route("list")]
        public Response GetFixtures([FromQuery] string type)
        {
            Response response = new Response();
            try
            {
                List<Fixtures> fixtures = new FixturesRepository().GetFixtures(_db, type);
                if (fixtures.Any())
                {
                    fixtures = type == "upcoming" ? fixtures.OrderBy(fix => fix.match_info?.starting_at).Take(12).ToList() : fixtures.OrderByDescending(fix => fix.match_info?.starting_at).Take(12).ToList();
                    foreach (Fixtures fixture in fixtures)
                    {
                        if (type == "recent" || type == "live")
                        {
                            int? score = fixture.match_score?[fixture.match_score.Count - 1].score;
                            List<string>? overs = fixture.match_score?[fixture.match_score.Count - 1]?.overs?.Split(".").ToList();
                            int? over = (Convert.ToInt16(overs?[0]) * 6) + (overs != null && overs?.Count > 1 ? Convert.ToInt16(overs?[1]) : 0);
                            if (fixture.match_info != null)
                            {
                                fixture.match_info.rpc_overs = Math.Round((Convert.ToDecimal(score) / Convert.ToDecimal(over)) * 6, 2).ToString();
                            }
                        }
                        if (fixture.match_info?.note?.Contains("Target") == true)
                        {
                            int target = Convert.ToInt16(Regex.Match(fixture.match_info.note, @"\d+").Value);
                            int? current_score = fixture.match_score?[fixture.match_score.Count - 1].score;
                            int? remaining_score = target - current_score;
                            List<string>? current_overs_lst = fixture.match_score?[fixture.match_score.Count - 1]?.overs?.Split(".").ToList();
                            int? current_overs = (Convert.ToInt16(current_overs_lst?[0]) * 6) + (current_overs_lst != null && current_overs_lst?.Count > 1 ? Convert.ToInt16(current_overs_lst?[1]) : 0);
                            int? remaining_overs = (fixture.match_info?.total_overs_played * 6) - current_overs;
                            if (fixture.match_info != null)
                            {
                                if (remaining_score > 0)
                                {
                                    fixture.match_info.rpc_target = Math.Round((Convert.ToDecimal(remaining_score) / Convert.ToDecimal(remaining_overs)) * 6, 2).ToString();
                                }
                                string? battingTeam = fixture.match_score?[fixture.match_score.Count - 1].team_id == fixture.match_info.localteam?.id ? fixture.match_info.localteam?.name : fixture.match_info.visitorteam?.name;
                                fixture.match_info.note = battingTeam + " requires " + remaining_score + (remaining_overs == 1 ? " run" : " runs") + " in " + remaining_overs + (remaining_overs == 1 ? " ball" : " balls");
                            }
                        }
                        else if (string.IsNullOrEmpty(fixture.match_info?.note) && fixture.match_info != null)
                        {
                            string? tossWinningTeam = fixture.match_info.toss_won_team_id == fixture.match_info.localteam?.id ? fixture.match_info.localteam?.name : fixture.match_info.visitorteam?.name;
                            fixture.match_info.note = tossWinningTeam + " won the toss and elected " + fixture.match_info.elected;
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
        [Route("info")]
        public Response GetFixtureByID([FromQuery] long fixture_id)
        {
            Response response = new Response();
            try
            {
                List<Fixtures> fixtures = new FixturesRepository().GetFixtureByID(_db, fixture_id);
                if (fixtures.Any())
                {
                    if (fixtures[0]?.match_info?.status != "NS")
                    {
                        foreach (Fixtures fixture in fixtures)
                        {
                            int? score = fixture.match_score?[fixture.match_score.Count - 1].score;
                            List<string>? overs = fixture.match_score?[fixture.match_score.Count - 1]?.overs?.Split(".").ToList();
                            int? over = (Convert.ToInt16(overs?[0]) * 6) + (overs != null && overs?.Count > 1 ? Convert.ToInt16(overs?[1]) : 0);
                            if (fixture.match_info != null)
                            {
                                fixture.match_info.rpc_overs = Math.Round((Convert.ToDecimal(score) / Convert.ToDecimal(over)) * 6, 2).ToString();
                            }
                            if (fixture.match_info?.note?.Contains("Target") == true)
                            {
                                int target = Convert.ToInt16(Regex.Match(fixture.match_info.note, @"\d+").Value);
                                int? current_score = fixture.match_score?[fixture.match_score.Count - 1].score;
                                int? remaining_score = target - current_score;
                                List<string>? current_overs_lst = fixture.match_score?[fixture.match_score.Count - 1]?.overs?.Split(".").ToList();
                                int? current_overs = (Convert.ToInt16(current_overs_lst?[0]) * 6) + (current_overs_lst != null && current_overs_lst?.Count > 1 ? Convert.ToInt16(current_overs_lst?[1]) : 0);
                                int? remaining_overs = (fixture.match_info?.total_overs_played * 6) - current_overs;
                                if (fixture.match_info != null)
                                {
                                    if (remaining_score > 0)
                                    {
                                        fixture.match_info.rpc_target = Math.Round((Convert.ToDecimal(remaining_score) / Convert.ToDecimal(remaining_overs)) * 6, 2).ToString();
                                    }
                                    string? battingTeam = fixture.match_score?[fixture.match_score.Count - 1].team_id == fixture.match_info.localteam?.id ? fixture.match_info.localteam?.name : fixture.match_info.visitorteam?.name;
                                    fixture.match_info.note = battingTeam + " requires " + remaining_score + (remaining_overs == 1 ? " run" : " runs") + " in " + remaining_overs + (remaining_overs == 1 ? " ball" : " balls");
                                }
                            }
                            else if (string.IsNullOrEmpty(fixture.match_info?.note) && fixture.match_info != null)
                            {
                                string? tossWinningTeam = fixture.match_info.toss_won_team_id == fixture.match_info.localteam?.id ? fixture.match_info.localteam?.name : fixture.match_info.visitorteam?.name;
                                fixture.match_info.note = tossWinningTeam + " won the toss and elected " + fixture.match_info.elected;
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
        #endregion

        #region Get Scorecard
        [HttpGet]
        [Route("scorecard")]
        public Response GetScorecard([FromQuery] long fixture_id)
        {
            Response response = new Response();
            try
            {
                Scoreboard? scorecard = new FixturesRepository().GetScorecard(_db, fixture_id);
                List<Scoreboard> scores = new List<Scoreboard>();
                if (scorecard != null)
                {
                    scores.Add(scorecard);
                }
                Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, scores);
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }
        #endregion

        #region Get Team Lineup
        [HttpGet]
        [Route("lineup")]
        public Response GetLineup([FromQuery] long fixture_id)
        {
            Response response = new Response();
            try
            {
                FixturesTeamLineup? fixturesTeamLineup = new FixturesRepository().GetLineup(_db, fixture_id);
                List<FixturesTeamLineup> fixturesTeamLineups = new List<FixturesTeamLineup>();
                if (fixturesTeamLineup != null)
                {
                    fixturesTeamLineups.Add(fixturesTeamLineup);
                }
                Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, fixturesTeamLineups);
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }
        #endregion

        #region Get Balls
        [HttpGet]
        [Route("balls")]
        public Response GetBalls([FromQuery] long fixture_id, string innings_id)
        {
            Response response = new Response();
            try
            {
                Fixtures_Balls? fixturesBalls = new FixturesRepository().GetBalls(_db, fixture_id, innings_id);
                List<Fixtures_Balls> fixturesBallsLst = new List<Fixtures_Balls>();
                if (fixturesBalls != null)
                {
                    fixturesBallsLst.Add(fixturesBalls);
                }
                Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, fixturesBallsLst);
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
