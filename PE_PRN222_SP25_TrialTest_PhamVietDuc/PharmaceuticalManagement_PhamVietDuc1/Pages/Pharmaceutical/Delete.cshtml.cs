using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Service.Services;

namespace PharmaceuticalManagement_PhamVietDuc1.Pages.Pharmaceutical
{
    public class DeleteModel : PageModel
    {
        private readonly Repository.Models.Sp25PharmaceuticalDbContext _context;
        private readonly ManufactureService _manufactureService;
        private readonly MedicineService _medicineService;


        public DeleteModel(Repository.Models.Sp25PharmaceuticalDbContext context, ManufactureService manufactureService, MedicineService medicineService)
        {
            _context = context;
            _manufactureService = manufactureService;
            _medicineService = medicineService;
        }

        [BindProperty]
        public MedicineInformation MedicineInformation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var role = HttpContext.Session.GetInt32("UserRole");
            if (role != 2)
            {
                HttpContext.Session.SetString("ErrorMessage", "You don't have permission");
                return RedirectToPage("./Index");

            }
            if (id == null)
            {
                return NotFound();
            }

            var medicineinformation = await _medicineService.GetMedicineByID(id);

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

            var medicineinformation = await _medicineService.GetMedicineByID(id);
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
