using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrgyLink.Models;
using Microsoft.EntityFrameworkCore;

namespace BrgyLink.Pages.RequestCertificates
{
    public class TrackRequestModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public TrackRequestModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string TrackingCode { get; set; }

        public CertificateRequest RequestDetails { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
           if(!string.IsNullOrEmpty(TrackingCode))
            {
                RequestDetails = await _context.CertificateRequests
                    .FirstOrDefaultAsync(r => r.TrackingCode == TrackingCode);

                if (RequestDetails == null)
                {
                    ModelState.AddModelError(string.Empty, "Request not found.");
                }
            }
            return Page();

        }
    }
}
