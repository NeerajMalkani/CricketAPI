namespace CricketAPI
{
    public class Fixtures
    {
        public long id { get; set; }
        public int is_live { get; set; }
        public string? league_name { get; set; }
        public string? league_image { get; set; }
        public string? match_name { get; set; }
        public int local_team_id { get; set; }
        public string? local_team_name { get; set; }
        public string? local_team_code { get; set; }
        public string? local_team_flag { get; set; }
        public long local_team_score { get; set; }
        public string? local_team_overs { get; set; }
        public long local_team_wickets { get; set; }
        public long local_team_inning { get; set; }
        public int visitor_team_id { get; set; }
        public string? visitor_team_name { get; set; }
        public string? visitor_team_code { get; set; }
        public string? visitor_team_flag { get; set; }
        public long visitor_team_score { get; set; }
        public string? visitor_team_overs { get; set; }
        public long visitor_team_wickets { get; set; }
        public long visitor_team_inning { get; set; }
        public string? toss_won_team { get; set; }
        public int total_overs_played { get; set; }
        public int won_id { get; set; }
        public string? type { get; set; }
        public string? status { get; set; }
        public string? note { get; set; }
        public string? elected { get; set; }
        public DateTime starting_at { get; set; }
    }
}
