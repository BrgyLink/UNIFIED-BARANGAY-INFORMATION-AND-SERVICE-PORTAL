using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BrgyLink.Pages.ManageFacilityLog
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<FacilityLog> FacilityLog { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.FacilityLogs != null)
            {
                FacilityLog = await _context.FacilityLogs.ToListAsync();
            }
        }
    }
}
