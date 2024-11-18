using BrgyLink.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Resident> Residents { get; set; }

    public DbSet<BrgyLink.Models.Blotter> Blotter { get; set; } = default!;
    public DbSet<BlotterVictim> BlotterVictims { get; set; }  // Add this
    // public DbSet<CertificateRequest> CertificateRequests { get; set; }
}
