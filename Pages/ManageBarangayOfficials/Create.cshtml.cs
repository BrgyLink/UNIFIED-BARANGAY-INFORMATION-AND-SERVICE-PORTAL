using System;
using System.Collections.Generic;
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
    [Authorize(Policy = "RequireAdminRole")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CreateModel> _logger;
        private readonly IWebHostEnvironment _environment;

        [BindProperty]
        public BarangayOfficial BarangayOfficial { get; set; } = default!;

        public List<Committee> Committees { get; set; } = new();
        [BindProperty]
        public List<int> SelectedCommitteeIds { get; set; } = new();

        public CreateModel(ApplicationDbContext context, ILogger<CreateModel> logger, IWebHostEnvironment environment)
        {
            _context = context;
            _logger = logger;
            _environment = environment;
        }

        public IActionResult OnGet()
        {
            Committees = _context.Committees.ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Committees = _context.Committees.ToList();
                return Page();
            }

            try
            {
                // Handle image upload
                if (BarangayOfficial.ImageFile != null && BarangayOfficial.ImageFile.Length > 0)
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                    var extension = Path.GetExtension(BarangayOfficial.ImageFile.FileName).ToLowerInvariant();

                    if (!allowedExtensions.Contains(extension))
                    {
                        ModelState.AddModelError("BarangayOfficial.ImageFile", "Only .jpg, .jpeg, and .png files are allowed.");
                        Committees = _context.Committees.ToList();
                        return Page();
                    }

                    if (BarangayOfficial.ImageFile.Length > 5 * 1024 * 1024)
                    {
                        ModelState.AddModelError("BarangayOfficial.ImageFile", "File size cannot exceed 5MB.");
                        Committees = _context.Committees.ToList();
                        return Page();
                    }

                    using (var memoryStream = new MemoryStream())
                    {
                        await BarangayOfficial.ImageFile.CopyToAsync(memoryStream);
                        memoryStream.Position = 0;

                        using (var image = await Image.LoadAsync(memoryStream))
                        {
                            int maxWidth = 800;
                            int maxHeight = 800;

                            if (image.Width > maxWidth || image.Height > maxHeight)
                            {
                                image.Mutate(x => x.Resize(new ResizeOptions
                                {
                                    Size = new Size(maxWidth, maxHeight),
                                    Mode = ResizeMode.Max
                                }));
                            }

                            using (var outputStream = new MemoryStream())
                            {
                                await image.SaveAsJpegAsync(outputStream);
                                BarangayOfficial.Photo = outputStream.ToArray();
                            }
                        }
                    }
                }

                _context.BarangayOfficials.Add(BarangayOfficial);
                await _context.SaveChangesAsync();

                if (SelectedCommitteeIds.Any())
                {
                    var officialCommittees = SelectedCommitteeIds.Select(id => new BarangayOfficialCommittee
                    {
                        BarangayOfficialId = BarangayOfficial.Id,
                        CommitteeId = id
                    });
                    _context.BarangayOfficialCommittees.AddRange(officialCommittees);
                    await _context.SaveChangesAsync();
                }

                TempData["SuccessMessage"] = "Barangay Official created successfully!";
                return RedirectToPage("/ManageBarangayOfficials/Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Barangay Official.");
                ModelState.AddModelError(string.Empty, "An error occurred while saving the Barangay Official. Please try again.");
                Committees = _context.Committees.ToList();
                return Page();
            }
        }
    }
}
