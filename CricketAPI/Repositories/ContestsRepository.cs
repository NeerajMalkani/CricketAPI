using CricketAPI.Entites;
using CricketAPI.Helpers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;

namespace CricketAPI.Repositories
{
    public class ContestsRepository
    {
        #region Prize Pool
        private Pools SetPoolObject(int remainingWinnigs, int sp, int ep, double per)
        {
            Pools pool = new Pools();
            pool.startPosition = sp;
            pool.endPosition = ep;
            pool.percentage = per;
            pool.prize = Convert.ToInt32(Math.Round(remainingWinnigs * Convert.ToDecimal(Convert.ToDecimal(per) / 100)));
            return pool;
        }

        public List<PrizePool> CalculatePrizePool(PrizePoolRequest prizePoolRequest)
        {
            List<PrizePool> prizePools = new List<PrizePool>();
            if (prizePoolRequest.entryFees >= 0 && prizePoolRequest.spots > 1)
            {
                List<int> breakPoints = new List<int>() { 1, 2, 3, 4, 5, 7, 10, 15, 25, 50, 100, 250, 500, 1000, 2000, 2500, 5000 };
                int totalWinnings = prizePoolRequest.entryFees * prizePoolRequest.spots;
                int ssCommission = Convert.ToInt32(Math.Round(totalWinnings * Convert.ToDecimal(Convert.ToDecimal(20) / 100)));
                int remainingWinnigs = totalWinnings - ssCommission;

                int maximumWinners = Convert.ToInt32(Math.Round(prizePoolRequest.spots * (Convert.ToDecimal(50) / 100)));
                maximumWinners = breakPoints.OrderBy(item => Math.Abs(maximumWinners - item)).First();

                foreach (int i in breakPoints)
                {
                    if (i <= maximumWinners)
                    {
                        PrizePool prizePool = new PrizePool();
                        prizePool.breakPoint = i;
                        List<Pools> pools = new List<Pools>();
                        switch (i)
                        {
                            case 1:
                                pools.Add(SetPoolObject(remainingWinnigs, 1, 1, 100));
                                break;
                            case 2:
                                pools.Add(SetPoolObject(remainingWinnigs, 1, 1, 70));
                                pools.Add(SetPoolObject(remainingWinnigs, 2, 2, 30));
                                break;
                            case 3:
                                pools.Add(SetPoolObject(remainingWinnigs, 1, 1, 50));
                                pools.Add(SetPoolObject(remainingWinnigs, 2, 2, 30));
                                pools.Add(SetPoolObject(remainingWinnigs, 3, 3, 20));
                                break;
                            case 4:
                                pools.Add(SetPoolObject(remainingWinnigs, 1, 1, 40));
                                pools.Add(SetPoolObject(remainingWinnigs, 2, 2, 25));
                                pools.Add(SetPoolObject(remainingWinnigs, 3, 3, 20));
                                pools.Add(SetPoolObject(remainingWinnigs, 4, 4, 15));
                                break;
                            case 5:
                                pools.Add(SetPoolObject(remainingWinnigs, 1, 1, 40));
                                pools.Add(SetPoolObject(remainingWinnigs, 2, 2, 23));
                                pools.Add(SetPoolObject(remainingWinnigs, 3, 3, 15));
                                pools.Add(SetPoolObject(remainingWinnigs, 4, 5, 11));
                                break;
                            case 7:
                                pools.Add(SetPoolObject(remainingWinnigs, 1, 1, 35));
                                pools.Add(SetPoolObject(remainingWinnigs, 2, 2, 19));
                                pools.Add(SetPoolObject(remainingWinnigs, 3, 3, 12));
                                pools.Add(SetPoolObject(remainingWinnigs, 4, 4, 10));
                                pools.Add(SetPoolObject(remainingWinnigs, 5, 7, 8));
                                break;
                            case 10:
                                pools.Add(SetPoolObject(remainingWinnigs, 1, 1, 30));
                                pools.Add(SetPoolObject(remainingWinnigs, 2, 2, 18));
                                pools.Add(SetPoolObject(remainingWinnigs, 3, 3, 11));
                                pools.Add(SetPoolObject(remainingWinnigs, 4, 4, 7.5));
                                pools.Add(SetPoolObject(remainingWinnigs, 5, 5, 6));
                                pools.Add(SetPoolObject(remainingWinnigs, 6, 10, 5.5));
                                break;
                            case 15:
                                pools.Add(SetPoolObject(remainingWinnigs, 1, 1, 25));
                                pools.Add(SetPoolObject(remainingWinnigs, 2, 2, 12.5));
                                pools.Add(SetPoolObject(remainingWinnigs, 3, 3, 10));
                                pools.Add(SetPoolObject(remainingWinnigs, 4, 4, 7.5));
                                pools.Add(SetPoolObject(remainingWinnigs, 5, 5, 5));
                                pools.Add(SetPoolObject(remainingWinnigs, 6, 10, 4));
                                break;
                            case 25:
                                pools.Add(SetPoolObject(remainingWinnigs, 1, 1, 20));
                                pools.Add(SetPoolObject(remainingWinnigs, 2, 2, 12));
                                pools.Add(SetPoolObject(remainingWinnigs, 3, 3, 8));
                                pools.Add(SetPoolObject(remainingWinnigs, 4, 4, 5));
                                pools.Add(SetPoolObject(remainingWinnigs, 5, 5, 5));
                                pools.Add(SetPoolObject(remainingWinnigs, 6, 10, 2.5));
                                break;
                            case 50:
                                pools.Add(SetPoolObject(remainingWinnigs, 1, 1, 15));
                                pools.Add(SetPoolObject(remainingWinnigs, 2, 2, 10));
                                pools.Add(SetPoolObject(remainingWinnigs, 3, 3, 8));
                                pools.Add(SetPoolObject(remainingWinnigs, 4, 4, 4));
                                pools.Add(SetPoolObject(remainingWinnigs, 5, 5, 3));
                                pools.Add(SetPoolObject(remainingWinnigs, 6, 10, 2));
                                pools.Add(SetPoolObject(remainingWinnigs, 11, 25, 1.5));
                                pools.Add(SetPoolObject(remainingWinnigs, 26, 50, 1.1));
                                break;
                            case 100:
                                pools.Add(SetPoolObject(remainingWinnigs, 1, 1, 15));
                                pools.Add(SetPoolObject(remainingWinnigs, 2, 2, 10));
                                pools.Add(SetPoolObject(remainingWinnigs, 3, 3, 8));
                                pools.Add(SetPoolObject(remainingWinnigs, 4, 4, 3.75));
                                pools.Add(SetPoolObject(remainingWinnigs, 5, 5, 3.5));
                                pools.Add(SetPoolObject(remainingWinnigs, 6, 10, 1.5));
                                pools.Add(SetPoolObject(remainingWinnigs, 11, 15, 1));
                                pools.Add(SetPoolObject(remainingWinnigs, 16, 25, 0.6));
                                pools.Add(SetPoolObject(remainingWinnigs, 26, 100, 0.55));
                                break;
                            case 250:
                                pools.Add(SetPoolObject(remainingWinnigs, 1, 1, 12));
                                pools.Add(SetPoolObject(remainingWinnigs, 2, 2, 7.5));
                                pools.Add(SetPoolObject(remainingWinnigs, 3, 3, 5));
                                pools.Add(SetPoolObject(remainingWinnigs, 4, 4, 3));
                                pools.Add(SetPoolObject(remainingWinnigs, 5, 5, 2.25));
                                pools.Add(SetPoolObject(remainingWinnigs, 6, 10, 2));
                                pools.Add(SetPoolObject(remainingWinnigs, 11, 15, 1));
                                pools.Add(SetPoolObject(remainingWinnigs, 16, 25, 0.5));
                                pools.Add(SetPoolObject(remainingWinnigs, 26, 50, 0.25));
                                pools.Add(SetPoolObject(remainingWinnigs, 50, 250, 0.22));
                                break;
                            case 500:
                                pools.Add(SetPoolObject(remainingWinnigs, 1, 1, 10));
                                pools.Add(SetPoolObject(remainingWinnigs, 2, 2, 7));
                                pools.Add(SetPoolObject(remainingWinnigs, 3, 3, 3.5));
                                pools.Add(SetPoolObject(remainingWinnigs, 4, 5, 2.5));
                                pools.Add(SetPoolObject(remainingWinnigs, 6, 10, 1));
                                pools.Add(SetPoolObject(remainingWinnigs, 11, 25, 0.3));
                                pools.Add(SetPoolObject(remainingWinnigs, 26, 100, 0.2));
                                pools.Add(SetPoolObject(remainingWinnigs, 101, 250, 0.15));
                                pools.Add(SetPoolObject(remainingWinnigs, 251, 500, 0.11));
                                break;
                            case 1000:
                                pools.Add(SetPoolObject(remainingWinnigs, 1, 1, 5));
                                pools.Add(SetPoolObject(remainingWinnigs, 2, 2, 3));
                                pools.Add(SetPoolObject(remainingWinnigs, 3, 3, 1.7));
                                pools.Add(SetPoolObject(remainingWinnigs, 4, 10, 0.9));
                                pools.Add(SetPoolObject(remainingWinnigs, 11, 50, 0.3));
                                pools.Add(SetPoolObject(remainingWinnigs, 51, 100, 0.2));
                                pools.Add(SetPoolObject(remainingWinnigs, 101, 500, 0.08));
                                pools.Add(SetPoolObject(remainingWinnigs, 501, 1000, 0.06));
                                break;
                            case 2000:
                                pools.Add(SetPoolObject(remainingWinnigs, 1, 1, 5));
                                pools.Add(SetPoolObject(remainingWinnigs, 2, 2, 3));
                                pools.Add(SetPoolObject(remainingWinnigs, 3, 3, 2));
                                pools.Add(SetPoolObject(remainingWinnigs, 4, 10, 1));
                                pools.Add(SetPoolObject(remainingWinnigs, 11, 50, 0.2));
                                pools.Add(SetPoolObject(remainingWinnigs, 51, 100, 0.1));
                                pools.Add(SetPoolObject(remainingWinnigs, 101, 500, 0.05));
                                pools.Add(SetPoolObject(remainingWinnigs, 501, 1000, 0.04));
                                pools.Add(SetPoolObject(remainingWinnigs, 1001, 2000, 0.03));
                                break;
                            case 2500:
                                pools.Add(SetPoolObject(remainingWinnigs, 1, 1, 5));
                                pools.Add(SetPoolObject(remainingWinnigs, 2, 2, 3));
                                pools.Add(SetPoolObject(remainingWinnigs, 3, 3, 2));
                                pools.Add(SetPoolObject(remainingWinnigs, 4, 10, 1));
                                pools.Add(SetPoolObject(remainingWinnigs, 11, 50, 0.2));
                                pools.Add(SetPoolObject(remainingWinnigs, 51, 100, 0.1));
                                pools.Add(SetPoolObject(remainingWinnigs, 101, 500, 0.05));
                                pools.Add(SetPoolObject(remainingWinnigs, 501, 1000, 0.04));
                                pools.Add(SetPoolObject(remainingWinnigs, 1001, 2500, 0.03));
                                break;
                            case 5000:
                                pools.Add(SetPoolObject(remainingWinnigs, 1, 1, 4));
                                pools.Add(SetPoolObject(remainingWinnigs, 2, 2, 2));
                                pools.Add(SetPoolObject(remainingWinnigs, 3, 3, 1));
                                pools.Add(SetPoolObject(remainingWinnigs, 4, 10, 0.5));
                                pools.Add(SetPoolObject(remainingWinnigs, 11, 50, 0.25));
                                pools.Add(SetPoolObject(remainingWinnigs, 51, 100, 0.1));
                                pools.Add(SetPoolObject(remainingWinnigs, 101, 500, 0.05));
                                pools.Add(SetPoolObject(remainingWinnigs, 501, 1000, 0.021));
                                pools.Add(SetPoolObject(remainingWinnigs, 1001, 5000, 0.011));
                                break;
                        }
                        prizePool.pools = pools;
                        prizePools.Add(prizePool);
                    }
                    else
                    {
                        break;
                    }
                }
            }


            return prizePools;
        }
        #endregion

