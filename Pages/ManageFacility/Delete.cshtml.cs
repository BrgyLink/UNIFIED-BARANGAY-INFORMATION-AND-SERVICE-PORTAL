using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace BrgyLink.Pages.ManageFacility
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Facility Facility { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            // If id is null or Facilities table is null, return NotFound
            if (id == null || _context.Facilities == null)
            {
                return NotFound();
            }

            // Fetch the facility using the provided id
            var facility = await _context.Facilities.FirstOrDefaultAsync(m => m.Id == id);

            if (facility == null)
            {
                return NotFound();
            }
            else
            {
                // Assign the fetched facility to the Facility property
                Facility = facility;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            // If id is null or Facilities table is null, return NotFound
            if (id == null || _context.Facilities == null)
            {
                return NotFound();
            }

            // Find the facility to delete
            var facility = await _context.Facilities.FindAsync(id);

            if (facility != null)
            {
                // Assign the facility to the Facility property and remove it
                Facility = facility;
                _context.Facilities.Remove(Facility);
                await _context.SaveChangesAsync();

                // Log the deletion action with the current user's name
                var currentUserName = HttpContext.User.Identity.Name; // Get username directly from the claims

                var facilityLog = new FacilityLog
                {
                    FacilityName = Facility.Name, // Log the name of the facility being deleted
                    Action = $"Deleted by: {currentUserName}", // Action description including the user
                    LogDate = DateTime.Now // Current date and time
                };

                // Add the log entry to the FacilityLogs table
                _context.FacilityLogs.Add(facilityLog);
                await _context.SaveChangesAsync(); // Save the log entry to the database
            }

            // Redirect to the Index page after deletion and logging
            return RedirectToPage("./Index");
        }
    }
}
