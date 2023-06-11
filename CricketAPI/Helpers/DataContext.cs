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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Fixtures>()
                .OwnsOne(fixture => fixture.localteam, builder => { builder.ToJson(); })
                .OwnsOne(fixture => fixture.visitorteam, builder => { builder.ToJson(); })
                .OwnsOne(fixture => fixture.umpires, builder => { builder.ToJson(); })
                .OwnsOne(fixture => fixture.venue, builder => { builder.ToJson(); })
                .OwnsOne(fixture => fixture.man_of_the_match, builder => { builder.ToJson(); })
                .OwnsOne(fixture => fixture.man_of_the_series, builder => { builder.ToJson(); });
        }

    }
}
