using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BrgyLink.Pages.ManageBarangayOfficials
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ResidentID"] = new SelectList(_context.Residents, "ResidentID", "Address");
            return Page();
        }

        [BindProperty]
        public BarangayOfficial BarangayOfficial { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.BarangayOfficials == null || BarangayOfficial == null)
            {
                return Page();
            }

            _context.BarangayOfficials.Add(BarangayOfficial);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
