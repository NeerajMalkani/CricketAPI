using Microsoft.EntityFrameworkCore;

namespace CricketAPI.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
             : base(options)
        {
        }

        public DbSet<Continents> Continents => Set<Continents>();
    }
}
