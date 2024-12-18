using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BrgyLink.Models;

namespace BrgyLink.Pages.ManageBarangayOfficialCommittee
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
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

            var barangayofficialcommittee = await _context.BarangayOfficialCommittees.FirstOrDefaultAsync(m => m.BarangayOfficialId == id);

            if (barangayofficialcommittee == null)
            {
                return NotFound();
            }
            else 
            {
                BarangayOfficialCommittee = barangayofficialcommittee;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.BarangayOfficialCommittees == null)
            {
                return NotFound();
            }
            var barangayofficialcommittee = await _context.BarangayOfficialCommittees.FindAsync(id);

            if (barangayofficialcommittee != null)
            {
                BarangayOfficialCommittee = barangayofficialcommittee;
                _context.BarangayOfficialCommittees.Remove(BarangayOfficialCommittee);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
