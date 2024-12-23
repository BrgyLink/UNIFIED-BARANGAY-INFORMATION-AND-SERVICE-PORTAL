using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace BrgyLink.Pages.ManageFacility
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
        public Facility Facility { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            // Log debug information to the console
            Console.WriteLine("Entering OnPostAsync method in ManageFacility.CreateModel.");
            Console.WriteLine($"ModelState.IsValid: {ModelState.IsValid}");
            Console.WriteLine($"Facility: {Facility?.Name ?? "null"}");

            foreach (var modelStateKey in ModelState.Keys)
            {
                var value = ModelState[modelStateKey];
                foreach (var error in value.Errors)
                {
                    Console.WriteLine($"Key: {modelStateKey}, Error: {error.ErrorMessage}");
                }
            }

            // Check if the model state is valid
            if (!ModelState.IsValid || _context.Facilities == null || Facility == null)
            {
                Console.WriteLine("ModelState is invalid, or Facility is null.");
                return Page();
            }

            Console.WriteLine($"Adding facility: {Facility.Name}");

            // Get the current user's username from HttpContext
            var currentUserName = HttpContext.User.Identity.Name; // Get username directly from the claims

            // Create a FacilityLog entry with FacilityName instead of FacilityId
            var facilityLog = new FacilityLog
            {
                FacilityName = Facility.Name, // Store the name of the facility in the log
                Action = $"Added by: {currentUserName}", // Action description with the user's name
                LogDate = DateTime.Now // Set the log's date and time
            };

            // Add the Facility to the database
            _context.Facilities.Add(Facility);

            // Save changes to the Facility table
            await _context.SaveChangesAsync();

            // Add the FacilityLog to the database
            _context.FacilityLogs.Add(facilityLog);

            // Save changes to the FacilityLog table
            await _context.SaveChangesAsync();

            Console.WriteLine($"Facility {Facility.Name} saved and log added successfully.");

            // Redirect to the Index page after saving the facility and log
            return RedirectToPage("./Index");
        }
    }
}
