using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BrgyLink.Pages.ManageBarangayOfficials
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BarangayOfficial BarangayOfficial { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.BarangayOfficials == null)
            {
                return NotFound();
            }

            var barangayofficial =  await _context.BarangayOfficials.FirstOrDefaultAsync(m => m.Id == id);
            if (barangayofficial == null)
            {
                return NotFound();
            }
            BarangayOfficial = barangayofficial;
           ViewData["ResidentID"] = new SelectList(_context.Residents, "ResidentID", "Address");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(BarangayOfficial).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BarangayOfficialExists(BarangayOfficial.Id))
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

        private bool BarangayOfficialExists(int id)
        {
          return (_context.BarangayOfficials?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
