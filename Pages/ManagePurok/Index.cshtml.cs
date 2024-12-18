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
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Purok> Purok { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Puroks != null)
            {
                Purok = await _context.Puroks
                    .Select(p => new Purok
                    { 
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        NumberOfRegisteredPeople = _context.Residents.Count(r => r.PurokId == p.Id)
                    }).ToListAsync();

            }
        }
    }
}
