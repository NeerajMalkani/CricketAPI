using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace CricketAPI
{

    [Owned]
    public class Localteam
    {
        public string? code { get; set; }
        public string? name { get; set; }
        public string? image_path { get; set; }
    }


    [Owned]
    public class Visitorteam
    {
        public string? code { get; set; }
        public string? name { get; set; }
        public string? image_path { get; set; }
    }


    [Owned]
    public class ManOfTheMatch
    {
        [Key]
        public long? id { get; set; }
        public string? name { get; set; }
        public string? image_path { get; set; }
    }


    [Owned]
    public class ManOfTheSeries
    {
        public string? name { get; set; }
        public string? image_path { get; set; }
    }


    [Owned]
    public class Umpires
    {
        public string? refree { get; set; }
        public string? tv_umpire { get; set; }
        public string? first_umpire { get; set; }
        public string? second_umpire { get; set; }
    }

    [Owned]
    public class Venue
    {
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
        public int? Localteamid { get; set; }
        public Localteam? Localteam { get; set; }
        public int? Visitorteamid { get; set; }
        public Visitorteam? Visitorteam { get; set; }
        public int? Venueid { get; set; }
        public Venue? Venue { get; set; }
        public int? Umpiresid { get; set; }
        public Umpires? Umpires { get; set; }
        public string? note { get; set; }
        public string? status { get; set; }
        public string? elected { get; set; }
        public string? starting_at { get; set; }
        public int rpc_overs { get; set; }
        public int rpc_target { get; set; }
        public string? follow_on { get; set; }
        public string? super_over { get; set; }        
        public string? draw_noresult { get; set; }
        public int toss_won_team_id { get; set; }
        public int winner_team_id { get; set; }
        public int? ManOfTheMatchid { get; set; }
        public ManOfTheMatch? ManOfTheMatch { get; set; }
        public int? ManOfTheSeriesid { get; set; }
        public ManOfTheSeries? ManOfTheSeries { get; set; }
        public int total_overs_played { get; set; }
    }
}
