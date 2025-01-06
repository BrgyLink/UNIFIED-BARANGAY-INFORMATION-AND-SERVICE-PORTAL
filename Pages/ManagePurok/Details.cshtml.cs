using BrgyLink.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrgyLink.Pages.ManagePurok
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Purok Purok { get; set; }
        public IList<Resident> Residents { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Purok = await _context.Puroks
                .Include(p => p.Residents)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Purok != null)
            {
                Purok.NumberOfRegisteredPeople = Purok.Residents.Count;
            }

            Residents = await _context.Residents
                .Where(r => r.PurokId == id)
                .ToListAsync();

            return Page();
        }
    }
}