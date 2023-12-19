using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CricketAPI.Entites
{
    public class UserTeam
    {
        [Key]
        public long id { get; set; }
        public string? user_id { get; set; }
        public long? fixture_id { get; set; }
        public long? team_id { get; set; }
        public long? player_id { get; set; }
        public bool? is_captain { get; set; } = false;
        public bool? is_vice_captain { get; set; } = false;

    }

    public class UserTeamRequest
    {
        public string? user_id { get; set; }
        public long fixture_id { get; set; }
    }

    public class UserTeamJson
    {
        [Key]
        public string UserTeamLineup { get; set; } = "";
    }

    public class UserTeamLineup
    {
        [Key]
        public long id { get; set; }
        public long? league_id { get; set; }
        public long? series_id { get; set; }
        public string? league_name { get; set; }
        public string? season_name { get; set; }
        public string? stage_name { get; set; }
        public string? round { get; set; }
        public string? note { get; set; }
        public string? type { get; set; }
        public string? status { get; set; }
        public List<Teamlineup>? teamlineup { get; set; }
        public string? starting_at { get; set; }
    }

    [Keyless]
    public class Teamlineup
    {
        public long? team_id { get; set; }
        public string? team_name { get; set; }
        public long? player_id { get; set; }
        public decimal? player_points { get; set; }
        public string? player_fullname { get; set; }
        public string? player_image_path { get; set; }
        public string? player_position_name { get; set; }
        public bool? is_captain { get; set; }
        public bool? is_vice_captain { get; set; }
    }


}
