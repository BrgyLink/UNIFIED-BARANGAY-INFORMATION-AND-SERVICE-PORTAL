using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BrgyLink.Pages.ManageEquipmentLog
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

      public EquipmentLog EquipmentLog { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.EquipmentLogs == null)
            {
                return NotFound();
            }

            var equipmentlog = await _context.EquipmentLogs.FirstOrDefaultAsync(m => m.Id == id);
            if (equipmentlog == null)
            {
                return NotFound();
            }
            else 
            {
                EquipmentLog = equipmentlog;
            }
            return Page();
        }
    }
}
