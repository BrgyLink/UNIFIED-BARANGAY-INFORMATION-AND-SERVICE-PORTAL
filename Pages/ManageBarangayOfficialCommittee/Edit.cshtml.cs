using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BrgyLink.Models;

namespace BrgyLink.Pages.ManageBarangayOfficialCommittee
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BarangayOfficialCommittee BarangayOfficialCommittee { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.BarangayOfficialCommittees == null)
            {
                return NotFound();
            }

            var barangayofficialcommittee =  await _context.BarangayOfficialCommittees.FirstOrDefaultAsync(m => m.BarangayOfficialId == id);
            if (barangayofficialcommittee == null)
            {
                return NotFound();
            }
            BarangayOfficialCommittee = barangayofficialcommittee;
           ViewData["BarangayOfficialId"] = new SelectList(_context.BarangayOfficials, "Id", "BarangayPosition");
           ViewData["CommitteeId"] = new SelectList(_context.Committees, "CommitteeId", "CommitteeName");
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

            _context.Attach(BarangayOfficialCommittee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BarangayOfficialCommitteeExists(BarangayOfficialCommittee.BarangayOfficialId))
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

        private bool BarangayOfficialCommitteeExists(int id)
        {
          return (_context.BarangayOfficialCommittees?.Any(e => e.BarangayOfficialId == id)).GetValueOrDefault();
        }
    }
}
