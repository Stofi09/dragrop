using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Dragdrop.Pages.report
{
    public class reportModel : PageModel
    {
        [BindProperty]
        public BugReportInputModel Input { get; set; }
        public string TextDescription { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Validate the total file size (15MB)
            const long maxTotalSize = 15 * 1024 * 1024;
            long totalSize = Input.Files?.Sum(f => f.Length) ?? 0;
            if (totalSize > maxTotalSize)
            {
                ModelState.AddModelError(string.Empty, "Total file size exceeds 15MB limit");
                return Page();
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                Console.WriteLine(Input.Description);


                Console.WriteLine("//////////////////");


                Console.WriteLine(Input.TextDescription);

                // Now you have access to:
                // Input.Description - HTML with embedded images
                // Input.TextDescription - Text with [Image1] placeholders
                // Input.Files - List of uploaded files

                var containsImages = Input.Description?.Contains("<img") ?? false;
                return RedirectToPage("./Success");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while processing your report. Please try again.");
                return Page();
            }
        }
    }

    public class BugReportInputModel
    {
        [Required]
        [Display(Name = "Title")]
        [StringLength(200, ErrorMessage = "Title cannot be longer than 200 characters")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        // Add this new property
        public string TextDescription { get; set; }

        [Display(Name = "Steps to Reproduce")]
        public string StepsToReproduce { get; set; }

        [Display(Name = "Expected Behavior")]
        public string ExpectedBehavior { get; set; }

        [Display(Name = "Actual Behavior")]
        public string ActualBehavior { get; set; }

        [Display(Name = "Environment")]
        public string Environment { get; set; }

        public List<IFormFile> Files { get; set; } = new();
    }
}