using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BrgyLink.Models;
using Microsoft.AspNetCore.Identity;

namespace BrgyLink.Services
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
            {       

            }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var admin = new IdentityRole("admin");
            admin.NormalizedName = "admin";

            var client = new IdentityRole("client");
            client.NormalizedName = "client";

            var resident = new IdentityRole("resident");
            resident.NormalizedName = "resident";

            builder.Entity<IdentityRole>().HasData(admin, client, resident);


        }
    }
}
