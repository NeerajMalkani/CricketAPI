using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CricketAPI.Entites
{
    #region Series
    public class Series
    {
        [Key]
        public long id { get; set; }
        public long series_id { get; set; }
        public long? league_id { get; set; }
        public string? image_path { get; set; }
        public string? league_type { get; set; }
        public string? series_name { get; set; }
        public DateTime? starting_at { get; set; }
    }
    public class SeriesJson
    {
        [Key]
        public string Series { get; set; } = "";
    }
    #endregion

    #region Points Table
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Standing
    {
        public string? team_code { get; set; }
        public string? team_name { get; set; }
        public string? team_image_path { get; set; }
        public int? won { get; set; }
        public int? draw { get; set; }
        public int? lost { get; set; }
        public int? played { get; set; }
        public int? points { get; set; }
        public int? noresult { get; set; }
        public int? position { get; set; }
        public int? runs_for { get; set; }
        public int? legend_id { get; set; }
        public double? overs_for { get; set; }
        public string? recent_form { get; set; }
        public int? runs_against { get; set; }
        public double? overs_against { get; set; }
        public double? netto_run_rate { get; set; }
        public string? legend_description { get; set; }
    }
    public class Standings
    {
        public int id { get; set; }
        public int league_id { get; set; }
        public string? image_path { get; set; }
        public string? league_type { get; set; }
        public string? series_name { get; set; }
        public string? season_name { get; set; }
        public List<Standing>? standings { get; set; }
    }
    public class StandingsJson
    {
        [Key]
        public string Standings { get; set; } = "";
    }
    #endregion

    #region Teams
    public class Team
    {
        [Key]
        public int id { get; set; }
        public string? code { get; set; }
        public string? name { get; set; }
        public string? image_path { get; set; }
    }
    public class SeriesTeams
    {
        [Key]
        public int id { get; set; }
        public int? league_id { get; set; }
        public string? image_path { get; set; }
        public string? league_type { get; set; }
        public string? season_name { get; set; }
        public string? series_name { get; set; }
        public List<Team>? teams { get; set; }
    }
    public class SeriesTeamsJson
    {
        [Key]
        public string SeriesTeams { get; set; } = "";
    }

    public class LastSeries
    {
        [Key]
        public long player_id { get; set; }
        public string? fullname { get; set; }
        public int? points { get; set; }
    }
    public class UpdateSeriesPoints
    {
        [Key]
        public long player_id { get; set; }
        public long series_id { get; set; }
        public decimal? points { get; set; }
    }

    public class SeriesTeamPlayers
    {
        [Key]
        public long id { get; set; }
        public long player_id { get; set; }
        public long team_id { get; set; }
        public long season_id { get; set; }
        public decimal? points { get; set; }
    }
    #endregion 
}