        #region Contests
        public async Task<long> InsertContests(DataContext context, Contests contests)
        {
            long insertedID = 0;
            context.Contests.Add(contests);
            await context.SaveChangesAsync();
            insertedID = contests.id;
            return insertedID;
        }

        public async Task<int> InsertUserContests(DataContext context, UserJoinedContests userJoinedContests)
        {
            int rowsAffected = 0;
            UserJoinedContests? userJoinedContestsExists = context.UserJoinedContests.ToList().Where(el => el.user_id == userJoinedContests.user_id && el.fixture_id == userJoinedContests.fixture_id && el.contest_id == userJoinedContests.contest_id).FirstOrDefault();
            if (userJoinedContestsExists == null || userJoinedContestsExists?.id == 0)
            {
                context.UserJoinedContests.Add(userJoinedContests);
                await context.SaveChangesAsync();
                rowsAffected = 1;
            }
            return rowsAffected;
        }

        public async Task<int> UpdateUserContestWithTeam(DataContext context, UserContestMapping userContestWithTeamRequest)
        {
            int rowsAffected = 0;
            UserContestMapping? userTeam = context.UserContestMapping.ToList().Where(el => el.user_team_id == userContestWithTeamRequest.user_team_id).FirstOrDefault();
            if (userTeam != null && userTeam.contest_id == null)
            {
                userTeam.contest_id = userContestWithTeamRequest.contest_id;
                await context.SaveChangesAsync();
                rowsAffected = 1;
            }
            else
            {
                context.UserContestMapping.Add(userContestWithTeamRequest);
                await context.SaveChangesAsync();
                rowsAffected = 1;
            }
            return rowsAffected;
        }

