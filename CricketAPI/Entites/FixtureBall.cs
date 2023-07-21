using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CricketAPI.Entites
{
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

    [Keyless]
    public class Fixtures_Balls
    {
        public string? ball { get; set; }
        public Team? team { get; set; }
        public Score? score { get; set; }
        public Bowler? bowler { get; set; }
        public int? ball_id { get; set; }
        public int? catch_by { get; set; }
        public int? run_out_by { get; set; }
        public string? scoreboard { get; set; }
        public int? batsman_out { get; set; }
        public BatsmanStrike? batsman_strike { get; set; }
        public BatsmanNonstrike? batsman_nonstrike { get; set; }
    }

    public class BallJson
    {
        [Key]
        public string Fixtures_Balls { get; set; } = "";
    }
}
