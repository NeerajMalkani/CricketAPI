using CricketAPI.Entites;
using CricketAPI.Helpers;

namespace CricketAPI.Repositories
{
    public class YouTubeRepository
    {
        public async Task<int> InsertYoutubeVideos(DataContext context, YouTubeVideos youTubeVideos)
        {
            int rowsAffected = 0;
            try
            {
                context.YouTubeVideos.Add(youTubeVideos);
                await context.SaveChangesAsync();
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

        public async Task<int> DeleteYoutubeVideos(DataContext context, YouTubeVideos youTubeVideos)
        {
            int rowsAffected = 0;
            try
            {
                YouTubeVideos youTubeVideosRemove = context.YouTubeVideos.Where(d => d.id == youTubeVideos.id).First();
                context.YouTubeVideos.Remove(youTubeVideosRemove);
                await context.SaveChangesAsync();
                rowsAffected = 1;
            }
            catch (Exception)
            {
                rowsAffected = 0;
            }
            return rowsAffected;
        }
    }
}
