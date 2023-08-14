using System.ComponentModel.DataAnnotations;

namespace CricketAPI.Entites
{
    public class Players
    {
        public int id { get; set; }
        public int country_id { get; set; }
        public string? firstname { get; set; }
        public string? lastname { get; set; }
        public string? fullname { get; set; }
        public string? image_path { get; set; }
        public string? dateofbirth { get; set; }
        public string? gender { get; set; }
        public string? battingstyle { get; set; }
        public string? bowlingstyle { get; set; }
        public string? positionresource { get; set; }
        public int? positionid { get; set; }
        public string? positionname { get; set; }
    }

    #region Player Stats
    public class BattingStats
    {
        public int matches { get; set; } = 0;
        public int innings { get; set; } = 0;
        public int runs_scored { get; set; } = 0;
        public int balls_faced { get; set; } = 0;
        public int not_outs { get; set; } = 0;     
        public int highest_inning_score { get; set; } = 0;
        public int six_x { get; set; } = 0;
        public int four_x { get; set; } = 0;
        public double strike_rate { get; set; } = 0.00;
        public double average { get; set; } = 0.00;
        public int fifties { get; set; } = 0;
        public int hundreds { get; set; } = 0;
        public double fow_balls { get; set; } = 0.00;
        public int fow_score { get; set; } = 0; 
    }

    public class PlayerBattingStats
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? image_path { get; set; }
        public string? league_type { get; set; }
        public string? season_name { get; set; }
        public string? series_name { get; set; }
        public BattingStats? stats { get; set; }
    }
    public class PlayerBattingStatsJson
    {
        [Key]
        public string PlayerBattingStats { get; set; } = "";
    }
    #endregion

    #region Player Stats
    public class BowlingStats
    {
        public int matches { get; set; } = 0;
        public int innings { get; set; } = 0;
        public double overs { get; set; } = 0.0;    
        public int runs { get; set; } = 0;
        public int medians { get; set; } = 0;
        public int wickets { get; set; } = 0;
        public double econ_rate { get; set; } = 0.00;
        public double strike_rate { get; set; } = 0.00;
        public double average { get; set; } = 0.00;
        public int wide { get; set; } = 0;      
        public int noball { get; set; } = 0;
        public int ten_wickets { get; set; } = 0;
        public int five_wickets { get; set; } = 0;
        public int four_wickets { get; set; } = 0;
        public double rate { get; set; } = 0.00;       
    }

    public class PlayerBowlingStats
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? image_path { get; set; }
        public string? league_type { get; set; }
        public string? season_name { get; set; }
        public string? series_name { get; set; }
        public BowlingStats? stats { get; set; }
    }
    public class PlayerBowlingStatsJson
    {
        [Key]
        public string PlayerBowlingStats { get; set; } = "";
    }
    #endregion
}
