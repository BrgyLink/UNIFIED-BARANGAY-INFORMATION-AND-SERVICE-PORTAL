using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BrgyLink.Models;
using BrgyLink.Services;

namespace BrgyLink.Pages.ManageBlotterReports
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly PdfService _pdfService;


        public CreateModel(ApplicationDbContext context, PdfService pdfService)
        {
            _context = context;
            _pdfService = pdfService;
        }


        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Blotter Blotter { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Blotter == null || Blotter == null)
            {
                return Page();
            }

            _context.Blotter.Add(Blotter);
            await _context.SaveChangesAsync();
            Blotter.DateReported = System.DateTime.Now;
            Blotter.LastUpdated = System.DateTime.Now;
            Blotter.Status = "Pending";

            return RedirectToPage("./Index");
        }
    }
}
