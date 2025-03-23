using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Models;
using Service.Services;

namespace PharmaceuticalManagement_PhamVietDuc1.Pages.Pharmaceutical
{
    public class CreateModel : PageModel
    {
        private readonly Sp25PharmaceuticalDbContext _context;
        public List<SelectListItem> Manufacturers { get; set; } = new();
        public string ErrorMessage { get; set; }
        private readonly ManufactureService _manufactureService;
        private readonly MedicineService _medicineService;
        [BindProperty]
        public MedicineInformation MedicineInformation { get; set; } = default!;

        public CreateModel(Repository.Models.Sp25PharmaceuticalDbContext context, ManufactureService manufactureService, MedicineService medicineService)
        {
            _context = context;
            _manufactureService = manufactureService;
            _medicineService = medicineService;
        }

        public async Task<IActionResult> OnGet()
        {
            var role = HttpContext.Session.GetInt32("UserRole");
            if (role != 2)
            {
                HttpContext.Session.SetString("ErrorMessage", "You don't have permission");
                return RedirectToPage("./Index");

            }
            await LoadManufacturers();
            return Page();
        }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            await _medicineService.Create(MedicineInformation);

            return RedirectToPage("./Index");
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
