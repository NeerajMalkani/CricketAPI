using CricketAPI.Helpers;

namespace CricketAPI.Repositories
{
    public class PlayersRepository
    {
        public List<Continents> GetPlayers(DataContext context)
        {
            List<Continents> continents = new List<Continents>();
            try
            {
                continents = context.Continents.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return continents;
        }
    }
}
