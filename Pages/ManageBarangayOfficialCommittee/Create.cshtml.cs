using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BrgyLink.Models;

namespace BrgyLink.Pages.ManageBarangayOfficialCommittee
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // This property is used to bind form data to the model
        [BindProperty]
        public BarangayOfficialCommittee BarangayOfficialCommittee { get; set; } = default!;

        public IActionResult OnGet()
        {
            // For BarangayOfficials, use FullName for display and Id for value
            ViewData["BarangayOfficialId"] = new SelectList(
                _context.BarangayOfficials,
                "Id",           // This is the value (matches your model)
                "FullName"      // This is what will be displayed in the dropdown
            );

            // For Committees, use CommitteeName for display and CommitteeId for value
            ViewData["CommitteeId"] = new SelectList(
                _context.Committees,
                "CommitteeId",  // This is the value (matches your model)
                "CommitteeName" // This is what will be displayed in the dropdown
            );

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            // Log detailed model state errors
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine($"Model State Error: {error.ErrorMessage}");
                    }
                }

                // Repopulate the dropdowns
                ViewData["BarangayOfficialId"] = new SelectList(
                    _context.BarangayOfficials,
                    "Id",
                    "FullName"
                );
                ViewData["CommitteeId"] = new SelectList(
                    _context.Committees,
                    "CommitteeId",
                    "CommitteeName"
                );

                return Page();
            }

            // Proceed with saving
            _context.BarangayOfficialCommittees.Add(BarangayOfficialCommittee);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }


    }
}
