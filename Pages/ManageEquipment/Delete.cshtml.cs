using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;  // Required for HttpContext

namespace BrgyLink.Pages.ManageEquipment
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Equipment Equipment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Equipments == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipments.FirstOrDefaultAsync(m => m.Id == id);

            if (equipment == null)
            {
                return NotFound();
            }
            else
            {
                Equipment = equipment;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Equipments == null)
            {
                return NotFound();
            }

            // Find the equipment
            var equipment = await _context.Equipments.FindAsync(id);

            if (equipment == null)
            {
                return NotFound();
            }

            // Assign equipment to the bound property
            Equipment = equipment;

            // Remove the equipment from the database
            _context.Equipments.Remove(Equipment);
            await _context.SaveChangesAsync();

            // Get the current user's username from HttpContext
            var currentUserName = HttpContext.User.Identity?.Name ?? "BrgyLink User";

            // Create a log entry for the deleted equipment
            var equipmentLog = new EquipmentLog
            {
                EquipmentName = Equipment.Name, // Reference to the deleted Equipment name
                Action = $"Deleted by: {currentUserName}", // Action including user and equipment name
                LogDate = DateTime.Now // Current date and time
            };

            // Add the log entry to the database
            _context.EquipmentLogs.Add(equipmentLog);
            await _context.SaveChangesAsync();

            // Redirect to the index page
            return RedirectToPage("./Index");
        }
    }
}
