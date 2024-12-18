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
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<BarangayOfficialCommittee> BarangayOfficialCommittee { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.BarangayOfficialCommittees != null)
            {
                BarangayOfficialCommittee = await _context.BarangayOfficialCommittees
                .Include(b => b.BarangayOfficial)
                .Include(b => b.Committee).ToListAsync();
            }
        }
    }
}
