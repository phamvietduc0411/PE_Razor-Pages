using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Repo
{
    public class MedicineRepository 
    {
        private readonly Sp25PharmaceuticalDbContext _context;

        public MedicineRepository(Sp25PharmaceuticalDbContext context)
        {
            _context = context;
        }

        public async Task<List<MedicineInformation>> GetList()
        {
            var result = await _context.MedicineInformations.Include(m => m.Manufacturer).ToListAsync();
            return result;
        }

        public async Task<MedicineInformation> Create(MedicineInformation medicine)
        {
            var newMedicine = await _context.MedicineInformations.AddAsync(medicine);
            await _context.SaveChangesAsync();
            return newMedicine.Entity;
        }

        public async Task<MedicineInformation> Update(MedicineInformation medicine)
        {
            var newMedicine = _context.MedicineInformations.Update(medicine);
            await _context.SaveChangesAsync();
            return newMedicine.Entity;
        }

        public async Task<MedicineInformation> Delete(MedicineInformation medicine)
        {
            var newMedicine = _context.MedicineInformations.Remove(medicine);
            await _context.SaveChangesAsync();
            return newMedicine.Entity;
        }

        public async Task<MedicineInformation> GetIdAsync(string id)
        {
            var rs = await _context.MedicineInformations
     .Include(m => m.Manufacturer)
     .FirstOrDefaultAsync(m => m.MedicineId == id);


            return rs != null ? rs : null;

            //if(rs != null)
            //{
            //    return rs;
            //}
            //return null;
        }

        //public async Task<List<MedicineInformation>> SearchAsync(
        //    string? activeIngredients, string? expirationDate, string? warningsAndPrecautions)
        //{
        //    var query = _context.MedicineInformations.Include(m => m.Manufacturer).AsQueryable();

        //    if (!string.IsNullOrEmpty(activeIngredients))
        //    {
        //        query = query.Where(m => m.ActiveIngredients.Contains(activeIngredients));
        //    }

        //    if (!string.IsNullOrEmpty(expirationDate))
        //    {
        //        query = query.Where(m => m.ExpirationDate == expirationDate);
        //    }

        //    if (!string.IsNullOrEmpty(warningsAndPrecautions))
        //    {
        //        query = query.Where(m => m.WarningsAndPrecautions.Contains(warningsAndPrecautions));
        //    }

        //    return await query.ToListAsync();
        //}

        public async Task<List<MedicineInformation>> SearchAsync(string? searchTerm)
        {
            var query = _context.MedicineInformations.Include(m => m.Manufacturer).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(m =>
                    m.ActiveIngredients.Contains(searchTerm) ||
                    m.ExpirationDate.Contains(searchTerm) ||
                    m.WarningsAndPrecautions.Contains(searchTerm) ||
                    m.MedicineName.Contains(searchTerm) 
                );
            }

            return await query.ToListAsync();
        }



    }
}
