using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BrgyLink.Pages
{
    [Authorize(Policy = "RequireAdminRole")]
    public class AdminDashboardModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        // Only one constructor accepting ApplicationDbContext as a dependency
        public AdminDashboardModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<AdminLogs> AdminLogs { get; set; } = default!;
        public IList<BarangayOfficial> Officials { get; set; } = default!;

        // Property to hold the total number of residents
        public int TotalResidents { get; set; }
        public int TotalOfficials { get; set; }
        public int TotalEquipments { get; set; }
        // Property for calculating progress percentages
        public double ResidentsProgress => TotalResidents; // No max value, just the total
        public double OfficialsProgress => TotalOfficials;
        public double EquipmentsProgress => TotalEquipments;

        // OnGetAsync method to fetch data
        public async Task OnGetAsync()
        {
            TotalOfficials = await _context.BarangayOfficials.CountAsync();
            // Retrieve the total count of residents from the database
            TotalResidents = await _context.Residents.CountAsync(); // Fetch the count of residents
            TotalEquipments = await _context.Equipments.CountAsync();


            if (_context.AdminLogs != null)
            {
                AdminLogs = await _context.AdminLogs.ToListAsync();
                // Fetch only the first 5 rows
                AdminLogs = await _context.AdminLogs
                    .OrderByDescending(log => log.Date) // Optional: Order by Date or other criteria
                    .Take(5)
                    .ToListAsync();

                Console.WriteLine($"AdminLogs count: {AdminLogs.Count}"); // Log the count
            }

            if (_context.BarangayOfficials != null)
            {
                Officials = await _context.BarangayOfficials.ToListAsync();
                Console.WriteLine($"Officials count: {Officials.Count}");
            }
        }
    }
}
