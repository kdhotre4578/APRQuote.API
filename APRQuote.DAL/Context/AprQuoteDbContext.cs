using APRQuote.DAL.Configurations;
using APRQuote.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace APRQuote.DAL.Context
{
    public class AprQuoteDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<QuoteType> QuoteTypes { get; set; }
        
        public DbSet<APRPercentRange> APRPercentRanges { get; set; }

        public DbSet<Quote> Quotes { get; set; }

        readonly string connectionString;

        public AprQuoteDbContext(string _connectionString)
        {
            connectionString = _connectionString;
        }

        public AprQuoteDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VehicleConfig());
            modelBuilder.ApplyConfiguration(new QuoteTypeConfig());
            modelBuilder.ApplyConfiguration(new APRPercentRangeConfig());
            modelBuilder.ApplyConfiguration(new QuoteConfig());
        }
    }
}
