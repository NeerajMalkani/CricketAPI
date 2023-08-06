using CricketAPI.Entites;
using CricketAPI.Helpers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CricketAPI.Repositories
{
    public class NewsRepository
    {
        #region Fixtures
        public List<News> GetNews(DataContext context)
        {
            List<News> news = new List<News>();
            try
            {
                List<NewsJson> newsJson = context.NewsJson.FromSqlRaw("CALL `cric_Get_News`()").ToList();
                if (newsJson[0].News != null)
                {
                    news = JsonConvert.DeserializeObject<List<News>>(newsJson[0].News) ?? throw new ArgumentException();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return news;
        }
        #endregion
    }
}
