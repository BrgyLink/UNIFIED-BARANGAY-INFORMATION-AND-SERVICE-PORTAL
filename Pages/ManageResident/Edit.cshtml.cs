using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BrgyLink.Models;
using Microsoft.AspNetCore.Authorization;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace BrgyLink.Pages.ManageResident
{
    [Authorize(Policy = "RequireAdminRole")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EditModel> _logger;

        public EditModel(ApplicationDbContext context, ILogger<EditModel> logger)
        {
            _context = context;
            _logger = logger;
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
                return Page();  // Return the page if the model state is not valid
            }

            try
            {
                // Handle the uploaded image
                if (Resident.ImageFile != null && Resident.ImageFile.Length > 0)
                {
                    // Validate file type (only .jpg, .jpeg, and .png)
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
                        // Copy the uploaded file to a memory stream
                        await Resident.ImageFile.CopyToAsync(memoryStream);

                        // Reset stream position to start
                        memoryStream.Position = 0;

                        // Resize the image if necessary
                        using (var image = await Image.LoadAsync(memoryStream))
                        {
                            int maxWidth = 800;
                            int maxHeight = 800;

                            // Resize the image if it exceeds the max dimensions
                            if (image.Width > maxWidth || image.Height > maxHeight)
                            {
                                image.Mutate(x => x.Resize(new ResizeOptions
                                {
                                    Size = new SixLabors.ImageSharp.Size(maxWidth, maxHeight),
                                    Mode = ResizeMode.Max
                                }));
                            }

                            // Save resized image to memory stream as JPEG
                            using (var outputStream = new MemoryStream())
                            {
                                await image.SaveAsJpegAsync(outputStream);

                                // Save the byte array to the ImageData field in the Resident model
                                Resident.ImageData = outputStream.ToArray();
                            }
                        }
                    }
                }
                else
                {
                    // If no new image is uploaded, use the existing photo if available
                    if (!string.IsNullOrEmpty(Request.Form["ExistingPhoto"]))
                    {
                        // Convert the base64 string back to byte array
                        var base64Image = Request.Form["ExistingPhoto"];
                        Resident.ImageData = Convert.FromBase64String(base64Image);
                    }
                }

                // Attach the modified Resident to the context and save changes
                _context.Attach(Resident).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                // Set success message and reload the page
                TempData["SuccessMessage"] = "Resident updated successfully!";
                return Page();
            }
            catch (Exception ex)
            {
                // Log the error and show a message
                _logger.LogError(ex, "Error updating Resident.");
                ModelState.AddModelError(string.Empty, "An error occurred while saving the Resident. Please try again.");
                return Page();
            }
        }


        private bool ResidentExists(int id)
        {
            return (_context.Residents?.Any(e => e.ResidentID == id)).GetValueOrDefault();
        }
    }
}
