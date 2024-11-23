using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BrgyLink.Pages
{
    public class AboutUsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AboutUsModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<BarangayOfficial> BarangayOfficials { get; set; }

        public async Task OnGetAsync()
        {
            BarangayOfficials = await _context.BarangayOfficials.ToListAsync();
        }
    }
}
