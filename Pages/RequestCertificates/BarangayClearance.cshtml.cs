using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BrgyLink.Models;

namespace BrgyLink.Pages.RequestCertificates
{
    public class RequestFormModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RequestFormModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CertificateRequest CertificateRequest { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Fetch admin settings
            var adminSettings = await _context.AdminSettings.FirstOrDefaultAsync();
            if (adminSettings == null)
            {
                ModelState.AddModelError(string.Empty, "Admin settings not configured.");
                return Page();
            }

            ViewData["CertificateFee"] = adminSettings.CertificateFee;
            ViewData["ContactNumber"] = adminSettings.ContactNumber;

            // Generate tracking code and set PaymentContactNumber
            CertificateRequest = new CertificateRequest
            {
                TrackingCode = GenerateTrackingCode(),
                PaymentContactNumber = adminSettings.ContactNumber,
                Fee = adminSettings.CertificateFee

            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Fetch current admin settings for the contact number
            var adminSettings = await _context.AdminSettings.FirstOrDefaultAsync();
            if (adminSettings == null)
            {
                ModelState.AddModelError(string.Empty, "Admin settings not configured.");
                return Page();
            }

            CertificateRequest.PaymentContactNumber = adminSettings.ContactNumber;
            CertificateRequest.Fee = adminSettings.CertificateFee;
            CertificateRequest.RequestDate = DateTime.UtcNow;

            // Ensure TrackingCode is set
            if (string.IsNullOrEmpty(CertificateRequest.TrackingCode))
            {
                CertificateRequest.TrackingCode = GenerateTrackingCode();
            }

            // Ensure GcashReferenceNo is set if PaymentMethod is GCash
            if (CertificateRequest.PaymentMethod == "GCash" && string.IsNullOrEmpty(CertificateRequest.GcashReferenceNo))
            {
                ModelState.AddModelError("CertificateRequest.GcashReferenceNo", "The GcashReferenceNo field is required for GCash payment.");
                return Page();
            }

            _context.CertificateRequests.Add(CertificateRequest);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }

        private string GenerateTrackingCode()
        {
            return string.Join("-", Guid.NewGuid().ToString().ToUpper().Split('-'));
        }
    }
}
