using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BrgyLink.Models;

namespace BrgyLink.Pages.ManageBlotterReports
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Blotter Blotter { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Blotter == null)
            {
                return NotFound();
            }

            var blotter = await _context.Blotter.FirstOrDefaultAsync(m => m.Id == id);

            if (blotter == null)
            {
                return NotFound();
            }
            else 
            {
                Blotter = blotter;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Blotter == null)
            {
                return NotFound();
            }
            var blotter = await _context.Blotter.FindAsync(id);

            if (blotter != null)
            {
                Blotter = blotter;
                _context.Blotter.Remove(Blotter);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
