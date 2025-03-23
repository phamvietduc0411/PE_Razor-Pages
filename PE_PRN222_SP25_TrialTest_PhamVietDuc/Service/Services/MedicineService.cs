using Repository.Models;
using Repository.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class MedicineService
    {
        protected readonly MedicineRepository _medicineRepo;
        public MedicineService(MedicineRepository medicineRepo)
        {
            _medicineRepo = medicineRepo;
        }

        public async Task<List<MedicineInformation>> GetAllMedicine()
        {
            return await _medicineRepo.GetList();

        }

        public async Task<MedicineInformation> Create(MedicineInformation medicine)
        {
            return await _medicineRepo.Create(medicine);

        }

        public async Task<MedicineInformation> Update(MedicineInformation medicine)
        {
            return await _medicineRepo.Update(medicine);

        }

        public async Task<MedicineInformation> Delete(MedicineInformation medicine)
        {
            return await _medicineRepo.Delete(medicine);

        }

        public async Task<List<MedicineInformation>> Search(string? search)
        {
            return await _medicineRepo.SearchAsync(search);

        }

        public async Task<MedicineInformation> GetMedicineByID(string id)
        {
            return await _medicineRepo.GetIdAsync(id);
        }






    }
}
