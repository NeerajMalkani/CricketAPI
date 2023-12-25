using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CricketAPI.Entites
{

    #region Insert Team
    public class UserTeam
    {
        [Key]
        public long id { get; set; }
        public string? user_id { get; set; }
        public long? fixture_id { get; set; }
        public string? team_name { get; set; }

    }
    public class UserTeamPlayers
    {
        [Key]
        public long id { get; set; }
        public long? user_team_id { get; set; }
        public long? team_id { get; set; }
        public long? player_id { get; set; }
        public bool? is_captain { get; set; } = false;
        public bool? is_vice_captain { get; set; } = false;

    }
    public class UserSaveTeamRequest
    {
        public UserTeam? userTeam { get; set; }
        public List<UserTeamPlayers>? userTeamPlayers { get; set; }
    }
    public class UserUpdateTeamRequest
    {
        public List<UserTeamPlayers>? userTeamPlayers { get; set; }
    }
    public class UserDeleteTeamRequest
    {
        public long? user_team_id { get; set; }

    }
    public class UserSaveTeamResponse
    {
        public int rowsAffected { get; set; }
        public long user_team_id { get; set; }
    }
    #endregion

    #region Get Request
    public class UserTeamRequest
    {
        public string? user_id { get; set; }
        public long? user_team_id { get; set; }
        public long? fixture_id { get; set; }
    }
    #endregion

    #region Team Response
    public class UserTeamJson
    {
        [Key]
        public string UserTeamList { get; set; } = "";
    }
    public class UserTeamList
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
        public List<TeamList>? teams { get; set; }
        public string? starting_at { get; set; }
    }
    public class TeamList
    {
        [Key]
        public long id { get; set; }
        public string? team_name { get; set; }
    }
    #endregion

    #region Team Players Response
    public class UserTeamLineupJson
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
    #endregion

    #region Team With Players Response
    public class UserTeamWithPlayersJson
    {
        [Key]
        public string UserTeamWithPlayers { get; set; } = "";
    }
    public class UserTeamWithPlayers
    {
        public int id { get; set; }
        public string? note { get; set; }
        public string? type { get; set; }
        public string? round { get; set; }
        public string? status { get; set; }
        public int? league_id { get; set; }
        public int? series_id { get; set; }
        public string? stage_name { get; set; }
        public string? league_name { get; set; }
        public string? season_name { get; set; }
        public List<TeamWithPlayers>? teams { get; set; }
        public DateTime? starting_at { get; set; }
    }

    [Keyless]
    public class TeamWithPlayers
    {
        public List<Teamlineup>? teamlineup { get; set; }
        public int? user_team_id { get; set; }
        public string? user_team_name { get; set; }
    } 
    #endregion
}
