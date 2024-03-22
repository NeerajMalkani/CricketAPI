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

    public class ContestRequest
    {
        public long fixture_id { get; set; }
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
        public string? updated_at { get; set; }
    }
    #endregion
}
