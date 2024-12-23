using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BrgyLink.Pages.ManageEquipment
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
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
            Equipment = equipment;
            ViewData["FacilityId"] = new SelectList(_context.Facilities, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var originalEquipment = await _context.Equipments.AsNoTracking().FirstOrDefaultAsync(e => e.Id == Equipment.Id);
            if (originalEquipment == null)
            {
                return NotFound();
            }

            _context.Attach(Equipment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                // Get the current user's username
                var currentUserName = HttpContext.User.Identity.Name;

                // Log the changes
                var equipmentLog = new EquipmentLog
                {
                    EquipmentName = Equipment.Name, // Use the updated name
                    Action = $"Edited by: {currentUserName}", // Action describing the user and action
                    LogDate = DateTime.Now
                };

                // Add the log entry to the database
                _context.EquipmentLogs.Add(equipmentLog);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipmentExists(Equipment.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EquipmentExists(int id)
        {
            return (_context.Equipments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
