using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using BrgyLink.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace BrgyLink.Pages.ManageResident
{
    [Authorize(Policy = "RequireAdminRole")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CreateModel> _logger;
        private readonly IWebHostEnvironment _environment;

        [BindProperty]
        public Resident Resident { get; set; } = default!;

        public CreateModel(
            ApplicationDbContext context,
            ILogger<CreateModel> logger,
            IWebHostEnvironment environment)
        {
            _context = context;
            _logger = logger;
            _environment = environment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Handle image upload
                if (Resident.ImageFile != null && Resident.ImageFile.Length > 0)
                {
                    // Validate file type
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                    var extension = Path.GetExtension(Resident.ImageFile.FileName).ToLowerInvariant();

                    if (!allowedExtensions.Contains(extension))
                    {
                        ModelState.AddModelError("Resident.ImageFile", "Only .jpg, .jpeg and .png files are allowed.");
                        return Page();
                    }

                    // Validate file size (e.g., max 5MB)
                    if (Resident.ImageFile.Length > 5 * 1024 * 1024)
                    {
                        ModelState.AddModelError("Resident.ImageFile", "File size cannot exceed 5MB.");
                        return Page();
                    }

                    using (var memoryStream = new MemoryStream())
                    {
                        await Resident.ImageFile.CopyToAsync(memoryStream);

                        // Reset stream position
                        memoryStream.Position = 0;

                        // Resize image if needed
                        using (var image = await Image.LoadAsync(memoryStream))
                        {
                            // Define maximum dimensions
                            int maxWidth = 800;
                            int maxHeight = 800;

                            // Resize if image is larger than maximum dimensions
                            if (image.Width > maxWidth || image.Height > maxHeight)
                            {
                                image.Mutate(x => x.Resize(new ResizeOptions
                                {
                                    Size = new Size(maxWidth, maxHeight),
                                    Mode = ResizeMode.Max
                                }));
                            }

                            // Save resized image to memory stream
                            using (var outputStream = new MemoryStream())
                            {
                                await image.SaveAsJpegAsync(outputStream);
                                Resident.ImageData = outputStream.ToArray();
                            }
                        }
                    }
                }

                // Set required fields
                Resident.DateRegistered = DateTime.Now;

                // Add to database
                _context.Residents.Add(Resident);
                await _context.SaveChangesAsync();

                // Set success message
                TempData["SuccessMessage"] = "Resident created successfully!";

                //return RedirectToPage("./Index");
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating resident");
                ModelState.AddModelError(string.Empty, "An error occurred while saving the resident. Please try again.");
                return Page();
            }
        }
    }
}