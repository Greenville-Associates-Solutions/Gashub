using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Gas.Models;

public partial class GashubContext : DbContext
{
    public GashubContext()
    {
    }

    public GashubContext(DbContextOptions<GashubContext> options)
        : base(options)
    {
    }
    public virtual DbSet<GasTickerPrice> GasTickerPrices { get; set; }

    public virtual DbSet<Apilog> Apilogs { get; set; }

    public virtual DbSet<GasPriceRecord> GasPriceRecords { get; set; }

    public virtual DbSet<Gashub> Gashubs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=./SQLDATA/Gashub.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Apilog>(entity =>
        {
            entity.ToTable("Apilog");

            entity.Property(e => e.Apiname).IsRequired();
            entity.Property(e => e.Hashid).HasColumnType("INT");
        });

        modelBuilder.Entity<GasPriceRecord>(entity =>
        {
            entity.ToTable("GasPriceRecord");

            entity.Property(e => e.Id).HasColumnName("ID");
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
            entity.Property(e => e.DailyAverage);
            entity.Property(e => e.TickerTotals);
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
            entity.Property(e => e.GasTicker).HasColumnType("TEXT");
        });

       
modelBuilder.Entity<GasTickerPrice>(entity =>
{
    entity.HasKey(e => e.Id);

    entity.HasIndex(e => new { e.GasTicker, e.RecordDate })
          .IsUnique();   // enforce one record per ticker per day

    entity.Property(e => e.RecordDate)
          .HasColumnType("DATE");

    entity.Property(e => e.Price)
          .HasColumnType("NUMERIC(10,4)")
          .IsRequired();
});

    OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
