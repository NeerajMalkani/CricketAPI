using System.ComponentModel.DataAnnotations;

namespace CricketAPI.Entites
{
    #region Prize Pool
    public class PrizePoolRequest
    {
        public int entryFees { get; set; }
        public int spots { get; set; }
    }
    public class PrizePool
    {
        public int breakPoint { get; set; }
        public List<Pools>? pools { get; set; }
    }
    public class Pools
    {
        public int startPosition { get; set; }
        public int endPosition { get; set; }
        public double percentage { get; set; }
        public int prize { get; set; }
    }
    #endregion

    #region Contests
    public class Contests
    {
        public long id { get; set; }
        public string? created_by { get; set; }
        public long fixture_id { get; set; }
        public string? contest_name { get; set; }
        public int entry_fees { get; set; }
        public int spots { get; set; }
        public int spots_filled { get; set; }
        public int first_prize { get; set; }
        public int max_prize_pool { get; set; }
        public int number_of_winners { get; set; }
    }
    public class ContestsHome
    {
        public int contest_count { get; set; }
        public int teams_count { get; set; }
    }


        public class ContestRequest
    {
        public long fixture_id { get; set; }
        public string? user_id { get; set; }
    }

    public class ContestFixtuesRequest
    {
        public string? type { get; set; }
        public string? user_id { get; set; }
    }

    public class ContestResponse
    {
        public long contest_id { get; set; }
    }

    public class UserContestTeam
    {
        public int id { get; set; }
        public string? team_name { get; set; }
    }

    public class GetContestResponse
    {
        public int id { get; set; }
        public string? created_by { get; set; }
        public int fixture_id { get; set; }
        public string? contest_name { get; set; }
        public int spots { get; set; }
        public int spots_filled { get; set; }
        public int first_prize { get; set; }
        public int max_prize_pool { get; set; }
        public int number_of_winners { get; set; }
        public List<UserContestTeam>? user_team { get; set; }
    }

    public class ContestsJson
    {
        [Key]
        public string Contests { get; set; } = "";
    }

    public class UserJoinedContests
    {
        public long id { get; set; }
        public string? user_id { get; set; }
        public long fixture_id { get; set; }
        public long contest_id { get; set; }
        public DateTime? updated_at { get; set; }
    }

    public class ContestLeaderboardRequest
    {
        public long fixture_id { get; set; }
        public long contest_id { get; set; }
    }

    public class ContestUserStatsRequest
    {
        public string? user_id { get; set; }
        public long fixture_id { get; set; }
        public long contest_id { get; set; }
    }
    

    public class ContestsLeaderboardJson
    {
        [Key]
        public string ContestLeaderboard { get; set; } = "";
    }
    
    public class ContestLeaderboard
    {
        public int id { get; set; }
        public string? contest_name { get; set; }
        public int entry_fees { get; set; }
        public int spots { get; set; }
        public int number_of_winners { get; set; }
        public int first_prize { get; set; }
        public int max_pool_prize { get; set; }
        public List<Leaderboard>? leaderboard { get; set; }
    }
    
    public class Leaderboard
    {
        public long team_id { get; set; }
        public string? team_name { get; set; }
        public int team_points { get; set; }
    }


    public class PlayingTeam
    {
        public int id { get; set; }
        public string? code { get; set; }
        public string? name { get; set; }
        public string? color { get; set; }
        public string? image_path { get; set; }
    }
    public class Points
    {
        public int scorepoints { get; set; }
        public int fourpoints { get; set; }
        public int sixpoints { get; set; }
        public int duckpoints { get; set; }
        public int thirtypoints { get; set; }
        public int fiftypoints { get; set; }
        public int hundredpoints { get; set; }
        public int strikeratepoints { get; set; }
        public int catchstumpspoints { get; set; }
        public int runoutpoints { get; set; }
        public int wicketspoints { get; set; }
        public int medianspoints { get; set; }
        public int threewicketpoints { get; set; }
        public int fourwicketpoints { get; set; }
        public int fivewicketpoints { get; set; }
        public int ecopoints { get; set; }
        public int total_points { get; set; }
    }
    public class TeamPlayer
    {
        public int player_id { get; set; }
        public string? player_name { get; set; }
        public int is_captain { get; set; }
        public int is_vice_captain { get; set; }
        public PlayingTeam? playing_team { get; set; }
        public Points? points { get; set; }
    }
    public class UserTeamStats
    {
        public int id { get; set; }
        public string? team_name { get; set; }
        public int team_points { get; set; }
        public List<TeamPlayer>? team_players { get; set; }
    }

    public class UserTeamPointsJson
    {
        [Key]
        public string UserTeamStats { get; set; } = "";
    }

    public class UserContestWithTeamRequest
    {
        public long contest_id { get; set; }
        public long user_team_id { get; set; }
    }

    #endregion
}
