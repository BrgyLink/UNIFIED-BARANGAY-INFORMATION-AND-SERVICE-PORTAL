using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BrgyLink.Pages.ManageEquipmentLog
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<EquipmentLog> EquipmentLog { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.EquipmentLogs != null)
            {
                EquipmentLog = await _context.EquipmentLogs.ToListAsync();
            }
        }
    }
}