        public async Task<int> DeleteUserTeamForContest(DataContext context, UserContestMapping userContestMapping)
        {
            int rowsAffected = 0;
            try
            {
                if (userContestMapping != null)
                {
                    UserContestMapping userContest = context.UserContestMapping.Where(item => item.user_team_id == userContestMapping.user_team_id && item.contest_id == userContestMapping.contest_id).First();
                    if (userContest != null)
                    {
                        context.UserContestMapping.Remove(userContest);
                        await context.SaveChangesAsync();
                    }
                    rowsAffected++;
                }
            }
            catch (Exception)
            {
                rowsAffected = 0;
            }
            return rowsAffected;
        }

        public List<Contests> GetContests(DataContext context, long fixture_id)
        {
            List<Contests> contests = new List<Contests>();
            try
            {
                contests = context.Contests.Where(el => el.fixture_id == fixture_id && el.created_by == "system").ToList();
            }
            catch (Exception)
            {
                contests = new List<Contests>();
            }
            return contests;
        }

        public List<ContestsHome> GetContestsHome(DataContext context, ContestRequest contestRequest)
        {
            List<ContestsHome> contests = new List<ContestsHome>();
            List<UserJoinedContests> userJoinedContests = new List<UserJoinedContests>();
            List<UserTeam> userTeam = new List<UserTeam>();
            try
            {
                userJoinedContests = context.UserJoinedContests.Where(el => el.fixture_id == contestRequest.fixture_id && el.user_id == contestRequest.user_id).ToList();
                userTeam = context.UserTeam.Where(el => el.fixture_id == contestRequest.fixture_id && el.user_id == contestRequest.user_id).ToList();
                ContestsHome contestsHome = new ContestsHome();
                contestsHome.contest_count = userJoinedContests.Count;
                contestsHome.teams_count = userTeam.Count;
                contests.Add(contestsHome);
            }
            catch (Exception)
            {
                contests = new List<ContestsHome>();
            }
            return contests;
        }

