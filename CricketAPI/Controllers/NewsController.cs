using CricketAPI.Entites;
using CricketAPI.Helpers;
using CricketAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CricketAPI.Controllers
{
    [Route("api/news")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly DataContext _db;

        public NewsController(DataContext dbContext)
        {
            _db = dbContext;
        }

        #region Get News
        [HttpGet]
        [Route("list")]
        public Response GetNews()
        {
            Response response = new Response();
            try
            {
                List<News> news = new NewsRepository().GetNews(_db);
                if (news.Any())
                {
                    news = news.Take(10).ToList();
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, news);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, news);
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
