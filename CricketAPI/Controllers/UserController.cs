using CricketAPI.Entites;
using CricketAPI.Helpers;
using CricketAPI.Repositories;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CricketAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _db;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment env;
        private readonly ILogger _logger;
        public UserController(DataContext dbContext, Microsoft.AspNetCore.Hosting.IHostingEnvironment env, ILogger<UserController> logger)
        {
            _db = dbContext;
            this.env = env;
            _logger = logger;
        }

        #region Users
        [HttpPost]
        [Route("")]
        public async Task<Response> InsertUser([FromBody] Users users)
        {
            Response response = new Response();
            try
            {
                int rowsAffected = await new UserRepository().InsertUser(_db, users);
                if (rowsAffected > 0)
                {
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }

        [HttpGet]
        [Route("")]
        public Response GetUser([FromQuery] Users users)
        {
            Response response = new Response();
            try
            {
                Users? getUser = new UserRepository().GetUser(_db, users);
                if (getUser != null)
                {
                    List<Users> users1 = new List<Users>();
                    users1.Add(getUser);
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, users1);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }

        [HttpGet]
        [Route("gcmtokens")]
        public Response GetGCMTokens([FromQuery] Users users)
        {
            Response response = new Response();
            try
            {
                List<GCMTokens> gCMTokens = new UserRepository().GetGCMTokens(_db, users);
                if (gCMTokens != null)
                {
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, gCMTokens);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }
        #endregion

        #region Transactions
        [HttpPost]
        [Route("transactions")]
        public async Task<Response> InsertTransaction([FromBody] Transactions transactions)
        {
            Response response = new Response();
            try
            {
                int rowsAffected = await new UserRepository().InsertTransactions(_db, transactions);
                if (rowsAffected > 0)
                {
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }
        #endregion

        #region User teams
        [HttpPost]
        [Route("teams")]
        public async Task<Response> InsertUserTeamAsync([FromBody] UserSaveTeamRequest userSaveTeamRequest)
        {
            Response response = new Response();
            try
            {
                UserSaveTeamResponse userSaveTeamResponse = await new UserRepository().InsertUserTeam(_db, userSaveTeamRequest);
                List<UserSaveTeamResponse> userSaveTeamResponses = new List<UserSaveTeamResponse>();
                if (userSaveTeamResponse.rowsAffected > 0)
                {
                    userSaveTeamResponses.Add(userSaveTeamResponse);
                    response.Data = userSaveTeamResponses;
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, userSaveTeamResponses);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, userSaveTeamResponses);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }

        [HttpGet]
        [Route("teams")]
        public Response GetUserTeamPlayers([FromQuery] UserTeamRequest userTeamRequest)
        {
            Response response = new Response();
            try
            {
                UserTeamList? userTeamList = new UserRepository().GetUserTeam(_db, userTeamRequest);
                List<UserTeamList> userTeamLists = new List<UserTeamList>();
                if (userTeamList != null && userTeamList.teams != null)
                {
                    userTeamLists.Add(userTeamList);
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, userTeamLists);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, userTeamLists);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }

        [HttpGet]
        [Route("teamswithplayers")]
        public Response GetUserTeamWithPlayers([FromQuery] UserTeamRequest userTeamRequest)
        {
            Response response = new Response();
            try
            {
                UserTeamWithPlayers? userTeamWithPlayers = new UserRepository().GetUserTeamWithPlayers(_db, userTeamRequest);
                List<UserTeamWithPlayers> userTeamsWithPlayers = new List<UserTeamWithPlayers>();
                if (userTeamWithPlayers != null && userTeamWithPlayers.teams != null)
                {
                    userTeamsWithPlayers.Add(userTeamWithPlayers);
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, userTeamsWithPlayers);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, userTeamsWithPlayers);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }

        [HttpGet]
        [Route("allteamswithplayers")]
        public Response GetUserAllTeamWithPlayers([FromQuery] UserTeamRequest userTeamRequest)
        {
            Response response = new Response();
            try
            {
                UserAllTeamWithPlayers? userAllTeamWithPlayers = new UserRepository().GetUserAllTeamWithPlayers(_db, userTeamRequest);
                List<UserAllTeamWithPlayers> userAllTeamsWithPlayers = new List<UserAllTeamWithPlayers>();
                if (userAllTeamWithPlayers != null && userAllTeamWithPlayers.teams != null)
                {
                    userAllTeamsWithPlayers.Add(userAllTeamWithPlayers);
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, userAllTeamsWithPlayers);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, userAllTeamsWithPlayers);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }

        [HttpGet]
        [Route("teams/players")]
        public Response GetUserTeam([FromQuery] UserTeamRequest userTeamRequest)
        {
            Response response = new Response();
            try
            {
                UserTeamLineup? userTeamLineup = new UserRepository().GetUserTeamPlayers(_db, userTeamRequest);
                List<UserTeamLineup> userTeamLineups = new List<UserTeamLineup>();
                if (userTeamLineup != null && userTeamLineup.teamlineup != null)
                {
                    userTeamLineups.Add(userTeamLineup);
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, userTeamLineups);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, userTeamLineups);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }

        [HttpPut]
        [Route("teams")]
        public async Task<Response> UpdateUserTeamAsync([FromBody] UserUpdateTeamRequest userUpdateTeamPlayers)
        {
            Response response = new Response();
            try
            {
                int rowsAffected = await new UserRepository().UpdateUserTeam(_db, userUpdateTeamPlayers.userTeamPlayers);
                if (rowsAffected > 0)
                {
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }

        [HttpPut]
        [Route("teams/players")]
        public async Task<Response> UpdateUserTeamPlayersAsync([FromBody] UserUpdateTeamRequest userUpdateTeamPlayers)
        {
            Response response = new Response();
            try
            {
                int rowsAffected = await new UserRepository().UpdateUserTeamPlayers(_db, userUpdateTeamPlayers.userTeamPlayers);
                if (rowsAffected > 0)
                {
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }

        [HttpDelete]
        [Route("teams")]
        public async Task<Response> DeleteUserTeam([FromBody] UserDeleteTeamRequest userDeleteTeamRequest)
        {
            Response response = new Response();
            try
            {
                int rowsAffected = await new UserRepository().DeleteUserTeamPlayers(_db, userDeleteTeamRequest);
                if (rowsAffected > 0)
                {
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }

        [HttpPost]
        [Route("sendmessagetodevice")]
        public async Task<Response> SendNotificationToDevice([FromBody] MessageRequest messageRequest)
        {
            Response response = new Response();
            try
            {
                var path = env.ContentRootPath;
                path = path + @"starselector-af5b2-firebase-adminsdk-zohvq-2cd40d052e.json";
                _logger.LogInformation(path);
                FirebaseApp app = null;
                try
                {
                    app = FirebaseApp.Create(new AppOptions()
                    {
                        Credential = GoogleCredential.FromFile(path)
                    }, "starselector -af5b2");
                }
                catch (Exception ex)
                {
                    app = FirebaseApp.GetInstance("starselector-af5b2");
                }
                _logger.LogInformation(app.ToString());
                var message = new Message()
                {
                    Notification = new Notification
                    {
                        Title = messageRequest.Title,
                        Body = messageRequest.Body
                    },
                    //Data = new Dictionary<string, string>()
                    //{
                    //    ["CustomData"] = "Custom Data"
                    //},
                    Token = messageRequest.DeviceToken
                };
                _logger.LogInformation(message.ToString());
                var fcm = FirebaseMessaging.GetMessaging(app);
                var result = await fcm.SendAsync(message).ConfigureAwait(false);
                _logger.LogInformation(result);
                if (!string.IsNullOrEmpty(result))
                {
                    List<string> lstResult = new List<string>();
                    lstResult.Add(result);
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, lstResult);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }
        #endregion    
    }
}
