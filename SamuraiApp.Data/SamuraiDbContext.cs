using Microsoft.EntityFrameworkCore;
using SumaraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SamuraiApp.Data
{
    public class SamuraiDbContext : DbContext
    {
        public DbSet<Samurai> Samurais { get; set; }

        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Clan> Clans { get; set; }

        public DbSet<Battle> Battles  { get; set; }

        // One way to add connection string
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Data Source=(localDB)\\MSSQLLocalDB; Initial Catalog =SamuraiAppData ");
            // not needed to call base method
            //base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SamuraiBattle>().HasKey(sb => new { sb.SamuraiId, sb.BattleId});
            // since Horse is not added as DBSet but as navigation property inside Samurai,
            // EF will create a table as Horse, but to pluralize that we use fluent api here
            modelBuilder.Entity<Horse>().ToTable("Horses");
            base.OnModelCreating(modelBuilder);
        }

    }
}
