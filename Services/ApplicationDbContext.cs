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

    // DbSets for entities
    public DbSet<Resident> Residents { get; set; }
    public DbSet<BarangayOfficial> BarangayOfficials { get; set; }
    public DbSet<Committee> Committees { get; set; }
    public DbSet<BarangayOfficialCommittee> BarangayOfficialCommittees { get; set; }
    public DbSet<Purok> Puroks { get; set; } // DbSet for Purok

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure the Resident entity
        modelBuilder.Entity<Resident>(entity =>
        {
            entity.HasKey(e => e.ResidentID);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.PurokId).IsRequired(); // Foreign key

            // Configure foreign key
            entity.Property(e => e.PurokId)
                .IsRequired();

            // Set up navigation property
            entity.HasOne(r => r.Purok)
                .WithMany(p => p.Residents)
                .HasForeignKey(r => r.PurokId);


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

        // Configure the Purok entity
        modelBuilder.Entity<Purok>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Description)
                .HasMaxLength(100);
        });

        // Configure the BarangayOfficial entity
        modelBuilder.Entity<BarangayOfficial>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.MiddleName)
                .HasMaxLength(100);

            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Gender)
                .IsRequired()
                .HasMaxLength(10);

            entity.Property(e => e.MaritalStatus)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.BarangayPosition)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.TermStart)
                .IsRequired()
                .HasColumnType("date");

            entity.Property(e => e.TermEnd)
                .IsRequired()
                .HasColumnType("date");

            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(20);

            // Configure optional photo
            entity.Property(e => e.Photo)
                .HasColumnType("varbinary(max)");

            entity.HasMany(bo => bo.BarangayOfficialCommittees)
                .WithOne(bc => bc.BarangayOfficial)
                .HasForeignKey(bc => bc.BarangayOfficialId);
        });

        // Configure the Committee entity
        modelBuilder.Entity<Committee>(entity =>
        {
            entity.HasKey(c => c.CommitteeId);

            entity.Property(c => c.CommitteeName)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasMany(c => c.BarangayOfficialCommittees)
                .WithOne(bc => bc.Committee)
                .HasForeignKey(bc => bc.CommitteeId);
        });

        // Configure the BarangayOfficialCommittee entity (join table for many-to-many relationship)
        modelBuilder.Entity<BarangayOfficialCommittee>(entity =>
        {
            entity.HasKey(bc => new { bc.BarangayOfficialId, bc.CommitteeId });

            entity.HasOne(bc => bc.BarangayOfficial)
                .WithMany(bo => bo.BarangayOfficialCommittees)
                .HasForeignKey(bc => bc.BarangayOfficialId);

            entity.HasOne(bc => bc.Committee)
                .WithMany(c => c.BarangayOfficialCommittees)
                .HasForeignKey(bc => bc.CommitteeId);
        });
    }
}
