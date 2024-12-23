using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BrgyLink.Pages.ManageEquipment
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

      public Equipment Equipment { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Equipments == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipments.FirstOrDefaultAsync(m => m.Id == id);
            if (equipment == null)
            {
                return NotFound();
            }
            else 
            {
                Equipment = equipment;
            }
            return Page();
        }
    }
}
