using System;
using System.IO;
using System.Drawing;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PdfSharpCore.Pdf;
using PdfSharpCore.Drawing;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using System.IO;
using BrgyLink.Models;
using PdfSharpCore.Drawing.Layout;

namespace BrgyLink.Pages.ManageBlotterReports
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        // Constructor only accepts ApplicationDbContext
        public DetailsModel(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Blotter Blotter { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Blotter == null)
            {
                return NotFound();
            }

            var blotter = await _context.Blotter.FirstOrDefaultAsync(m => m.Id == id);
            if (blotter == null)
            {
                return NotFound();
            }
            else
            {
                Blotter = blotter;
            }

            return Page();
        }
        public IActionResult OnPostGeneratePdf(int id)
        {
            // Fetch the blotter information from the database
            var blotter = _context.Blotter.FirstOrDefault(m => m.Id == id);
            if (blotter == null)
            {
                return NotFound();
            }

            // Create the PDF document
            var document = new PdfDocument();
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);

            // Define fonts for titles, headings, and content
            var titleFont1 = new XFont("Arial", 14, XFontStyle.Bold);
            var headerFont1 = new XFont("Arial", 12, XFontStyle.Bold);
            var headerFont = new XFont("Arial", 12, XFontStyle.Regular);
            var contentFont = new XFont("Arial", 12, XFontStyle.Regular);

            // Define margins and spacings
            double marginLeft = 50;
            double marginTop = 50;
            double lineSpacing = 18;
            double sectionSpacing = 30;
            double footerMargin = 50;

            // Set initial position for drawing
            double currentY = marginTop;

            // Define the path to the logo file
            string logoFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "logoBrgySeal.png");

            try
            {
                if (System.IO.File.Exists(logoFilePath))
                {
                    // Load the image using System.Drawing
                    using (var bitmap = new System.Drawing.Bitmap(logoFilePath))
                    {
                        // Convert to a stream for PdfSharp compatibility
                        using (var stream = new MemoryStream())
                        {
                            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                            stream.Position = 0;

                            // Use Func<Stream> to wrap the MemoryStream
                            Func<System.IO.Stream> streamFunc = () => stream;

                            var logo = XImage.FromStream(streamFunc);
                            gfx.DrawImage(logo, marginLeft, marginTop, 100, 100); // Adjust dimensions as needed
                        }
                    }
                }
                else
                {
                    throw new FileNotFoundException($"Logo file not found at: {logoFilePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading or displaying logo: {ex.Message}");
            }



            // Center-align Header text
            double headerX = page.Width / 2;
            gfx.DrawString("Republic of the Philippines", headerFont, XBrushes.Black, new XPoint(headerX, marginTop), XStringFormats.Center);
            gfx.DrawString("Province of Cebu", headerFont, XBrushes.Black, new XPoint(headerX, marginTop + 15), XStringFormats.Center);
            gfx.DrawString("Municipality of Compostela", headerFont, XBrushes.Black, new XPoint(headerX, marginTop + 30), XStringFormats.Center);
            gfx.DrawString("Barangay Cabadiangan", headerFont, XBrushes.Black, new XPoint(headerX, marginTop + 45), XStringFormats.Center);
            gfx.DrawString("OFFICE OF THE PUNONG BARANGAY", headerFont, XBrushes.Black, new XPoint(headerX, marginTop + 60), XStringFormats.Center);

            // Horizontal line after header
            double headerLineY = marginTop + 100;
            gfx.DrawLine(XPens.Black, marginLeft, headerLineY, page.Width - marginLeft, headerLineY);
            currentY = headerLineY + 30; // Start after the header

            // Title
            gfx.DrawString("Blotter Report", titleFont1, XBrushes.Black, new XPoint(headerX, currentY), XStringFormats.Center);
            currentY += lineSpacing + 30;

            // Create a helper method to render multi-line sections
            void DrawMultiLineText(string label, string content)
            {
                gfx.DrawString(label, headerFont1, XBrushes.Black, new XPoint(marginLeft, currentY)); // Label
                currentY += lineSpacing;

                var contentRect = new XRect(marginLeft, currentY, page.Width - 2 * marginLeft, page.Height - footerMargin - currentY);
                var formatter = new XTextFormatter(gfx);
                formatter.DrawString(content, contentFont, XBrushes.Black, contentRect, XStringFormats.TopLeft);

                // Measure the height of the content
                var measuredHeight = gfx.MeasureString(content, contentFont).Height;
                currentY += measuredHeight + sectionSpacing;
            }

            // Draw each section
            DrawMultiLineText("Incident:", blotter.Incident);
            DrawMultiLineText("Complainant:", blotter.Complainant);
            DrawMultiLineText("Respondent:", blotter.Respondent);
            DrawMultiLineText("Date Reported:", blotter.DateReported.ToString("yyyy-MM-dd"));
            DrawMultiLineText("Action Taken:", blotter.ActionTaken);
            currentY += lineSpacing + 10;
            DrawMultiLineText("Status:", blotter.Status);

            // Footer Section
            currentY = page.Height - footerMargin - 30;
            gfx.DrawLine(XPens.Black, marginLeft, currentY, page.Width - marginLeft, currentY); // Footer line
            currentY += 25;

            gfx.DrawString("For inquiries, please contact Barangay Cabadiangan Office.", contentFont, XBrushes.Black, new XPoint(marginLeft, currentY));
            currentY += lineSpacing;
            gfx.DrawString("Phone: +63 1234567891 | Email: cabadiangancompostela@gmail.com", contentFont, XBrushes.Black, new XPoint(marginLeft, currentY));

            // Save the PDF to a memory stream
            using (var stream = new MemoryStream())
            {
                document.Save(stream, false);
                var fileBytes = stream.ToArray();

                return new FileContentResult(fileBytes, "application/pdf")
                {
                    FileDownloadName = $"BlotterReport_{blotter.Incident}_{blotter.DateReported:yyyyMMdd}.pdf"
                };
            }
        }






    }
}
