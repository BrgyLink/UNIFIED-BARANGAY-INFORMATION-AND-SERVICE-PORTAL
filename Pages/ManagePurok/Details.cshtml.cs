using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BrgyLink.Models;

namespace BrgyLink.Pages.ManagePurok
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

      public Purok Purok { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Puroks == null)
            {
                return NotFound();
            }

            var purok = await _context.Puroks.FirstOrDefaultAsync(m => m.Id == id);
            if (purok == null)
            {
                return NotFound();
            }
            else 
            {
                Purok = purok;
            }
            return Page();
        }
    }
}
