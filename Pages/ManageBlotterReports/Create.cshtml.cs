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


        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
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
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                return Page();
            }

            //// Set default status if not provided
            //if (string.IsNullOrEmpty(Blotter.Status))
            //{
            //    Blotter.Status = "Pending"; // Default value for Status
            //}
            // Get the current user's username from HttpContext
            var currentUserName = HttpContext.User.Identity.Name; // Get username directly from the claims
            var adminLog = new AdminLogs
            {
                Firstname = $"{currentUserName}",
                Actions = "Blotter",
                Description = $"Added a new Incident " + Blotter.Incident + " on Blotter Reports",
                Role = "Official",
                Date = DateTime.Now
            };

            Blotter.DateReported = DateTime.Now;
            Blotter.LastUpdated = DateTime.Now;

            _context.AdminLogs.Add(adminLog);
            _context.Blotter.Add(Blotter);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}
