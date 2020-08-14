using Microsoft.EntityFrameworkCore;
using SumaraiApp.Domain;


namespace SamuraiApp.Data
{
    public class SamuraiDbContext : DbContext
    {
        public DbSet<Samurai> Samurais { get; set; }

        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Clan> Clans { get; set; }

        public DbSet<Battle> Battles { get; set; }

        public DbSet<SamuraiBattleStat> SamuraiBattleStats { get; set; }

        public SamuraiDbContext(DbContextOptions<SamuraiDbContext> options): base (options)
        {
            // no logner tracking query
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public SamuraiDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // if this check is not performed then test will fail, because we are also using in memory provider in our tests
            // and we can't use two providers at the same time
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("Data Source=(localDB)\\MSSQLLocalDB; Initial Catalog =SamuraiTestData");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SamuraiBattle>().HasKey(sb => new { sb.SamuraiId, sb.BattleId });
            // since Horse is not added as DBSet but as navigation property inside Samurai,
            // EF will create a table as Horse, but to pluralize that we use fluent api here
            modelBuilder.Entity<Horse>().ToTable("Horses");


            modelBuilder.Entity<SamuraiBattleStat>().HasNoKey().ToView("SamuraiBattleStats");
            base.OnModelCreating(modelBuilder);
        }

    }
}
