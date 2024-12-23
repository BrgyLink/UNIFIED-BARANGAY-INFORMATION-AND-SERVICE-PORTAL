using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BrgyLink.Pages.ManageFacility
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Facility Facility { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Facilities == null)
            {
                return NotFound();
            }

            // Fetch the facility by id
            var facility = await _context.Facilities.FirstOrDefaultAsync(m => m.Id == id);

            if (facility == null)
            {
                return NotFound();
            }

            // Assign fetched facility to the Facility property for the view
            Facility = facility;
            return Page();
        }

        // Method for handling the POST request when the form is submitted
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Return to the page with validation errors if the model state is invalid
                return Page();
            }

            // Attach the modified facility and set the state to Modified
            _context.Attach(Facility).State = EntityState.Modified;

            try
            {
                // Save the changes to the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // If the facility doesn't exist anymore, return NotFound
                if (!FacilityExists(Facility.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw; // Re-throw the exception if concurrency issues happen
                }
            }

            // Get the current user's username (for logging)
            var currentUserName = HttpContext.User.Identity.Name;

            // Create a log entry for the update action
            var facilityLog = new FacilityLog
            {
                FacilityName = Facility.Name, // Log the facility name being updated
                Action = $"Updated by: {currentUserName}", // Log the user who updated the facility
                LogDate = DateTime.Now // Current timestamp for the update
            };

            // Add the log entry to the database
            _context.FacilityLogs.Add(facilityLog);
            await _context.SaveChangesAsync(); // Save the log entry to the database

            // Redirect to the Index page after the update
            return RedirectToPage("./Index");
        }

        // Helper method to check if a facility exists by its ID
        private bool FacilityExists(int id)
        {
            return (_context.Facilities?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
