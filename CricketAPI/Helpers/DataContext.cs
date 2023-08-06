using CricketAPI.Entites;
using Microsoft.EntityFrameworkCore;

namespace CricketAPI.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
             : base(options)
        {
        }

        public DbSet<Countries> Countries => Set<Countries>();
        public DbSet<Leagues> Leagues => Set<Leagues>();
        public DbSet<Seasons> Seasons => Set<Seasons>();
        public DbSet<Stages> Stages => Set<Stages>();
        public DbSet<Teams> Teams => Set<Teams>();
        public DbSet<Players> Players => Set<Players>();
        public DbSet<Venues> Venues => Set<Venues>();
        public DbSet<Officials> Officials => Set<Officials>();
        public DbSet<Fixtures> Fixtures => Set<Fixtures>();

        public DbSet<FixturesJson> FixturesJson => Set<FixturesJson>();
        public DbSet<ScorecardJson> ScorecardJson => Set<ScorecardJson>();
        public DbSet<LineupJson> LineupJson => Set<LineupJson>();
        public DbSet<BallJson> BallJson => Set<BallJson>();
        public DbSet<SeriesJson> SeriesJson => Set<SeriesJson>();
        public DbSet<NewsJson> NewsJson => Set<NewsJson>();

    }
}
