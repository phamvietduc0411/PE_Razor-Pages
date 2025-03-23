using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace PharmaceuticalManagement_PhamVietDuc1.Pages.Pharmaceutical
{
    public class DeleteModel : PageModel
    {
        private readonly Repository.Models.Sp25PharmaceuticalDbContext _context;


        public DeleteModel(Repository.Models.Sp25PharmaceuticalDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MedicineInformation MedicineInformation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Manager")
            {
                HttpContext.Session.SetString("ErrorMessage", "You don't have permission");
                return RedirectToPage("./Index");

            }
            if (id == null)
            {
                return NotFound();
            }

            var medicineinformation = await _context.MedicineInformations.FirstOrDefaultAsync(m => m.MedicineId == id);

            if (medicineinformation == null)
            {
                return NotFound();
            }
            else
            {
                MedicineInformation = medicineinformation;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicineinformation = await _context.MedicineInformations.FindAsync(id);
            if (medicineinformation != null)
            {
                MedicineInformation = medicineinformation;
                _context.MedicineInformations.Remove(MedicineInformation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
