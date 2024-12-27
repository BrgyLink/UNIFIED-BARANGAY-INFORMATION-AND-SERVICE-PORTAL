using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BrgyLink.Models;

namespace BrgyLink.Pages.ManageBlotterReports
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
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

            var blotter =  await _context.Blotter.FirstOrDefaultAsync(m => m.Id == id);
            if (blotter == null)
            {
                return NotFound();
            }
            Blotter = blotter;
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

            _context.Attach(Blotter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlotterExists(Blotter.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Blotter.LastUpdated = System.DateTime.Now;

            return RedirectToPage("./Index");
        }

        private bool BlotterExists(int id)
        {
          return (_context.Blotter?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
