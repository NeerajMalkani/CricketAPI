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


        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fixtures>().OwnsOne(p => p.Localteam);
            modelBuilder.Entity<Fixtures>().OwnsOne(p => p.Visitorteam);
            modelBuilder.Entity<Fixtures>().OwnsOne(p => p.Venue);
            modelBuilder.Entity<Fixtures>().OwnsOne(p => p.Umpires);
            modelBuilder.Entity<Fixtures>().OwnsOne(p => p.ManOfTheMatch);
            modelBuilder.Entity<Fixtures>().OwnsOne(p => p.ManOfTheSeries);
        }
        #endregion

    }
}
