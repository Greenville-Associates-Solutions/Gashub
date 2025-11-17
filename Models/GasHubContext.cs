using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Gas.Models
{
    public partial class GashubContext : DbContext
    {
        public GashubContext()
        {
        }

        public GashubContext(DbContextOptions<GashubContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Apilog> Apilogs { get; set; }
        public virtual DbSet<FilesProcessed> FilesProcessed { get; set; }
        public virtual DbSet<GasPriceRecord> GasPriceRecords { get; set; }
        public virtual DbSet<GasTickerPrice> GasTickerPrices { get; set; }
        public virtual DbSet<Gashub> Gashubs { get; set; }

        // ✅ Original OnConfiguring with hardcoded connection string
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=./SQLDATA/Gashub.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Apilog>(entity =>
            {
                entity.ToTable("Apilog");
                entity.Property(e => e.Apiname).IsRequired();
                entity.Property(e => e.Hashid).HasColumnType("INT");
            });

            modelBuilder.Entity<FilesProcessed>(entity =>
            {
                entity.ToTable("FilesProcessed");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.FileDate).HasColumnType("DATE");
                entity.Property(e => e.FilePath).IsRequired();
                entity.Property(e => e.ProcessedDateTime).HasColumnType("DATETIME");
            });

            modelBuilder.Entity<GasPriceRecord>(entity =>
            {
                entity.ToTable("GasPriceRecord");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.DailyAverage).HasColumnType("decimal");
                entity.Property(e => e.Price1).HasColumnType("NUMERIC(10,4)");
                entity.Property(e => e.Price10).HasColumnType("NUMERIC(10,4)");
                entity.Property(e => e.Price2).HasColumnType("NUMERIC(10,4)");
                entity.Property(e => e.Price3).HasColumnType("NUMERIC(10,4)");
                entity.Property(e => e.Price4).HasColumnType("NUMERIC(10,4)");
                entity.Property(e => e.Price5).HasColumnType("NUMERIC(10,4)");
                entity.Property(e => e.Price6).HasColumnType("NUMERIC(10,4)");
                entity.Property(e => e.Price7).HasColumnType("NUMERIC(10,4)");
                entity.Property(e => e.Price8).HasColumnType("NUMERIC(10,4)");
                entity.Property(e => e.Price9).HasColumnType("NUMERIC(10,4)");
                entity.Property(e => e.RecordDate).HasColumnType("DATE");
                entity.Property(e => e.Ticker1).HasColumnType("VARCHAR(10)");
                entity.Property(e => e.Ticker10).HasColumnType("VARCHAR(10)");
                entity.Property(e => e.Ticker2).HasColumnType("VARCHAR(10)");
                entity.Property(e => e.Ticker3).HasColumnType("VARCHAR(10)");
                entity.Property(e => e.Ticker4).HasColumnType("VARCHAR(10)");
                entity.Property(e => e.Ticker5).HasColumnType("VARCHAR(10)");
                entity.Property(e => e.Ticker6).HasColumnType("VARCHAR(10)");
                entity.Property(e => e.Ticker7).HasColumnType("VARCHAR(10)");
                entity.Property(e => e.Ticker8).HasColumnType("VARCHAR(10)");
                entity.Property(e => e.Ticker9).HasColumnType("VARCHAR(10)");
                entity.Property(e => e.TickerTotals).HasColumnType("INT");
            });

            modelBuilder.Entity<GasTickerPrice>(entity =>
            {
                entity.HasIndex(e => new { e.GasTicker, e.RecordDate }, "IX_GasTickerPrices_GasTicker_RecordDate").IsUnique();
                entity.Property(e => e.GasTicker).IsRequired();
                entity.Property(e => e.Price).HasColumnType("NUMERIC(10,4)");
                entity.Property(e => e.RecordDate).HasColumnType("DATE");
            });

            modelBuilder.Entity<Gashub>(entity =>
            {
                entity.ToTable("Gashub");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.CompanyEmail).HasColumnType("VARCHAR(100)");
                entity.Property(e => e.CompanyFax).HasColumnType("VARCHAR(20)");
                entity.Property(e => e.CompanyId)
                    .IsRequired()
                    .HasColumnType("VARCHAR(50)")
                    .HasColumnName("CompanyID");
                entity.Property(e => e.CompanyPhone).HasColumnType("VARCHAR(20)");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.Glaccount)
                    .HasColumnType("VARCHAR(50)")
                    .HasColumnName("GLAccount");
                entity.Property(e => e.SubAccount).HasColumnType("VARCHAR(50)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

