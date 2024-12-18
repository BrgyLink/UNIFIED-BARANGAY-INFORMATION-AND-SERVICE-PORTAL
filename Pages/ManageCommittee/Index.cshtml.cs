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
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Committee> Committee { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Committees != null)
            {
                Committee = await _context.Committees
                    .Include(c => c.BarangayOfficialCommittees)
                    .ThenInclude(boc => boc.BarangayOfficial)
                    .ToListAsync();
            }
        }
    }
}
