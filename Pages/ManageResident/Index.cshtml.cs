using BrgyLink.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrgyLink.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BrgyLink.Pages.ManageResident
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Resident> Resident { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync(int? pageIndex)
        {
            var query = _context.Residents.AsQueryable();

            if (!string.IsNullOrEmpty(SearchString))
            {
                query = query.Where(r => r.FirstName.Contains(SearchString) || r.LastName.Contains(SearchString));
            }

            int pageSize = 15;
            Resident = await PaginatedList<Resident>.CreateAsync(query.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}