using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BrgyLink.Models;

namespace BrgyLink.Pages.ManageBlotterReports
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Blotter> Blotter { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Blotter != null)
            {
                Blotter = await _context.Blotter.ToListAsync();
            }
        }
    }
}
