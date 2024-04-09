using CricketAPI.Entites;
using CricketAPI.Helpers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CricketAPI.Repositories
{
    public class UserRepository
    {
        public async Task<UserSaveTeamResponse> InsertUserTeam(DataContext context, UserSaveTeamRequest userSaveTeamRequest)
        {
            UserSaveTeamResponse userSaveTeamResponse = new UserSaveTeamResponse();
            try
            {
                if (userSaveTeamRequest != null && userSaveTeamRequest.userTeam != null && userSaveTeamRequest.userTeamPlayers != null && userSaveTeamRequest.userTeamPlayers.Count > 0)
                {
                    UserTeam? userTeam = context.UserTeam.ToList().Where(el => el.user_id == userSaveTeamRequest.userTeam.user_id && el.fixture_id == userSaveTeamRequest.userTeam.fixture_id && el.contest_id == userSaveTeamRequest.userTeam.contest_id && el.team_name == userSaveTeamRequest.userTeam.team_name).FirstOrDefault();
                    if (userTeam == null || userTeam?.id == 0)
                    {
                        context.UserTeam.Add(userSaveTeamRequest.userTeam);
                        await context.SaveChangesAsync();
                    }
                    foreach (UserTeamPlayers team in userSaveTeamRequest.userTeamPlayers)
                    {
                        team.user_team_id = userTeam == null ? userSaveTeamRequest.userTeam.id : userTeam.id;
                        context.UserTeamPlayers.Add(team);
                    }
                    await context.SaveChangesAsync();
                    userSaveTeamResponse.user_team_id = userTeam == null ? userSaveTeamRequest.userTeam.id : userTeam.id;
                    userSaveTeamResponse.rowsAffected = 1;
                }

            }
            catch (Exception)
            {
                userSaveTeamResponse.rowsAffected = 0;
            }
            return userSaveTeamResponse;
        }

        public UserTeamList? GetUserTeam(DataContext context, UserTeamRequest userTeamRequest)
        {
            UserTeamList? userTeamList = new UserTeamList();
            try
            {
                List<UserTeamJson> userTeamJsons = context.UserTeamJson.FromSqlRaw("CALL `cric_Get_UserTeam`(" + userTeamRequest.fixture_id + ", '" + userTeamRequest.user_id + "')").ToList();
                if (userTeamJsons.Count > 0)
                {
                    userTeamList = JsonConvert.DeserializeObject<UserTeamList>(userTeamJsons[0].UserTeamList) ?? throw new ArgumentException();
                }

            }
            catch (Exception)
            {
                userTeamList = new UserTeamList();
            }
            return userTeamList;
        }

        public UserTeamWithPlayers? GetUserTeamWithPlayers(DataContext context, UserTeamRequest userTeamRequest)
        {
            UserTeamWithPlayers? userTeamWithPlayers = new UserTeamWithPlayers();
            try
            {
                List<UserTeamWithPlayersJson> userTeamJsons = context.UserTeamWithPlayersJson.FromSqlRaw("CALL `cric_Get_UserTeamWithPlayers`(" + userTeamRequest.fixture_id + ", '" + userTeamRequest.user_id + "')").ToList();
                if (userTeamJsons.Count > 0)
                {
                    userTeamWithPlayers = JsonConvert.DeserializeObject<UserTeamWithPlayers>(userTeamJsons[0].UserTeamWithPlayers) ?? throw new ArgumentException();
                }

            }
            catch (Exception)
            {
                userTeamWithPlayers = new UserTeamWithPlayers();
            }
            return userTeamWithPlayers;
        }

        public UserAllTeamWithPlayers? GetUserAllTeamWithPlayers(DataContext context, UserTeamRequest userTeamRequest)
        {
            UserAllTeamWithPlayers? userAllTeamWithPlayers = new UserAllTeamWithPlayers();
            try
            {
                List<UserAllTeamWithPlayersJson> userAllTeamJsons = context.UserAllTeamWithPlayersJson.FromSqlRaw("CALL `cric_Get_UserAllTeamWithPlayers`('" + userTeamRequest.user_id + "')").ToList();
                if (userAllTeamJsons.Count > 0)
                {
                    userAllTeamWithPlayers = JsonConvert.DeserializeObject<UserAllTeamWithPlayers>(userAllTeamJsons[0].UserAllTeamWithPlayers) ?? throw new ArgumentException();
                }

            }
            catch (Exception)
            {
                userAllTeamWithPlayers = new UserAllTeamWithPlayers();
            }
            return userAllTeamWithPlayers;
        }

        public UserTeamLineup? GetUserTeamPlayers(DataContext context, UserTeamRequest userTeamRequest)
        {
            UserTeamLineup? userTeamLineup = new UserTeamLineup();
            try
            {
                List<UserTeamLineupJson> userTeamJsons = context.UserTeamLineupJson.FromSqlRaw("CALL `cric_Get_UserTeamPlayers`(" + userTeamRequest.fixture_id + ", " + userTeamRequest.user_team_id + ", '" + userTeamRequest.user_id + "')").ToList();
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

        public async Task<int> UpdateUserTeam(DataContext context, List<UserTeamPlayers>? userTeamPlayers)
        {
            int rowsAffected = 0;
            try
            {
                if (userTeamPlayers != null && userTeamPlayers.Count > 0)
                {
                    foreach (UserTeamPlayers userTeamPlayer in userTeamPlayers)
                    {
                        UserTeamPlayers? team = context.UserTeamPlayers.FirstOrDefault(item => item.user_team_id == userTeamPlayer.user_team_id && item.player_id == userTeamPlayer.player_id);
                        if (team != null)
                        {
                            team.is_captain = userTeamPlayer.is_captain;
                            team.is_vice_captain = userTeamPlayer.is_vice_captain;
                            context.UserTeamPlayers.Update(team);
                            await context.SaveChangesAsync();
                            rowsAffected++;
                        }
                    }
                }

            }
            catch (Exception)
            {
                rowsAffected = 0;
            }
            return rowsAffected;
        }

        public async Task<int> UpdateUserTeamPlayers(DataContext context, List<UserTeamPlayers>? userTeamPlayers)
        {
            int rowsAffected = 0;
            try
            {
                if (userTeamPlayers != null && userTeamPlayers.Count > 0)
                {
                    List<UserTeamPlayers>? prevTeam = context.UserTeamPlayers.Where(item => item.user_team_id == userTeamPlayers[0].user_team_id).ToList();
                    if (prevTeam != null && prevTeam.Count > 0)
                    {
                        context.UserTeamPlayers.RemoveRange(prevTeam);
                        await context.SaveChangesAsync();
                        context.UserTeamPlayers.AddRange(userTeamPlayers);
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

        public async Task<int> DeleteUserTeamPlayers(DataContext context, UserDeleteTeamRequest userDeleteTeamRequest)
        {
            int rowsAffected = 0;
            try
            {
                if (userDeleteTeamRequest != null && userDeleteTeamRequest.user_team_id != null)
                {
                    List<UserTeamPlayers>? prevTeamPlayers = context.UserTeamPlayers.Where(item => item.user_team_id == userDeleteTeamRequest.user_team_id).ToList();
                    if (prevTeamPlayers != null && prevTeamPlayers.Count > 0)
                    {
                        context.UserTeamPlayers.RemoveRange(prevTeamPlayers);
                        await context.SaveChangesAsync();
                        UserTeam userTeam = context.UserTeam.Where(item => item.id == userDeleteTeamRequest.user_team_id).First();
                        if (userTeam != null)
                        {
                            context.UserTeam.Remove(userTeam);
                            await context.SaveChangesAsync();
                        }
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

        public async Task<int> InsertUser(DataContext context, Users users)
        {
            int rowsAffected = 0;
            try
            {
                if (users != null)
                {
                    Users? userDetails = context.Users.Where(item => item.id == users.id).FirstOrDefault();
                    if(userDetails != null)
                    {
                        userDetails.fullname = users.fullname;
                        userDetails.gender = users.gender;
                        userDetails.dob = users.dob;
                    } else
                    {
                        await context.Users.AddAsync(users);
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

        public Users? GetUser(DataContext context, Users users)
        {
            Users? getUser = new Users();
            try
            {
                if (users != null)
                {
                    getUser = context.Users.Where(item => item.id == users.id).FirstOrDefault();
                }

            }
            catch (Exception)
            {
                getUser = new Users();
            }
            return getUser;
        }

        public async Task<int> InsertTransactions(DataContext context, Transactions transactions)
        {
            int rowsAffected = 0;
            try
            {
                if (transactions != null)
                {
                    await context.Transactions.AddAsync(transactions);
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
    }
}
