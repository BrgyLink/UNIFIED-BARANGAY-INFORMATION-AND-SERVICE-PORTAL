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
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

      public Committee Committee { get; set; } = default!;
        public List<BarangayOfficial> Members { get; set; } = new List<BarangayOfficial>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Committees == null)
            {
                return NotFound();
            }

            var committee = await _context.Committees
                 .Include(c => c.BarangayOfficialCommittees)
                 .ThenInclude(boc => boc.BarangayOfficial)
                 .FirstOrDefaultAsync(m => m.CommitteeId == id);

            if (committee == null)
            {
                return NotFound();
            }

            Committee = committee;

            // Extract members for display
            Members = committee.BarangayOfficialCommittees
                .Select(boc => boc.BarangayOfficial)
                .Where(official => official != null)
                .ToList();

            return Page();
        }
    }
}
