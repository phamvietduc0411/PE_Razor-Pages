using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Repository.Repo;
using Service.Services;

namespace PharmaceuticalManagement_PhamVietDuc1.Pages.Pharmaceutical
{
    public class IndexModel : PageModel
    {
        private readonly Sp25PharmaceuticalDbContext _context;
        public string ErrorMessage { get; set; }

        public string SearchTerm { get; set; }

        private readonly MedicineService _medicineService;


        public IndexModel(Sp25PharmaceuticalDbContext context,MedicineService medicineService)
        {
            _context = context;
            _medicineService = medicineService;
        }
        public IList<MedicineInformation> MedicineInformation { get; set; } = default!;
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 3; // Số bản ghi trên mỗi trang


        //public async Task OnGetAsync(int? pageIndex)
        //{
        //    ErrorMessage = HttpContext.Session.GetString("ErrorMessage") ?? string.Empty;
        //    HttpContext.Session.Remove("ErrorMessage");

        //    PageIndex = pageIndex ?? 1;
        //    var query = _context.MedicineInformations.Include(m => m.Manufacturer);

        //    int totalRecords = await query.CountAsync();
        //    TotalPages = (int)Math.Ceiling(totalRecords / (double)PageSize);

        //    MedicineInformation = await query
        //        .Skip((PageIndex - 1) * PageSize)
        //        .Take(PageSize)
        //        .OrderBy(m => m.MedicineName)
        //        .ToListAsync();
        //}

        public async Task<IActionResult> OnGetAsync(int? pageIndex, string? searchTerm)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToPage("/Login/Login");
            }

            ErrorMessage = HttpContext.Session.GetString("ErrorMessage") ?? string.Empty;
            HttpContext.Session.Remove("ErrorMessage");

            PageIndex = pageIndex ?? 1;


            var query = await _medicineService.Search(searchTerm);


            int totalRecords = query.Count();
            TotalPages = (int)Math.Ceiling(totalRecords / (double)PageSize);

            MedicineInformation = query
                .Skip((PageIndex - 1) * PageSize)
                .Take(PageSize)
                .OrderBy(m => m.MedicineName)
                .ToList();

            return Page();
        }


    }
}

