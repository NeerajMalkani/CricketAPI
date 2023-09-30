using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CricketAPI
{
    #region Fixtures
    public class Localteam
    {
        [Key]
        public long? id { get; set; }
        public string? code { get; set; }
        public string? color { get; set; }
        public string? name { get; set; }
        public string? image_path { get; set; }
    }
    public class Visitorteam
    {
        [Key]
        public long? id { get; set; }
        public string? code { get; set; }
        public string? color { get; set; }
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
        public long series_id { get; set; }
        public long league_id { get; set; }
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
        public string? rpc_overs { get; set; }
        public string? rpc_target { get; set; }
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
        public decimal? strike_rate { get; set; }
        public string? bowler { get; set; }
        public string? runout { get; set; }
        public string? how_out { get; set; }
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
        public List<Lineup>? yetToBat { get; set; }
        public List<Bowling>? bowling { get; set; }
    }
    public class Scoreboard
    {
        [Key]
        public long id { get; set; }
        public long series_id { get; set; }
        public long league_id { get; set; }
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
        public long player_id { get; set; }
        public int? sort { get; set; }
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
        public string? team_color { get; set; }
        public string? team_name { get; set; }
        public string? team_image_path { get; set; }
    }
    public class FixturesTeamLineup
    {
        [Key]
        public long id { get; set; }
        public long series_id { get; set; }
        public long league_id { get; set; }
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

    #region Balls
    [Keyless]
    public class BatsmanNonstrike
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? position { get; set; }
        public string? image_path { get; set; }
    }
    [Keyless]
    public class BatsmanStrike
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? position { get; set; }
        public string? image_path { get; set; }
    }
    [Keyless]
    public class Bowler
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? position { get; set; }
        public string? image_path { get; set; }
    }
    [Keyless]
    public class Score
    {
        public bool? bye { get; set; }
        public string? is_out { get; set; }
        public string? six { get; set; }
        public string? ball { get; set; }
        public string? four { get; set; }
        public string? name { get; set; }
        public int? runs { get; set; }
        public bool? noball { get; set; }
        public bool? leg_bye { get; set; }
        public string? is_wicket { get; set; }
        public int? noball_runs { get; set; }
    }
    [Keyless]
    public class Team
    {
        public int id { get; set; }
        public string? code { get; set; }
        public string? name { get; set; }
        public string? image_path { get; set; }
    }
    public class FixturesBalls
    {
        public int? ball_id { get; set; }
        public string? ball { get; set; }
        public Team? team { get; set; }
        public Score? score { get; set; }
        public Bowler? bowler { get; set; }
        public string? catch_by { get; set; }
        public string? run_out_by { get; set; }
        public string? scoreboard { get; set; }
        public string? batsman_out { get; set; }
        public int? total_score { get; set; }
        public int? over_score { get; set; }
        public int? total_wickets { get; set; }
        public BatsmanStrike? batsman_strike { get; set; }
        public BatsmanNonstrike? batsman_nonstrike { get; set; }
    }
    public class Miniscore
    {
        public List<Batting>? batting { get; set; }
        public List<Bowling>? bowling { get; set; }
    }
    public class LastWicket
    {
        public string? playerName { get; set; }
        public int? score { get; set; }
        public string? overNumber { get; set; }
        public int? wicketNumber { get; set; }
    }
    public class Fixtures_Balls
    {
        [Key]
        public long id { get; set; }
        public long? series_id { get; set; }
        public long? league_id { get; set; }
        public string? note { get; set; }
        public string? type { get; set; }
        public string? round { get; set; }
        public string? status { get; set; }
        public string? stage_name { get; set; }
        public string? league_name { get; set; }
        public string? season_name { get; set; }
        public string? rpc_overs { get; set; }
        public string? rpc_target { get; set; }
        public int? total_overs_played { get; set; }
        public DateTime? starting_at { get; set; }
        public Localteam? localteam { get; set; }
        public Visitorteam? visitorteam { get; set; }
        public List<MatchScore>? match_score { get; set; }
        public Miniscore? miniscore { get; set; }
        public List<FixturesBalls>? balls { get; set; }
        public ManOfTheMatch? man_of_the_match { get; set; }
        public ManOfTheSeries? man_of_the_series { get; set; }
        public LastWicket? last_wicket { get; set; }
    }
    public class BallJson
    {
        [Key]
        public string Fixtures_Balls { get; set; } = "";
    }
    #endregion
}
