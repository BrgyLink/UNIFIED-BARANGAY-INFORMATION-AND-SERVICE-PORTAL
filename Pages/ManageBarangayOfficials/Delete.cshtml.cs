using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BrgyLink.Pages.ManageBarangayOfficials
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
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

            var barangayofficial = await _context.BarangayOfficials.FirstOrDefaultAsync(m => m.Id == id);

            if (barangayofficial == null)
            {
                return NotFound();
            }
            else 
            {
                BarangayOfficial = barangayofficial;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.BarangayOfficials == null)
            {
                return NotFound();
            }
            var barangayofficial = await _context.BarangayOfficials.FindAsync(id);

            if (barangayofficial != null)
            {
                BarangayOfficial = barangayofficial;
                _context.BarangayOfficials.Remove(BarangayOfficial);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
