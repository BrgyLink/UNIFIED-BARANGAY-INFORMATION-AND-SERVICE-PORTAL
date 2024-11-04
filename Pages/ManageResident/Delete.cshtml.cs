using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BrgyLink.Models;

namespace BrgyLink.Pages.ManageResident
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Resident Resident { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Residents == null)
            {
                return NotFound();
            }

            var resident = await _context.Residents.FirstOrDefaultAsync(m => m.ResidentID == id);

            if (resident == null)
            {
                return NotFound();
            }
            else 
            {
                Resident = resident;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Residents == null)
            {
                return NotFound();
            }
            var resident = await _context.Residents.FindAsync(id);

            if (resident != null)
            {
                Resident = resident;
                _context.Residents.Remove(Resident);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
