using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BrgyLink.Pages.ManageFacility
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

      public Facility Facility { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Facilities == null)
            {
                return NotFound();
            }

            var facility = await _context.Facilities.FirstOrDefaultAsync(m => m.Id == id);
            if (facility == null)
            {
                return NotFound();
            }
            else 
            {
                Facility = facility;
            }
            return Page();
        }
    }
}
