using BrgyLink.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;
using BrgyLink.Helpers;

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
        [BindProperty(SupportsGet = true)]
        public string Category { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; }

        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        public async Task OnGetAsync(string searchString, string category, string sortOrder, int? pageIndex)
        {
            // Default to page 1 if no page index is provided
            PageIndex = pageIndex ?? 1;

            var query = _context.BarangayOfficials.AsQueryable();

            // Apply search filter if provided
            if (!string.IsNullOrEmpty(SearchString))
            {
                query = query.Where(o => o.FirstName.Contains(SearchString) || o.LastName.Contains(SearchString));
            }

            // Apply category filter if provided
            if (!string.IsNullOrEmpty(Category))
            {
                switch (Category)
                {
                    case "Gender":
                        query = query.Where(o => o.Gender.Contains(SearchString));
                        break;
                    case "Position":
                        query = query.Where(o => o.BarangayPosition.Contains(SearchString));
                        break;
                        // Add more cases as needed
                }
            }

            // Apply sorting if provided
            switch (SortOrder)
            {
                case "NameAsc":
                    query = query.OrderBy(o => o.FirstName).ThenBy(o => o.LastName);
                    break;
                case "NameDesc":
                    query = query.OrderByDescending(o => o.FirstName).ThenByDescending(o => o.LastName);
                    break;
                case "AgeAsc":
                    query = query.OrderBy(o => o.Birthdate);
                    break;
                case "AgeDesc":
                    query = query.OrderByDescending(o => o.Birthdate);
                    break;
                default:
                    query = query.OrderBy(o => o.FirstName).ThenBy(o => o.LastName);
                    break;
            }

            // Set page size to 10
            int pageSize = 10;
            BarangayOfficial = await PaginatedList<BarangayOfficial>.CreateAsync(query.AsNoTracking(), PageIndex, pageSize, SearchString, Category, null, SortOrder);

            // Calculate total pages
            TotalPages = BarangayOfficial.TotalPages;
        }
    }
}
