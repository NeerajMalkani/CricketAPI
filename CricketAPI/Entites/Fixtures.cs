using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CricketAPI
{

    //public class Fixtures
    //{
    //    [Key]
    //    public long id { get; set; }
    //    public int league_id { get; set; }
    //    public int season_id { get; set; }
    //    public int stage_id { get; set; }
    //    public string? round { get; set; }
    //    public int localteam_id { get; set; }
    //    public int visitorteam_id { get; set; }
    //    public DateTime starting_at { get; set; }
    //    public string? type { get; set; }
    //    public bool live { get; set; }
    //    public string? status { get; set; }
    //    public string? last_period { get; set; }
    //    public string? note { get; set; }
    //    public int venue_id { get; set; }
    //    public int toss_won_team_id { get; set; }
    //    public int winner_team_id { get; set; }
    //    public bool draw_noresult { get; set; }
    //    public int first_umpire_id { get; set; }
    //    public int second_umpire_id { get; set; }
    //    public int tv_umpire_id { get; set; }
    //    public int referee_id { get; set; }
    //    public int man_of_match_id { get; set; }
    //    public int man_of_series_id { get; set; }
    //    public string? total_overs_played { get; set; }
    //    public string? elected { get; set; }
    //    public bool super_over { get; set; }
    //    public bool follow_on { get; set; }
    //    public int localteam_dl_datascore { get; set; }
    //    public int localteam_dl_dataovers { get; set; }
    //    public int localteam_dl_datawickets_out { get; set; }
    //    public int visitorteam_dl_datascore { get; set; }
    //    public int visitorteam_dl_dataovers { get; set; }
    //    public int visitorteam_dl_datawickets_out { get; set; }
    //    public int rpc_overs { get; set; }
    //    public int rpc_target { get; set; }
    //    public DateTime when_updated { get; set; }
    //}

    public class Localteam
    {
        [Key]
        public long id { get; set; }
        public string? code { get; set; }
        public string? name { get; set; }
        public string? image_path { get; set; }
    }

    public class Visitorteam
    {
        [Key]
        public long id { get; set; }
        public string? code { get; set; }
        public string? name { get; set; }
        public string? image_path { get; set; }
    }

    public class ManOfTheMatch
    {
        [Key]
        public long? id { get; set; }
        public string? name { get; set; }
        public string? image_path { get; set; }
    }

    public class ManOfTheSeries
    {
        [Key]
        public long? id { get; set; }
        public string? name { get; set; }
        public string? image_path { get; set; }
    }

    public class Umpires
    {
        [Key]
        public long? id { get; set; }
        public string? refree { get; set; }
        public string? tv_umpire { get; set; }
        public string? first_umpire { get; set; }
        public string? second_umpire { get; set; }
    }

    public class Venue
    {
        [Key]
        public int? id { get; set; }
        public string? city { get; set; }
        public string? name { get; set; }
        public string? country { get; set; }
        public int? capacity { get; set; }
        public string? floodlight { get; set; }
        public string? image_path { get; set; }
    }

    public class Fixtures
    {
        [Key]
        public long id { get; set; }
        public string? league_name { get; set; }
        public string? season_name { get; set; }
        public string? stage_name { get; set; }
        public string? type { get; set; }
        public string? round { get; set; }
        public Localteam? localteam { get; set; }
        public Visitorteam? visitorteam { get; set; }
        public Venue? venue { get; set; }
        public Umpires? umpires { get; set; }
        public string? note { get; set; }
        public string? status { get; set; }
        public string? elected { get; set; }
        public string? starting_at { get; set; }
        public int? rpc_overs { get; set; }
        public int? rpc_target { get; set; }
        public string? follow_on { get; set; }
        public string? super_over { get; set; }
        public string? draw_noresult { get; set; }
        public int? toss_won_team_id { get; set; }
        public int? winner_team_id { get; set; }
        public ManOfTheMatch? man_of_the_match { get; set; }
        public ManOfTheSeries? man_of_the_series { get; set; }
        public int? total_overs_played { get; set; }
    }

    public class FixturesJson
    {
        [Key]
        public string Fixtures { get; set; } = "";
    }
}
