using CricketAPI.Entites;
using CricketAPI.Helpers;
using CricketAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CricketAPI.Controllers
{
    [Route("api/youtubevideos")]
    [ApiController]
    public class YouTubeVideosController : ControllerBase
    {
        private readonly DataContext _db;

        public YouTubeVideosController(DataContext dbContext)
        {
            _db = dbContext;
        }

        #region Insert Youtube videos
        [HttpPost]
        [Route("insert")]
        public async Task<Response> InsertYoutubeVideosAsync([FromBody] YouTubeVideos youTubeVideos)
        {
            Response response = new Response();
            try
            {
                int rowsAffected = await new YouTubeRepository().InsertYoutubeVideos(_db, youTubeVideos);
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
        [Route("list")]
        public Response GetYoutubeVideos()
        {
            Response response = new Response();
            try
            {
                List<YouTubeVideos> youTubeVideos = new YouTubeRepository().GetYouTubeVideos(_db);
                if (youTubeVideos.Any())
                {
                    Common.CreateResponse(HttpStatusCode.OK, "Success", "Success", out response, youTubeVideos);
                }
                else
                {
                    Common.CreateResponse(HttpStatusCode.NoContent, "Success", "No data", out response, youTubeVideos);
                }
            }
            catch (Exception ex)
            {
                Common.CreateErrorResponse(HttpStatusCode.BadRequest, out response, ex);
            }
            return response;
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<Response> DeleteYoutubeVideosAsync([FromQuery] YouTubeVideos youTubeVideos)
        {
            Response response = new Response();
            try
            {
                int rowsAffected = await new YouTubeRepository().DeleteYoutubeVideos(_db, youTubeVideos);
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
    }
}
