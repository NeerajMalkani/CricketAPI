using CricketAPI.Entites;
using CricketAPI.Helpers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CricketAPI.Repositories
{
    public class UserRepository
    {
        public async Task<int> InsertUserTeam(DataContext context, List<UserTeam> userTeams)
        {
            int rowsAffected = 0;
            try
            {
                if (userTeams != null && userTeams.Count() > 0)
                {
                    foreach (var team in userTeams)
                    {
                        context.UserTeam.Add(team);
                    }
                    await context.SaveChangesAsync();
                    rowsAffected++;
                }

            }
            catch (Exception)
            {
                rowsAffected = 0;
            }
            return rowsAffected;
        }

        public UserTeamLineup? GetUserTeam(DataContext context, UserTeamRequest userTeamRequest)
        {
            UserTeamLineup? userTeamLineup = new UserTeamLineup();
            try
            {
                List<UserTeamJson> userTeamJsons = context.UserTeamJson.FromSqlRaw("CALL `cric_Get_UserTeam`(" + userTeamRequest.fixture_id + ", '" + userTeamRequest.user_id + "')").ToList();
                if (userTeamJsons.Count > 0)
                {
                    userTeamLineup = JsonConvert.DeserializeObject<UserTeamLineup>(userTeamJsons[0].UserTeamLineup) ?? throw new ArgumentException();
                }

            }
            catch (Exception)
            {
                userTeamLineup = new UserTeamLineup();
            }
            return userTeamLineup;
        }

        public async Task<int> UpdateUserTeam(DataContext context, UserTeam userTeam)
        {
            int rowsAffected = 0;
            try
            {
                if (userTeam != null)
                {
                    UserTeam? team = context.UserTeam.FirstOrDefault(item => item.user_id == userTeam.user_id && item.fixture_id == userTeam.fixture_id && item.player_id == userTeam.player_id);
                    if (team != null)
                    {
                        team.is_captain = userTeam.is_captain;
                        team.is_vice_captain = userTeam.is_vice_captain;
                        context.UserTeam.Update(team);
                        await context.SaveChangesAsync();
                        rowsAffected++;
                    }
                }

            }
            catch (Exception)
            {
                rowsAffected = 0;
            }
            return rowsAffected;
        }
    }
}
