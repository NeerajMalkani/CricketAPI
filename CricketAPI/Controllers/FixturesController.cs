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
                            if (overs != null)
                            {
                                int? over = (Convert.ToInt16(overs?[0]) * 6) + (overs != null && overs?.Count > 1 ? Convert.ToInt16(overs?[1]) : 0);
                                if (fixture.match_info != null && Convert.ToDecimal(over) > 0)
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
                                fixture.match_info.note = battingTeam + " requires " + (remaining_score < 0 ? 0 : remaining_score) + (remaining_overs == 1 ? " run" : " runs") + " in " + remaining_overs + (remaining_overs == 1 ? " ball" : " balls");
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
                            if (overs != null)
                            {
                                int? over = (Convert.ToInt16(overs?[0]) * 6) + (overs != null && overs?.Count > 1 ? Convert.ToInt16(overs?[1]) : 0);
                                if (fixture.match_info != null && Convert.ToDecimal(over) > 0)
                                {
                                    fixture.match_info.rpc_overs = Math.Round((Convert.ToDecimal(score) / Convert.ToDecimal(over)) * 6, 2).ToString();
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
                if (scorecard != null && scorecard.scorecard != null)
                {
                    foreach (Scorecard scoreboard in scorecard.scorecard)
                    {
                        scoreboard.batting = scoreboard?.batting1?[0];
                    }
                }
                FixturesTeamLineup? fixturesTeamLineup = new FixturesRepository().GetLineup(_db, fixture_id);
                if (fixturesTeamLineup != null && fixturesTeamLineup.teamlineup != null && scorecard != null && scorecard.scorecard != null && scorecard.scorecard.Count > 0)
                {
                    Teamlineup? teamlineup1 = fixturesTeamLineup.teamlineup.Find(team => team.team_id == scorecard.scorecard[0].team_id);
                    if (teamlineup1 != null)
                    {
                        teamlineup1.team = teamlineup1.team?.OrderBy(e => e.sort).ToList();
                        scorecard.scorecard[0].yetToBat = teamlineup1.team;
                    }
                    if (scorecard.scorecard.Count > 1)
                    {
                        Teamlineup? teamlineup2 = fixturesTeamLineup.teamlineup.Find(team => team.team_id == scorecard.scorecard[1].team_id);
                        if (teamlineup2 != null)
                        {
                            teamlineup2.team = teamlineup2.team?.OrderBy(e => e.sort).ToList();
                            scorecard.scorecard[1].yetToBat = teamlineup2.team;
                        }
                    }
                }
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
                if (fixturesTeamLineup != null && fixturesTeamLineup.teamlineup != null)
                {
                    foreach (Teamlineup teamlineup in fixturesTeamLineup.teamlineup)
                    {
                        if (teamlineup != null && teamlineup.team != null)
                        {
                            teamlineup.team = teamlineup.team.OrderBy(team => team?.sort).ToList();
                        }
                    }
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
                if (fixturesBalls != null)
                {

                    int? score = fixturesBalls.match_score?[fixturesBalls.match_score.Count - 1].score;
                    List<string>? overs = fixturesBalls.match_score?[fixturesBalls.match_score.Count - 1]?.overs?.Split(".").ToList();
                    if (overs != null)
                    {
                        int? over = (Convert.ToInt16(overs?[0]) * 6) + (overs != null && overs?.Count > 1 ? Convert.ToInt16(overs?[1]) : 0);
                        if (fixturesBalls != null && Convert.ToDecimal(over) > 0)
                        {
                            fixturesBalls.rpc_overs = Math.Round((Convert.ToDecimal(score) / Convert.ToDecimal(over)) * 6, 2).ToString();
                        }
                    }

                    if (fixturesBalls?.note?.Contains("Target") == true && (fixturesBalls?.match_score?.Count > 1 && Convert.ToDouble(fixturesBalls?.match_score?[fixturesBalls.match_score.Count - 1].overs) > 0))
                    {
                        if (fixturesBalls != null)
                        {
                            int target = Convert.ToInt16(Regex.Match(fixturesBalls.note, @"\d+").Value);
                            int? current_score = fixturesBalls?.match_score?[fixturesBalls.match_score.Count - 1].score;
                            int? remaining_score = target - current_score;
                            List<string>? current_overs_lst = fixturesBalls?.match_score?[fixturesBalls.match_score.Count - 1]?.overs?.Split(".").ToList();
                            int? current_overs = (Convert.ToInt16(current_overs_lst?[0]) * 6) + (current_overs_lst != null && current_overs_lst?.Count > 1 ? Convert.ToInt16(current_overs_lst?[1]) : 0);
                            int? remaining_overs = (fixturesBalls?.total_overs_played * 6) - current_overs;

                            if (fixturesBalls != null && remaining_score > 0 && remaining_overs > 0)
                            {
                                fixturesBalls.rpc_target = Math.Round((Convert.ToDecimal(remaining_score) / Convert.ToDecimal(remaining_overs)) * 6, 2).ToString();
                            }
                            string? battingTeam = fixturesBalls?.match_score?[fixturesBalls.match_score.Count - 1].team_id == fixturesBalls?.localteam?.id ? fixturesBalls?.localteam?.name : fixturesBalls?.visitorteam?.name;
                            if (fixturesBalls != null)
                            {
                                fixturesBalls.note = battingTeam + " requires " + remaining_score + (remaining_overs == 1 ? " run" : " runs") + " in " + remaining_overs + (remaining_overs == 1 ? " ball" : " balls");
                            }
                        }
                    }

                    List<Bowling>? objBowling = new();

                    FixturesBalls? objLastFB = fixturesBalls?.balls?.Last();
                    string? LastBowlerName = Convert.ToString(objLastFB?.bowler?.name);
                    decimal LastBall = Convert.ToDecimal(objLastFB?.ball);
                    Bowling? miniscoreBolwer1 = fixturesBalls?.miniscore?.bowling?.Find(a => a.bowler == LastBowlerName);
                    if (miniscoreBolwer1 != null)
                    {
                        objBowling.Add(miniscoreBolwer1);
                    }

                    if (LastBall >= 1)
                    {
                        FixturesBalls? objSecondLastFB = fixturesBalls?.balls?.Find(e => e.ball == (Convert.ToString(Math.Floor(LastBall) - 1) + ".6"));
                        string? SecondLastBowlerName = Convert.ToString(objSecondLastFB?.bowler?.name);
                        Bowling? miniscoreBolwer2 = fixturesBalls?.miniscore?.bowling?.Find(a => a.bowler == SecondLastBowlerName);

                        if (miniscoreBolwer2 != null)
                        {
                            objBowling.Add(miniscoreBolwer2);
                        }
                    }
                    if (fixturesBalls != null && fixturesBalls.miniscore != null)
                    {
                        fixturesBalls.miniscore.bowling = objBowling;
                    }

                    if (fixturesBalls != null && fixturesBalls.balls != null)
                    {
                        string currOver = "0";
                        int overRuns = 0;
                        int totalRuns = 0;
                        int totalWickets = 0;
                        foreach (FixturesBalls ball in fixturesBalls.balls)
                        {
                            string? currBall = ball?.ball;
                            if (currBall != null)
                            {
                                string traverseOver = currBall.Split(".")[0];
                                if (traverseOver != currOver)
                                {
                                    overRuns = 0;
                                    currOver = traverseOver;
                                }

                                if (ball?.score?.is_wicket == "1")
                                {
                                    totalWickets++;
                                }

                                overRuns += Convert.ToInt32(ball?.score?.runs);
                                totalRuns += Convert.ToInt32(ball?.score?.runs);
                                if (ball?.score?.leg_bye == true || ball?.score?.bye == true)
                                {
                                    totalRuns += Convert.ToInt32(ball?.score?.name?.Split(" ")[0]);
                                    overRuns += Convert.ToInt32(ball?.score?.name?.Split(" ")[0]);
                                }

                                if (ball != null)
                                {
                                    ball.total_wickets = totalWickets;
                                    ball.total_score = totalRuns;
                                    ball.over_score = overRuns;
                                }
                            }
                        }
                    }

                    List<FixturesBalls>? arrLastWicketFB = fixturesBalls?.balls?.FindAll(e => e.score?.is_wicket == "1");
                    if (arrLastWicketFB != null && arrLastWicketFB.Any())
                    {
                        FixturesBalls objLastWicketFB = arrLastWicketFB.Last();
                        LastWicket lstWicket = new LastWicket();
                        lstWicket.playerName = objLastWicketFB?.batsman_out;
                        lstWicket.wicketNumber = objLastWicketFB?.total_wickets;
                        lstWicket.overNumber = objLastWicketFB?.ball;
                        lstWicket.score = objLastWicketFB?.total_score;

                        if (fixturesBalls != null)
                        {
                            fixturesBalls.last_wicket = lstWicket;
                        }
                    }

                    List<Fixtures_Balls> fixturesBallsLst = new List<Fixtures_Balls>();
                    if (fixturesBalls != null)
                    {
                        fixturesBallsLst.Add(fixturesBalls);
                    }
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, fixturesBallsLst);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "No data", out response);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }

        [HttpGet]
        [Route("ballswithoffset")]
        public Response GetBallsWithOffset([FromQuery] long fixture_id, string innings_id, int start_index, int count)
        {
            Response response = new Response();
            try
            {
                Fixtures_Balls? fixturesBalls = new FixturesRepository().GetBallsWithOffset(_db, fixture_id, innings_id);
                if (fixturesBalls != null)
                {
                    int? score = fixturesBalls.match_score?[fixturesBalls.match_score.Count - 1].score;
                    List<string>? overs = fixturesBalls.match_score?[fixturesBalls.match_score.Count - 1]?.overs?.Split(".").ToList();
                    if (overs != null)
                    {
                        int? over = (Convert.ToInt16(overs?[0]) * 6) + (overs != null && overs?.Count > 1 ? Convert.ToInt16(overs?[1]) : 0);
                        if (fixturesBalls != null && Convert.ToDecimal(over) > 0)
                        {
                            fixturesBalls.rpc_overs = Math.Round((Convert.ToDecimal(score) / Convert.ToDecimal(over)) * 6, 2).ToString();
                        }
                    }

                    if (fixturesBalls?.note?.Contains("Target") == true && (fixturesBalls?.match_score?.Count > 1 && Convert.ToDouble(fixturesBalls?.match_score?[fixturesBalls.match_score.Count - 1].overs) > 0))
                    {
                        if (fixturesBalls != null)
                        {
                            int target = Convert.ToInt16(Regex.Match(fixturesBalls.note, @"\d+").Value);
                            int? current_score = fixturesBalls?.match_score?[fixturesBalls.match_score.Count - 1].score;
                            int? remaining_score = target - current_score;
                            List<string>? current_overs_lst = fixturesBalls?.match_score?[fixturesBalls.match_score.Count - 1]?.overs?.Split(".").ToList();
                            int? current_overs = (Convert.ToInt16(current_overs_lst?[0]) * 6) + (current_overs_lst != null && current_overs_lst?.Count > 1 ? Convert.ToInt16(current_overs_lst?[1]) : 0);
                            int? remaining_overs = (fixturesBalls?.total_overs_played * 6) - current_overs;

                            if (fixturesBalls != null && remaining_score > 0 && remaining_overs > 0)
                            {
                                fixturesBalls.rpc_target = Math.Round((Convert.ToDecimal(remaining_score) / Convert.ToDecimal(remaining_overs)) * 6, 2).ToString();
                            }
                            string? battingTeam = fixturesBalls?.match_score?[fixturesBalls.match_score.Count - 1].team_id == fixturesBalls?.localteam?.id ? fixturesBalls?.localteam?.name : fixturesBalls?.visitorteam?.name;
                            if (fixturesBalls != null)
                            {
                                fixturesBalls.note = battingTeam + " requires " + remaining_score + (remaining_overs == 1 ? " run" : " runs") + " in " + remaining_overs + (remaining_overs == 1 ? " ball" : " balls");
                            }
                        }
                    }

                    List<Bowling>? objBowling = new();

                    FixturesBalls? objLastFB = fixturesBalls?.balls?.Last();
                    string? LastBowlerName = Convert.ToString(objLastFB?.bowler?.name);
                    decimal LastBall = Convert.ToDecimal(objLastFB?.ball);
                    Bowling? miniscoreBolwer1 = fixturesBalls?.miniscore?.bowling?.Find(a => a.bowler == LastBowlerName);
                    if (miniscoreBolwer1 != null)
                    {
                        objBowling.Add(miniscoreBolwer1);
                    }

                    if (LastBall >= 1)
                    {
                        FixturesBalls? objSecondLastFB = fixturesBalls?.balls?.Find(e => e.ball == (Convert.ToString(Math.Floor(LastBall) - 1) + ".6"));
                        string? SecondLastBowlerName = Convert.ToString(objSecondLastFB?.bowler?.name);
                        Bowling? miniscoreBolwer2 = fixturesBalls?.miniscore?.bowling?.Find(a => a.bowler == SecondLastBowlerName);

                        if (miniscoreBolwer2 != null)
                        {
                            objBowling.Add(miniscoreBolwer2);
                        }
                    }
                    if (fixturesBalls != null && fixturesBalls.miniscore != null)
                    {
                        fixturesBalls.miniscore.bowling = objBowling;
                    }

                    if (fixturesBalls != null && fixturesBalls.balls != null)
                    {
                        string currOver = "0";
                        int overRuns = 0;
                        int totalRuns = 0;
                        int totalWickets = 0;
                        foreach (FixturesBalls ball in fixturesBalls.balls)
                        {
                            string? currBall = ball?.ball;
                            if (currBall != null)
                            {
                                string traverseOver = currBall.Split(".")[0];
                                if (traverseOver != currOver)
                                {
                                    overRuns = 0;
                                    currOver = traverseOver;
                                }

                                if (ball?.score?.is_wicket == "1")
                                {
                                    totalWickets++;
                                }

                                overRuns += Convert.ToInt32(ball?.score?.runs);
                                totalRuns += Convert.ToInt32(ball?.score?.runs);
                                if (ball?.score?.leg_bye == true || ball?.score?.bye == true)
                                {
                                    totalRuns += Convert.ToInt32(ball?.score?.name?.Split(" ")[0]);
                                    overRuns += Convert.ToInt32(ball?.score?.name?.Split(" ")[0]);
                                }

                                if (ball != null)
                                {
                                    ball.total_wickets = totalWickets;
                                    ball.total_score = totalRuns;
                                    ball.over_score = overRuns;
                                }
                            }
                        }
                    }

                    List<FixturesBalls>? arrLastWicketFB = fixturesBalls?.balls?.FindAll(e => e.score?.is_wicket == "1");
                    if (arrLastWicketFB != null && arrLastWicketFB.Any())
                    {
                        FixturesBalls objLastWicketFB = arrLastWicketFB.Last();
                        LastWicket lstWicket = new LastWicket();
                        lstWicket.playerName = objLastWicketFB?.batsman_out;
                        lstWicket.wicketNumber = objLastWicketFB?.total_wickets;
                        lstWicket.overNumber = objLastWicketFB?.ball;
                        lstWicket.score = objLastWicketFB?.total_score;

                        if (fixturesBalls != null)
                        {
                            fixturesBalls.last_wicket = lstWicket;
                        }
                    }

                    List<Fixtures_Balls> fixturesBallsLst = new List<Fixtures_Balls>();
                    if (fixturesBalls != null)
                    {
                        fixturesBallsLst.Add(fixturesBalls);
                    }

                    fixturesBallsLst[0].balls?.Reverse();
                    fixturesBallsLst[0].balls = fixturesBallsLst[0].balls?.Skip(start_index).Take(count).ToList();

                    if (fixturesBallsLst[0] != null && fixturesBallsLst[0].miniscore != null && fixturesBallsLst[0]?.miniscore?.batting != null)
                    {
                        FixturesBalls? objFirstBall = fixturesBalls?.balls?.First();
                        foreach (Batting batt in fixturesBallsLst[0].miniscore.batting)
                        {
                            batt.is_strike = (batt.batsman == objFirstBall?.batsman_strike?.name);
                        }
                    }


                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, fixturesBallsLst);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "No data", out response);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }

        [HttpGet]
        [Route("overswithoffset")]
        public Response GetOversWithOffset([FromQuery] long fixture_id, string innings_id, int start_index, int count)
        {
            Response response = new Response();
            try
            {
                Fixtures_Balls? fixturesBalls = new FixturesRepository().GetBallsWithOffset(_db, fixture_id, innings_id);
                if (fixturesBalls != null)
                {
                    int? score = fixturesBalls.match_score?[fixturesBalls.match_score.Count - 1].score;
                    List<string>? overs = fixturesBalls.match_score?[fixturesBalls.match_score.Count - 1]?.overs?.Split(".").ToList();
                    if (overs != null)
                    {
                        int? over = (Convert.ToInt16(overs?[0]) * 6) + (overs != null && overs?.Count > 1 ? Convert.ToInt16(overs?[1]) : 0);
                        if (fixturesBalls != null && Convert.ToDecimal(over) > 0)
                        {
                            fixturesBalls.rpc_overs = Math.Round((Convert.ToDecimal(score) / Convert.ToDecimal(over)) * 6, 2).ToString();
                        }
                    }

                    if (fixturesBalls?.note?.Contains("Target") == true && (fixturesBalls?.match_score?.Count > 1 && Convert.ToDouble(fixturesBalls?.match_score?[fixturesBalls.match_score.Count - 1].overs) > 0))
                    {
                        if (fixturesBalls != null)
                        {
                            int target = Convert.ToInt16(Regex.Match(fixturesBalls.note, @"\d+").Value);
                            int? current_score = fixturesBalls?.match_score?[fixturesBalls.match_score.Count - 1].score;
                            int? remaining_score = target - current_score;
                            List<string>? current_overs_lst = fixturesBalls?.match_score?[fixturesBalls.match_score.Count - 1]?.overs?.Split(".").ToList();
                            int? current_overs = (Convert.ToInt16(current_overs_lst?[0]) * 6) + (current_overs_lst != null && current_overs_lst?.Count > 1 ? Convert.ToInt16(current_overs_lst?[1]) : 0);
                            int? remaining_overs = (fixturesBalls?.total_overs_played * 6) - current_overs;

                            if (fixturesBalls != null && remaining_score > 0 && remaining_overs > 0)
                            {
                                fixturesBalls.rpc_target = Math.Round((Convert.ToDecimal(remaining_score) / Convert.ToDecimal(remaining_overs)) * 6, 2).ToString();
                            }
                            string? battingTeam = fixturesBalls?.match_score?[fixturesBalls.match_score.Count - 1].team_id == fixturesBalls?.localteam?.id ? fixturesBalls?.localteam?.name : fixturesBalls?.visitorteam?.name;
                            if (fixturesBalls != null)
                            {
                                fixturesBalls.note = battingTeam + " requires " + remaining_score + (remaining_overs == 1 ? " run" : " runs") + " in " + remaining_overs + (remaining_overs == 1 ? " ball" : " balls");
                            }
                        }
                    }

                    List<Bowling>? objBowling = new();

                    FixturesBalls? objLastFB = fixturesBalls?.balls?.Last();
                    string? LastBowlerName = Convert.ToString(objLastFB?.bowler?.name);
                    decimal LastBall = Convert.ToDecimal(objLastFB?.ball);
                    Bowling? miniscoreBolwer1 = fixturesBalls?.miniscore?.bowling?.Find(a => a.bowler == LastBowlerName);
                    if (miniscoreBolwer1 != null)
                    {
                        objBowling.Add(miniscoreBolwer1);
                    }

                    if (LastBall >= 1)
                    {
                        FixturesBalls? objSecondLastFB = fixturesBalls?.balls?.Find(e => e.ball == (Convert.ToString(Math.Floor(LastBall) - 1) + ".6"));
                        string? SecondLastBowlerName = Convert.ToString(objSecondLastFB?.bowler?.name);
                        Bowling? miniscoreBolwer2 = fixturesBalls?.miniscore?.bowling?.Find(a => a.bowler == SecondLastBowlerName);

                        if (miniscoreBolwer2 != null)
                        {
                            objBowling.Add(miniscoreBolwer2);
                        }
                    }
                    if (fixturesBalls != null && fixturesBalls.miniscore != null)
                    {
                        fixturesBalls.miniscore.bowling = objBowling;
                    }

                    if (fixturesBalls != null && fixturesBalls.balls != null)
                    {
                        string currOver = "0";
                        int overRuns = 0;
                        int totalRuns = 0;
                        int totalWickets = 0;
                        foreach (FixturesBalls ball in fixturesBalls.balls)
                        {
                            string? currBall = ball?.ball;
                            if (currBall != null)
                            {
                                string traverseOver = currBall.Split(".")[0];
                                if (traverseOver != currOver)
                                {
                                    overRuns = 0;
                                    currOver = traverseOver;
                                }

                                if (ball?.score?.is_wicket == "1")
                                {
                                    totalWickets++;
                                }

                                overRuns += Convert.ToInt32(ball?.score?.runs);
                                totalRuns += Convert.ToInt32(ball?.score?.runs);
                                if (ball?.score?.leg_bye == true || ball?.score?.bye == true)
                                {
                                    totalRuns += Convert.ToInt32(ball?.score?.name?.Split(" ")[0]);
                                    overRuns += Convert.ToInt32(ball?.score?.name?.Split(" ")[0]);
                                }

                                if (ball != null)
                                {
                                    ball.total_wickets = totalWickets;
                                    ball.total_score = totalRuns;
                                    ball.over_score = overRuns;
                                }
                            }
                        }
                    }

                    List<FixturesBalls>? arrLastWicketFB = fixturesBalls?.balls?.FindAll(e => e.score?.is_wicket == "1");
                    if (arrLastWicketFB != null && arrLastWicketFB.Any())
                    {
                        FixturesBalls objLastWicketFB = arrLastWicketFB.Last();
                        LastWicket lstWicket = new LastWicket();
                        lstWicket.playerName = objLastWicketFB?.batsman_out;
                        lstWicket.wicketNumber = objLastWicketFB?.total_wickets;
                        lstWicket.overNumber = objLastWicketFB?.ball;
                        lstWicket.score = objLastWicketFB?.total_score;

                        if (fixturesBalls != null)
                        {
                            fixturesBalls.last_wicket = lstWicket;
                        }
                    }

                    List<Fixtures_Balls> fixturesBallsLst = new List<Fixtures_Balls>();
                    if (fixturesBalls != null)
                    {
                        fixturesBallsLst.Add(fixturesBalls);
                    }

                    fixturesBallsLst[0].balls?.Reverse();
                    decimal lastOver = Decimal.Truncate(Convert.ToDecimal(fixturesBallsLst[0].balls?[0].ball));
                    fixturesBallsLst[0].balls = fixturesBallsLst[0].balls?.Where(e => Decimal.Truncate(Convert.ToDecimal(e.ball)) <= (lastOver - start_index) && Decimal.Truncate(Convert.ToDecimal(e.ball)) > (lastOver - (start_index + count))).ToList();

                    if (fixturesBallsLst[0] != null && fixturesBallsLst[0].miniscore != null && fixturesBallsLst[0]?.miniscore?.batting != null)
                    {
                        FixturesBalls? objFirstBall = fixturesBalls?.balls?.First();
                        foreach (Batting batt in fixturesBallsLst[0].miniscore.batting)
                        {
                            batt.is_strike = (batt.batsman == objFirstBall?.batsman_strike?.name);
                        }
                    }


                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, fixturesBallsLst);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "No data", out response);
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