        public List<GetContestResponse> GetUserContests(DataContext context, ContestRequest contestRequest)
        {
            List<GetContestResponse> getContestResponse = new List<GetContestResponse>();
            try
            {
                List<ContestsJson> contestsJsons = context.ContestsJson.FromSqlRaw("CALL `cric_Get_UserContest`(" + contestRequest.fixture_id + ", '" + contestRequest.user_id + "')").ToList();
                if (contestsJsons.Count > 0)
                {
                    getContestResponse = JsonConvert.DeserializeObject<List<GetContestResponse>>(contestsJsons[0].Contests) ?? throw new ArgumentException();
                }

            }
            catch (Exception)
            {
                getContestResponse = new List<GetContestResponse>();
            }
            return getContestResponse;
        }

        public List<Fixtures> GetContestFixtures(DataContext context, ContestFixtuesRequest contestFixtuesRequest)
        {
            List<Fixtures> fixtures = new List<Fixtures>();
            try
            {
                switch (contestFixtuesRequest.type)
                {
                    case "upcoming":
                        List<FixturesJson> fixturesUpcomingJson = context.FixturesJson.FromSqlRaw("CALL `cric_Get_UpcomingFixturesContests`('" + contestFixtuesRequest.user_id + "')").ToList();
                        if (!fixturesUpcomingJson[0].Fixtures.Equals("[]"))
                        {
                            fixtures = JsonConvert.DeserializeObject<List<Fixtures>>(fixturesUpcomingJson[0].Fixtures) ?? throw new ArgumentException();
                        }
                        break;
                    case "recent":
                        List<FixturesJson> fixturesRecentJson = context.FixturesJson.FromSqlRaw("CALL `cric_Get_RecentFixturesContests`('" + contestFixtuesRequest.user_id + "')").ToList();
                        if (!fixturesRecentJson[0].Fixtures.Equals("[]"))
                        {
                            fixtures = JsonConvert.DeserializeObject<List<Fixtures>>(fixturesRecentJson[0].Fixtures) ?? throw new ArgumentException();
                        }
                        break;
                    case "live":
                        List<FixturesJson> fixturesLiveJson = context.FixturesJson.FromSqlRaw("CALL `cric_GetLiveFixturesContests`('" + contestFixtuesRequest.user_id + "')").ToList();
                        if (!fixturesLiveJson[0].Fixtures.Equals("[]"))
                        {
                            fixtures = JsonConvert.DeserializeObject<List<Fixtures>>(fixturesLiveJson[0].Fixtures) ?? throw new ArgumentException();
                        }
                        break;
                }
            }
            catch (Exception)
            {
                fixtures = new List<Fixtures>();
            }
            return fixtures;
        }

