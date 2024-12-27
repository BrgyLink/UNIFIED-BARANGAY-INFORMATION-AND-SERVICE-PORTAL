using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BrgyLink.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BrgyLink.Pages.ManageBarangayOfficialCommittee
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // Bind the BarangayOfficialCommittee model to the form inputs
        [BindProperty]
        public BarangayOfficialCommittee BarangayOfficialCommittee { get; set; } = default!;

        public IActionResult OnGet()
        {

            // Populate dropdowns
            PopulateDropdowns();
            return Page();


        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Repopulate dropdowns if the model state is invalid
                PopulateDropdowns();
                return Page();
            }

            // Check if the Barangay Official is already assigned to the selected committee
            var exists = await _context.BarangayOfficialCommittees
                .AnyAsync(b => b.BarangayOfficialId == BarangayOfficialCommittee.BarangayOfficialId
                               && b.CommitteeId == BarangayOfficialCommittee.CommitteeId);

            if (exists)
            {
                // Add a warning message if the entry already exists
                ModelState.AddModelError(string.Empty, "This Barangay Official is already assigned to the selected committee.");
                PopulateDropdowns();
                return Page();
            }

            try
            {
                // Get the current user's username from HttpContext
                var currentUserName = HttpContext.User.Identity?.Name ?? "Unknown User";

                // Retrieve Barangay Official and Committee names for the log
                var officialName = await _context.BarangayOfficials
                    .Where(o => o.Id == BarangayOfficialCommittee.BarangayOfficialId)
                    .Select(o => o.FullName)
                    .FirstOrDefaultAsync();

                var committeeName = await _context.Committees
                    .Where(c => c.CommitteeId == BarangayOfficialCommittee.CommitteeId)
                    .Select(c => c.CommitteeName)
                    .FirstOrDefaultAsync();

                // Create admin log entry
                var adminLog = new AdminLogs
                {
                    Firstname = currentUserName,
                    Actions = "Added to Committee",
                    Description = $"Added {officialName} to the {committeeName}",
                    Role = "Official",
                    Date = DateTime.Now
                };

                // Add the log entry and save the BarangayOfficialCommittee
                _context.AdminLogs.Add(adminLog);
                _context.BarangayOfficialCommittees.Add(BarangayOfficialCommittee);
                await _context.SaveChangesAsync();

                // Redirect to index after successful save
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                // Log the error and show a user-friendly message
                Console.WriteLine($"Error: {ex.Message}");
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");
                PopulateDropdowns();
                return Page();
            }
        }

        private void PopulateDropdowns()
        {
            // Populate Barangay Official dropdown
            ViewData["BarangayOfficialId"] = new SelectList(
                _context.BarangayOfficials,
                "Id",
                "FullName"
            );

            // Populate Committee dropdown
            ViewData["CommitteeId"] = new SelectList(
                _context.Committees,
                "CommitteeId",
                "CommitteeName"
            );
        }
    }
}
