using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Repository.Models;

public partial class Sp25PharmaceuticalDbContext : DbContext
{
    public Sp25PharmaceuticalDbContext()
    {
    }

    public Sp25PharmaceuticalDbContext(DbContextOptions<Sp25PharmaceuticalDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<MedicineInformation> MedicineInformations { get; set; }

    public virtual DbSet<StoreAccount> StoreAccounts { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { optionsBuilder.UseSqlServer(GetConnectionString()); }

    private string GetConnectionString()
    {
        IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
        return configuration["ConnectionStrings:DefaultConnection"];
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.ManufacturerId).HasName("PK__Manufact__357E5CA117699DA8");

            entity.ToTable("Manufacturer");

            entity.Property(e => e.ManufacturerId)
                .HasMaxLength(20)
                .HasColumnName("ManufacturerID");
            entity.Property(e => e.ContactInformation).HasMaxLength(120);
            entity.Property(e => e.CountryofOrigin).HasMaxLength(250);
            entity.Property(e => e.ManufacturerName).HasMaxLength(100);
            entity.Property(e => e.ShortDescription).HasMaxLength(400);
        });

        modelBuilder.Entity<MedicineInformation>(entity =>
        {
            entity.HasKey(e => e.MedicineId).HasName("PK__Medicine__4F2128F0C6F81E18");

            entity.ToTable("MedicineInformation");

            entity.Property(e => e.MedicineId)
                .HasMaxLength(30)
                .HasColumnName("MedicineID");
            entity.Property(e => e.ActiveIngredients).HasMaxLength(200);
            entity.Property(e => e.DosageForm).HasMaxLength(400);
            entity.Property(e => e.ExpirationDate).HasMaxLength(120);
            entity.Property(e => e.ManufacturerId)
                .HasMaxLength(20)
                .HasColumnName("ManufacturerID");
            entity.Property(e => e.MedicineName).HasMaxLength(160);
            entity.Property(e => e.WarningsAndPrecautions).HasMaxLength(400);

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.MedicineInformations)
                .HasForeignKey(d => d.ManufacturerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__MedicineI__Manuf__3C69FB99");
        });

        modelBuilder.Entity<StoreAccount>(entity =>
        {
            entity.HasKey(e => e.StoreAccountId).HasName("PK__StoreAcc__42D52A6A6A674CA6");

            entity.ToTable("StoreAccount");

            entity.HasIndex(e => e.EmailAddress, "UQ__StoreAcc__49A147406325389B").IsUnique();

            entity.Property(e => e.StoreAccountId)
                .ValueGeneratedNever()
                .HasColumnName("StoreAccountID");
            entity.Property(e => e.EmailAddress).HasMaxLength(90);
            entity.Property(e => e.StoreAccountDescription).HasMaxLength(140);
            entity.Property(e => e.StoreAccountPassword).HasMaxLength(90);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
