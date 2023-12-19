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
                        context.UserTeam.Add(team);
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

        public List<UserTeamResponse> GetUserTeam(DataContext context, UserTeamRequest userTeamRequest)
        {
            List<UserTeamResponse>? userTeamResponse = new List<UserTeamResponse>();
            try
            {
                userTeamResponse = context.UserTeamResponse.FromSqlRaw("CALL `cric_Get_UserTeam`(" + userTeamRequest.fixture_id + ", '" + userTeamRequest.user_id + "')").ToList();
            }
            catch (Exception)
            {
                userTeamResponse = new List<UserTeamResponse>();
            }
            return userTeamResponse;
        }
    }
}
