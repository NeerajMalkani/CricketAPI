using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CricketAPI
{
    #region Fixtures
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

    public class MatchInfo
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

    [Keyless]
    public class MatchScore
    {
        public string? overs { get; set; }
        public int? score { get; set; }
        public int? inning { get; set; }
        public int? team_id { get; set; }
        public int? wickets { get; set; }
    }

    [Keyless]
    public class Fixtures
    {
        public MatchInfo? match_info { get; set; }
        public List<MatchScore>? match_score { get; set; }
    }

    public class FixturesJson
    {
        [Key]
        public string Fixtures { get; set; } = "";
    }
    #endregion

    #region Scorecard
    [Keyless]
    public class Batting
    {
        public string? batsman { get; set; }
        public string? image_path { get; set; }
        public int? runs { get; set; }
        public int? balls { get; set; }
        public int? four { get; set; }
        public int? six { get; set; }
        public int? strike_rate { get; set; }
        public string? bowler { get; set; }
        public object? runout { get; set; }
        public string? catch_stump { get; set; }
    }

    [Keyless]
    public class Bowling
    {
        public string? bowler { get; set; }
        public string? image_path { get; set; }
        public double? overs { get; set; }
        public int? runs { get; set; }
        public int? medians { get; set; }
        public int? wickets { get; set; }
        public double? rate { get; set; }
        public int? wide { get; set; }
        public int? noball { get; set; } 
    }

    [Keyless]
    public class Scorecard
    {
        public int? team_id { get; set; }
        public string? team_image_path { get; set; }
        public string? team_code { get; set; }
        public string? team_name { get; set; }
        public string? scoreboard { get; set; }
        public MatchScore? match_score { get; set; }
        public List<Batting>? batting { get; set; }
        public List<Bowling>? bowling { get; set; }
    }

    public class Scoreboard
    {
        [Key]
        public long id { get; set; }
        public string? note { get; set; }
        public string? type { get; set; }
        public string? round { get; set; }
        public string? status { get; set; }
        public string? stage_name { get; set; }
        public string? league_name { get; set; }
        public string? season_name { get; set; }
        public DateTime? starting_at { get; set; }
        public List<Scorecard>? scorecard { get; set; }
    }

    public class ScorecardJson
    {
        [Key]
        public string FixtureScoreboard { get; set; } = "";
    }
    #endregion

    #region Lineup
    public class Lineup
    {
        public int player_id { get; set; }
        public string? player_dob { get; set; }
        public string? player_fullname { get; set; }
        public int? player_position { get; set; }
        public string? player_image_path { get; set; }
        public string? player_is_captain { get; set; }
        public string? player_batting_style { get; set; }
        public string? player_bowling_style { get; set; }
        public string? player_position_name { get; set; }
        public string? player_is_wicket_keeper { get; set; }
    }

    public class Teamlineup
    {
        public List<Lineup>? team { get; set; }
        public int team_id { get; set; }
        public string? team_code { get; set; }
        public string? team_name { get; set; }
        public string? team_image_path { get; set; }
    }

    public class FixturesTeamLineup
    {
        [Key]
        public long id { get; set; }
        public string? note { get; set; }
        public string? type { get; set; }
        public string? round { get; set; }
        public string? status { get; set; }
        public string? stage_name { get; set; }
        public string? league_name { get; set; }
        public string? season_name { get; set; }
        public DateTime? starting_at { get; set; }
        public List<Teamlineup>? teamlineup { get; set; }
    }

    public class LineupJson
    {
        [Key]
        public string Fixtures_TeamLineup { get; set; } = "";
    }
    #endregion
}
