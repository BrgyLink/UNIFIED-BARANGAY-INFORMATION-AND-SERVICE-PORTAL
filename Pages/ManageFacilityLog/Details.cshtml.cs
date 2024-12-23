using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BrgyLink.Pages.ManageFacilityLog
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

      public FacilityLog FacilityLog { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.FacilityLogs == null)
            {
                return NotFound();
            }

            var facilitylog = await _context.FacilityLogs.FirstOrDefaultAsync(m => m.Id == id);
            if (facilitylog == null)
            {
                return NotFound();
            }
            else 
            {
                FacilityLog = facilitylog;
            }
            return Page();
        }
    }
}
