using System.ComponentModel.DataAnnotations;

namespace CricketAPI
{

    public class Fixtures
    {
        [Key]
        public long id { get; set; }
        public int league_id { get; set; }
        public int season_id { get; set; }
        public int stage_id { get; set; }
        public string? round { get; set; }
        public int localteam_id { get; set; }
        public int visitorteam_id { get; set; }
        public DateTime starting_at { get; set; }
        public string? type { get; set; }
        public bool live { get; set; }
        public string? status { get; set; }
        public string? last_period { get; set; }
        public string? note { get; set; }
        public int venue_id { get; set; }
        public int toss_won_team_id { get; set; }
        public int winner_team_id { get; set; }
        public bool draw_noresult { get; set; }
        public int first_umpire_id { get; set; }
        public int second_umpire_id { get; set; }
        public int tv_umpire_id { get; set; }
        public int referee_id { get; set; }
        public int man_of_match_id { get; set; }
        public int man_of_series_id { get; set; }
        public string? total_overs_played { get; set; }
        public string? elected { get; set; }
        public bool super_over { get; set; }
        public bool follow_on { get; set; }
        public int localteam_dl_datascore { get; set; }
        public int localteam_dl_dataovers { get; set; }
        public int localteam_dl_datawickets_out { get; set; }
        public int visitorteam_dl_datascore { get; set; }
        public int visitorteam_dl_dataovers { get; set; }
        public int visitorteam_dl_datawickets_out { get; set; }
        public int rpc_overs { get; set; }
        public int rpc_target { get; set; }
        public DateTime when_updated { get; set; }
    }
}
