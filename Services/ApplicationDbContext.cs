using BrgyLink.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Resident> Residents { get; set; }
    public DbSet<BarangayOfficial> BarangayOfficials { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Resident>(entity =>
        {
            // Configure primary key
            entity.HasKey(e => e.ResidentID);

            // Configure image data to use efficient storage
            entity.Property(e => e.ImageData)
                .HasColumnType("varbinary(max)")
                .IsRequired(false);

            // Configure required string properties with max length
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(200);

            // Configure optional string properties
            entity.Property(e => e.MiddleName)
                .HasMaxLength(50)
                .IsRequired(false);

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsRequired(false);

            // Configure default values
            entity.Property(e => e.DateRegistered)
                .HasDefaultValueSql("GETDATE()");

            entity.Property(e => e.ResidencyStatus)
                .HasDefaultValue("Resident");

            entity.Property(e => e.Nationality)
                .HasDefaultValue("Filipino");

            entity.Property(e => e.CivilStatus)
                .HasDefaultValue("Single");

            entity.Property(e => e.VoterStatus)
                .HasDefaultValue("Non-voter");
        });
    }
}