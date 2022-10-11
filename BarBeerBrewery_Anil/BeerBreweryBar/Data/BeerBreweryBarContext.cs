using Microsoft.EntityFrameworkCore;
using BeerBreweryBar.Models;

namespace BeerBreweryBar.Data
{
    public class BeerBreweryBarContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public BeerBreweryBarContext(DbContextOptions<BeerBreweryBarContext> options)
            : base(options)
        {
        }

        public DbSet<Beer> Beer { get; set; }
        public DbSet<Bar> Bar { get; set; }
        public DbSet<Brewery> Brewery { get; set; }
        public DbSet<BarBeer> BarBeers { get; set; }
        //public DbSet<BreweryBeer> BreweryBeers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Beer>().Property(p => p.PercentageAlcoholByVolume).HasColumnType("decimal(18,4)");
        }
    }
}
