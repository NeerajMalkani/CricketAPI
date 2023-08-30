using CricketAPI.Entites;
using CricketAPI.Helpers;

namespace CricketAPI.Repositories
{
    public class YouTubeRepository
    {
        public int InsertYoutubeVideos(DataContext context, YouTubeVideos youTubeVideos)
        {
            int rowsAffected = 0;
            try
            {
                context.YouTubeVideos.Add(youTubeVideos);
                rowsAffected = 1;
            }
            catch (Exception)
            {
                rowsAffected = 0;
            }
            return rowsAffected;
        }

        public List<YouTubeVideos> GetYouTubeVideos(DataContext context)
        {
            List<YouTubeVideos> youTubeVideos = new List<YouTubeVideos>();
            try
            {
                youTubeVideos = context.YouTubeVideos.ToList();
            }
            catch (Exception)
            {
                youTubeVideos = new List<YouTubeVideos>();
            }
            return youTubeVideos;
        }
    }
}
