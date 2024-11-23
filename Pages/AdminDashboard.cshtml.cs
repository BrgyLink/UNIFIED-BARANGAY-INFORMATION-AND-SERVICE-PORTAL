using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BrgyLink.Pages
{
    [Authorize(Policy = "RequireAdminRole")]
    public class AdminDashboardModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        // Only one constructor accepting ApplicationDbContext as a dependency
        public AdminDashboardModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // Property to hold the total number of residents
        public int TotalResidents { get; set; }

        // OnGetAsync method to fetch data
        public async Task OnGetAsync()
        {
            // Retrieve the total count of residents from the database
            TotalResidents = await _context.Residents.CountAsync(); // Fetch the count of residents
        }
    }
}
