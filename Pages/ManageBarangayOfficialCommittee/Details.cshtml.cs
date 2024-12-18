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
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
