using BrgyLink.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrgyLink.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace BrgyLink.Pages.ManageBarangayOfficials
{
    [Authorize(Policy = "RequireAdminRole")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public PaginatedList<BarangayOfficial> BarangayOfficial { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        public async Task OnGetAsync(int? pageIndex)
        {
            // Default to page 1 if no page index is provided
            PageIndex = pageIndex ?? 1;

            var query = _context.BarangayOfficials.AsQueryable();

            // Apply search filter if provided
            if (!string.IsNullOrEmpty(SearchString))
            {
                query = query.Where(b => b.FirstName.Contains(SearchString) ||
                                          b.MiddleName.Contains(SearchString) ||
                                          b.LastName.Contains(SearchString) ||
                                          b.BarangayPosition.Contains(SearchString));
            }

            // Set page size to 10
            int pageSize = 10;  // You can adjust this value if you need a different page size
            BarangayOfficial = await PaginatedList<BarangayOfficial>.CreateAsync(query.AsNoTracking(), PageIndex, pageSize);

            // Calculate total pages
            TotalPages = BarangayOfficial.TotalPages;
        }
    }
}