        public Contests? GetUserContest(DataContext context, long contest_id)
        {
            Contests? contest = new Contests();
            try
            {
                contest = context.Contests.Where(el => el.id == contest_id).ToList().FirstOrDefault();
            }
            catch (Exception)
            {
                contest = new Contests();
            }
            return contest;
        }

        public ContestLeaderboard GetUserContestLeaderboard(DataContext context, ContestLeaderboardRequest contestLeaderboardRequest)
        {
            ContestLeaderboard contestLeaderboards = new ContestLeaderboard();
            try
            {
                List<ContestsLeaderboardJson> contestsLeaderboardJsons = context.ContestsLeaderboardJson.FromSqlRaw("CALL `cric_Get_Leaderboard`(" + contestLeaderboardRequest.fixture_id + ", " + contestLeaderboardRequest.contest_id + ")").ToList();
                if (contestsLeaderboardJsons.Count > 0)
                {
                    contestLeaderboards = JsonConvert.DeserializeObject<ContestLeaderboard>(contestsLeaderboardJsons[0].ContestLeaderboard) ?? throw new ArgumentException();
                }
            }
            catch (Exception)
            {
                contestLeaderboards = new ContestLeaderboard();
            }
            return contestLeaderboards;
        }

        public UserTeamStats GetUserContestUserStats(DataContext context, ContestUserStatsRequest contestLeaderboardRequest)
        {
            UserTeamStats userTeamStats = new UserTeamStats();
            try
            {
                List<UserTeamPointsJson> contestsLeaderboardJsons = context.UserTeamPointsJson.FromSqlRaw("CALL `cric_Get_UserTeamPlayerPoints`('" + contestLeaderboardRequest.user_id + "', " + contestLeaderboardRequest.fixture_id + ", " + contestLeaderboardRequest.contest_id + ")").ToList();
                if (contestsLeaderboardJsons.Count > 0)
                {
                    userTeamStats = JsonConvert.DeserializeObject<UserTeamStats>(contestsLeaderboardJsons[0].UserTeamStats) ?? throw new ArgumentException();
                }
            }
            catch (Exception)
            {
                userTeamStats = new UserTeamStats();
            }
            return userTeamStats;
        }

