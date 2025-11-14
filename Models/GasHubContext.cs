using Microsoft.EntityFrameworkCore;
using GasHub.Models;

namespace GasHub.Data
{
    public class GasHubContext : DbContext
    {
        public GasHubContext(DbContextOptions<GasHubContext> options)
            : base(options)
        {
        }

        public DbSet<Gashub> Gashubs { get; set; }
        public DbSet<GasPriceRecord> GasPriceRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Table names
            modelBuilder.Entity<Gashub>().ToTable("Gashub");
            modelBuilder.Entity<GasPriceRecord>().ToTable("GasPriceRecord");

            // Precision for prices
            for (int i = 1; i <= 10; i++)
            {
                modelBuilder.Entity<GasPriceRecord>()
                    .Property(typeof(decimal), $"Price{i}")
                    .HasPrecision(10, 4);
            }
        }
    }
}
