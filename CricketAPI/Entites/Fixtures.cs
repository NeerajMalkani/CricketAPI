namespace CricketAPI
{
    public class Fixtures
    {
        public long id { get; set; }
        public string? league_name { get; set; }
        public string? league_image { get; set; }
        public string? match_name { get; set; }
        public int localteam_id { get; set; }
        public string? local_team_name { get; set; }
        public string? local_team_code { get; set; }
        public string? local_team_flag { get; set; }
        public long local_team_score { get; set; }
        public string? local_team_overs { get; set; }
        public long local_team_wickets { get; set; }
        public long local_team_inning { get; set; }
        public int visitorteam_id { get; set; }
        public string? visitor_team_name { get; set; }
        public string? visitor_team_code { get; set; }
        public string? visitor_team_flag { get; set; }
        public long visitor_team_score { get; set; }
        public string? visitor_team_overs { get; set; }
        public long visitor_team_wickets { get; set; }
        public long visitor_team_inning { get; set; }
        public FixtureDetails? fixture_details { get; set; }
        public FixtureToss? fixture_toss { get; set; }
        public FixtureUmpires? fixture_umpires { get; set; }
        public FixtureVenue? fixture_venue { get; set; }
    }

    public class FixtureDetails
    {
        public int is_live { get; set; }
        public int total_overs_played { get; set; }
        public int won_id { get; set; }
        public string? type { get; set; }
        public string? status { get; set; }
        public string? note { get; set; }
        public DateTime starting_at { get; set; }
    }

    public class FixtureToss
    {
        public string? toss_won_team { get; set; }
        public string? elected { get; set; }
    }

    public class FixtureUmpires
    {
        public string? first_umpire { get; set; }
        public string? second_umpire { get; set; }
        public string? tv_umpire { get; set; }
        public string? referee { get; set; }
    }

    public class FixtureVenue
    {
        public string? venue_name { get; set; }
        public string? venue_city { get; set; }
        public string? venue_image { get; set; }
        public string? venue_capacity { get; set; }
    }
}
