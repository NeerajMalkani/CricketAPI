using Microsoft.EntityFrameworkCore;

namespace CricketAPI.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
             : base(options)
        {
        }

        public DbSet<Teams> Teams => Set<Teams>();
        public DbSet<Fixtures> Fixtures => Set<Fixtures>();
        public DbSet<FixturesJson> FixturesJson => Set<FixturesJson>();

    }
}
