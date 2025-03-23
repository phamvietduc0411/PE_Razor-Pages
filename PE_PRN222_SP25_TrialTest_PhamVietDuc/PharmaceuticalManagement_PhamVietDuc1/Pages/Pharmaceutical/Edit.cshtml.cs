using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Repository.Repo;
using Service.Services;

namespace PharmaceuticalManagement_PhamVietDuc1.Pages.Pharmaceutical
{
    public class EditModel : PageModel
    {
        private readonly Sp25PharmaceuticalDbContext _context;
        public List<SelectListItem> Manufacturers { get; set; } = new();
        public string ErrorMessage { get; set; }
        private readonly ManufactureService _manufactureService;
        private readonly MedicineService _medicineService;
        [BindProperty]
        public MedicineInformation MedicineInformation { get; set; } = default!;

        public EditModel(Repository.Models.Sp25PharmaceuticalDbContext context, ManufactureService manufactureService, MedicineService medicineService)
        {
            _context = context;
            _manufactureService = manufactureService;
            _medicineService = medicineService;
        }



        public async Task<IActionResult> OnGet(string id)
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
            MedicineInformation = medicineinformation;
            await LoadManufacturers();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(MedicineInformation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicineInformationExists(MedicineInformation.MedicineId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MedicineInformationExists(string id)
        {
            return _context.MedicineInformations.Any(e => e.MedicineId == id);
        }

        private async Task LoadManufacturers()
        {
            var manufacturers = await _manufactureService.GetAll();

            Manufacturers = manufacturers.Select(m => new SelectListItem
            {
                Value = m.ManufacturerId.ToString(),
                Text = m.ManufacturerName
            }).ToList();
        }
    }
}
