using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using BrgyLink.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace BrgyLink.Pages.ManageBarangayOfficials
{
    [Authorize(Policy = "RequireAdminRole")]  // Apply admin role authorization
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CreateModel> _logger;
        private readonly IWebHostEnvironment _environment;

        [BindProperty]
        public BarangayOfficial BarangayOfficial { get; set; } = default!;

        public CreateModel(
            ApplicationDbContext context,
            ILogger<CreateModel> logger,
            IWebHostEnvironment environment)
        {
            _context = context;
            _logger = logger;
            _environment = environment;
        }

        // On GET, return the page
        public IActionResult OnGet()
        {
            return Page();
        }

        // On POST, handle the form submission
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();  // Return the page if the model state is not valid
            }

            try
            {
                // Handle image upload
                if (BarangayOfficial.ImageFile != null && BarangayOfficial.ImageFile.Length > 0)
                {
                    // Validate file type (only jpg, jpeg, and png)
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                    var extension = Path.GetExtension(BarangayOfficial.ImageFile.FileName).ToLowerInvariant();

                    if (!allowedExtensions.Contains(extension))
                    {
                        ModelState.AddModelError("BarangayOfficial.ImageFile", "Only .jpg, .jpeg, and .png files are allowed.");
                        return Page();
                    }

                    // Validate file size (max 5MB)
                    if (BarangayOfficial.ImageFile.Length > 5 * 1024 * 1024)
                    {
                        ModelState.AddModelError("BarangayOfficial.ImageFile", "File size cannot exceed 5MB.");
                        return Page();
                    }

                    using (var memoryStream = new MemoryStream())
                    {
                        await BarangayOfficial.ImageFile.CopyToAsync(memoryStream);

                        // Reset stream position to start
                        memoryStream.Position = 0;

                        // Resize image if necessary
                        using (var image = await Image.LoadAsync(memoryStream))
                        {
                            // Define maximum dimensions for the image
                            int maxWidth = 800;
                            int maxHeight = 800;

                            // Resize the image if it exceeds the max dimensions
                            if (image.Width > maxWidth || image.Height > maxHeight)
                            {
                                image.Mutate(x => x.Resize(new ResizeOptions
                                {
                                    Size = new Size(maxWidth, maxHeight),
                                    Mode = ResizeMode.Max
                                }));
                            }

                            // Save resized image to memory stream as JPEG
                            using (var outputStream = new MemoryStream())
                            {
                                await image.SaveAsJpegAsync(outputStream);
                                BarangayOfficial.Photo = outputStream.ToArray();
                            }
                        }
                    }
                }

                // Add the BarangayOfficial to the database
                _context.BarangayOfficials.Add(BarangayOfficial);
                await _context.SaveChangesAsync();

                // Set success message and redirect
                TempData["SuccessMessage"] = "Barangay Official created successfully!";
                // Redirect to the Index page after successful creation
                return RedirectToPage("/ManageBarangayOfficials/Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Barangay Official.");
                ModelState.AddModelError(string.Empty, "An error occurred while saving the Barangay Official. Please try again.");
                return Page();
            }
        }

    }
}
