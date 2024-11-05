using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BrgyLink.Models;

namespace BrgyLink.Pages.ManageResident
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CreateModel> _logger;
        public bool Success { get; set; }

        public CreateModel(ApplicationDbContext context, ILogger<CreateModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Resident Resident { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Check if an image file is uploaded
            if (Resident.ImageFile != null && Resident.ImageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await Resident.ImageFile.CopyToAsync(memoryStream);
                    Resident.ImageData = memoryStream.ToArray(); // Set ImageData only if an image is uploaded
                }
            }
            else
            {
                // If no image is uploaded, ImageData remains null (no need to assign null explicitly)
                // Resident.ImageData = null; // This line can be removed
            }

            // Set other required fields to default values if necessary
            Resident.DateRegistered = DateTime.Now;

            // Add the resident to the context and save changes
            _context.Residents.Add(Resident);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}