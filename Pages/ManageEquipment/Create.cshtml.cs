using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BrgyLink.Models;
using Microsoft.AspNetCore.Http;  // Required for HttpContext

namespace BrgyLink.Pages.ManageEquipment
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        // Constructor with dependency injection of ApplicationDbContext
        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Equipment Equipment { get; set; }

        public IActionResult OnGet()
        {
            // Populate the Facility dropdown with a list of available facilities
            ViewData["FacilityId"] = new SelectList(_context.Facilities, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // If model state is invalid, re-populate facilities and return to the form
                ViewData["FacilityId"] = new SelectList(_context.Facilities, "Id", "Name");
                return Page();
            }

            // Add the new Equipment to the database
            _context.Equipments.Add(Equipment);
            await _context.SaveChangesAsync();

            // Get the current user's username from HttpContext
            var currentUserName = HttpContext.User.Identity.Name; // Get username directly from the claims

            // Create a log entry for the new equipment with the user's name
            var equipmentLog = new EquipmentLog
            {
                EquipmentName = Equipment.Name, // Reference to the newly created Equipment
                Action = $"Added by: {currentUserName}", // Action including user and equipment name
                LogDate = DateTime.Now // Current date and time
            };

            // Add the log entry to the database
            _context.EquipmentLogs.Add(equipmentLog);
            await _context.SaveChangesAsync();

            // Set a success message and redirect to the Equipment list page
            TempData["SuccessMessage"] = "Equipment created successfully!";
            return RedirectToPage("./Index");
        }
    }
}
