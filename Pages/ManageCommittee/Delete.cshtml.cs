using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BrgyLink.Models;

namespace BrgyLink.Pages.ManageCommittee
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Committee Committee { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Committees == null)
            {
                return NotFound();
            }

            var committee = await _context.Committees.FirstOrDefaultAsync(m => m.CommitteeId == id);

            if (committee == null)
            {
                return NotFound();
            }
            else 
            {
                Committee = committee;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Committees == null)
            {
                return NotFound();
            }
            var committee = await _context.Committees.FindAsync(id);

            if (committee != null)
            {
                Committee = committee;
                _context.Committees.Remove(Committee);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
