using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BrgyLink.Models;
using Microsoft.AspNetCore.Authorization;

namespace BrgyLink.Pages.ManageResident
{
    [Authorize(Policy = "RequireAdminRole")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Resident Resident { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Residents == null)
            {
                return NotFound();
            }

            var resident = await _context.Residents.FirstOrDefaultAsync(m => m.ResidentID == id);
            if (resident == null)
            {
                return NotFound();
            }
            Resident = resident;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Handle image file upload if provided
            if (Resident.ImageFile != null && Resident.ImageFile.Length > 0)
            {
                // Validate file type (only jpg, jpeg, and png)
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(Resident.ImageFile.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("Resident.ImageFile", "Only .jpg, .jpeg, and .png files are allowed.");
                    return Page();
                }

                // Validate file size (max 5MB)
                if (Resident.ImageFile.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("Resident.ImageFile", "File size cannot exceed 5MB.");
                    return Page();
                }

                using (var memoryStream = new MemoryStream())
                {
                    await Resident.ImageFile.CopyToAsync(memoryStream);
                    Resident.ImageData = memoryStream.ToArray(); // Save image as byte array
                }
            }

            _context.Attach(Resident).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResidentExists(Resident.ResidentID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ResidentExists(int id)
        {
            return (_context.Residents?.Any(e => e.ResidentID == id)).GetValueOrDefault();
        }
    }
}
