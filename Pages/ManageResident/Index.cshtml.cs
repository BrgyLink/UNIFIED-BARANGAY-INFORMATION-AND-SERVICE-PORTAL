using BrgyLink.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using BrgyLink.Helpers;
using System.Collections.Generic;

namespace BrgyLink.Pages.ManageResident
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public PaginatedList<Resident> Resident { get; set; }
        public List<Purok> Puroks { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Filter { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; }

        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        public async Task OnGetAsync(int? pageIndex)
        {
            // Default to page 1 if no page index is provided
            PageIndex = pageIndex ?? 1;

            var query = _context.Residents.AsQueryable();

            // Load Puroks for the dropdown
            Puroks = await _context.Puroks.ToListAsync();

            // Apply search filter if provided
            if (!string.IsNullOrEmpty(SearchString))
            {
                query = query.Where(r => r.FirstName.Contains(SearchString) || r.LastName.Contains(SearchString));
            }

            // Apply combined filter if provided
            if (!string.IsNullOrEmpty(Filter))
            {
                switch (Filter)
                {
                    case "Purok":
                        if (int.TryParse(SearchString, out int purokId))
                        {
                            query = query.Where(r => r.PurokId == purokId);
                        }
                        break;
                    case "Gender":
                        query = query.Where(r => r.Gender.Contains(SearchString));
                        break;
                    case "AgeGroup":
                        // Implement age group filtering logic here
                        // Example: AgeGroup filter could be "Child", "Adult", "Senior"
                        if (SearchString == "Child")
                        {
                            query = query.Where(r => EF.Functions.DateDiffYear(r.BirthDate, DateTime.Now) < 18);
                        }
                        else if (SearchString == "Adult")
                        {
                            query = query.Where(r => EF.Functions.DateDiffYear(r.BirthDate, DateTime.Now) >= 18 && EF.Functions.DateDiffYear(r.BirthDate, DateTime.Now) < 60);
                        }
                        else if (SearchString == "Senior")
                        {
                            query = query.Where(r => EF.Functions.DateDiffYear(r.BirthDate, DateTime.Now) >= 60);
                        }
                        break;
                }
            }

            // Apply sorting if provided
            switch (SortOrder)
            {
                case "NameAsc":
                    query = query.OrderBy(r => r.FirstName).ThenBy(r => r.LastName);
                    break;
                case "NameDesc":
                    query = query.OrderByDescending(r => r.FirstName).ThenByDescending(r => r.LastName);
                    break;
                case "AgeAsc":
                    query = query.OrderBy(r => EF.Functions.DateDiffYear(r.BirthDate, DateTime.Now));
                    break;
                case "AgeDesc":
                    query = query.OrderByDescending(r => EF.Functions.DateDiffYear(r.BirthDate, DateTime.Now));
                    break;
                default:
                    query = query.OrderBy(r => r.FirstName).ThenBy(r => r.LastName);
                    break;
            }

            // Set page size to 10
            int pageSize = 10;
            Resident = await PaginatedList<Resident>.CreateAsync(query.AsNoTracking(), PageIndex, pageSize, SearchString, Filter, SortOrder, null);

            // Calculate total pages
            TotalPages = Resident.TotalPages;
        }
    }
}