        public UserTeamStats GetUserTeamPlayerStats(DataContext context, UserTeamRequest userTeamRequest)
        {
            UserTeamStats userTeamStats = new UserTeamStats();
            try
            {
                List<UserTeamPointsJson> contestsLeaderboardJsons = context.UserTeamPointsJson.FromSqlRaw("CALL `cric_Get_UserSingleTeamPlayerPoints`(" + userTeamRequest.fixture_id + ", " + userTeamRequest.user_team_id + ",'" + userTeamRequest.user_id + "')").ToList();
                if (contestsLeaderboardJsons.Count > 0)
                {
                    userTeamStats = JsonConvert.DeserializeObject<UserTeamStats>(contestsLeaderboardJsons[0].UserTeamStats) ?? throw new ArgumentException();
                }
            }
            catch (Exception ex)
            {
                userTeamStats = new UserTeamStats();
            }
            return userTeamStats;
        }

        public List<UserTeamPointsStats>? GetUserTeamStats(DataContext context, ContestUserTeamStatsRequest contestUserTeamStats)
        {
            List<UserTeamPointsStats>? userTeamPointsStats = new List<UserTeamPointsStats>();
            try
            {
                if (contestUserTeamStats.user_team_id != null)
                {
                    userTeamPointsStats = context.UserTeamPointsStats.Where(el => contestUserTeamStats.user_team_id.Contains(el.user_team_id)).ToList();
                }
            }
            catch (Exception)
            {
                userTeamPointsStats = new List<UserTeamPointsStats>();
            }
            return userTeamPointsStats;
        }

        public List<FantasyPoints>? GetFantasyPoints(DataContext context, UserTeamPlayerPointsRequest userTeamPlayerPointsRequest)
        {
            List<FantasyPoints>? fantasyPoints = new List<FantasyPoints>();
            try
            {
                if (userTeamPlayerPointsRequest != null)
                {
                    fantasyPoints = context.FantasyPoints.Where(el => el.fixture_id == userTeamPlayerPointsRequest.fixture_id && el.player_id == userTeamPlayerPointsRequest.player_id).ToList();
                }
            }
            catch (Exception)
            {
                fantasyPoints = new List<FantasyPoints>();
            }
            return fantasyPoints;
        }
        #endregion
    }
}
