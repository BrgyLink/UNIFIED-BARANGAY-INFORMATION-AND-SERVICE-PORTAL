using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using BrgyLink.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;  // Optionally add for specific format handling

namespace BrgyLink.Pages.ManageBarangayOfficials
{
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
        public BarangayOfficial BarangayOfficial { get; set; } = default!;

        public List<Committee> Committees { get; set; } = new();
        [BindProperty]
        public List<int> SelectedCommitteeIds { get; set; } = new();

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.BarangayOfficials == null)
            {
                return NotFound();
            }

            var barangayofficial = await _context.BarangayOfficials.FirstOrDefaultAsync(m => m.Id == id);
            Committees = _context.Committees.ToList();
            if (barangayofficial == null)
            {
                return NotFound();
            }

            BarangayOfficial = barangayofficial;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Committees = _context.Committees.ToList();
                return Page();  // Return the page if the model state is not valid
            }

            try
            {
                // If an image is provided, handle the upload
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    // Validate file type (only .jpg, .jpeg, and .png)
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                    var extension = Path.GetExtension(ImageFile.FileName).ToLowerInvariant();

                    if (!allowedExtensions.Contains(extension))
                    {
                        ModelState.AddModelError("BarangayOfficial.ImageFile", "Only .jpg, .jpeg, and .png files are allowed.");
                        return Page();
                    }

                    // Validate file size (max 5MB)
                    if (ImageFile.Length > 5 * 1024 * 1024)
                    {
                        ModelState.AddModelError("BarangayOfficial.ImageFile", "File size cannot exceed 5MB.");
                        return Page();
                    }

                    using (var memoryStream = new MemoryStream())
                    {
                        // Copy the uploaded file to a memory stream
                        await ImageFile.CopyToAsync(memoryStream);

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

                                // Save the byte array to the Photo field in the model
                                BarangayOfficial.Photo = outputStream.ToArray();
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
                        BarangayOfficial.Photo = Convert.FromBase64String(base64Image);
                    }
                }

                // Attach the modified BarangayOfficial to the context and save changes
                _context.Attach(BarangayOfficial).State = EntityState.Modified;

                // Update committees
                var existingCommittees = _context.BarangayOfficialCommittees
                    .Where(boc => boc.BarangayOfficialId == BarangayOfficial.Id);
                // Remove old associations
                _context.BarangayOfficialCommittees.RemoveRange(existingCommittees);

                if (SelectedCommitteeIds.Any())
                {
                    var officialCommittees = SelectedCommitteeIds.Select(id => new BarangayOfficialCommittee
                    {
                        BarangayOfficialId = BarangayOfficial.Id,
                        CommitteeId = id
                    });
                    _context.BarangayOfficialCommittees.AddRange(officialCommittees);
                   
                }

                await _context.SaveChangesAsync();
                // Set success message and redirect to the list page
                TempData["SuccessMessage"] = "Barangay Official updated successfully!";
                return Page();
            }
            catch (Exception ex)
            {
                // Log the error and show a message
                _logger.LogError(ex, "Error updating Barangay Official.");
                ModelState.AddModelError(string.Empty, "An error occurred while saving the Barangay Official. Please try again.");
                return Page();
            }
        }


    }
}
