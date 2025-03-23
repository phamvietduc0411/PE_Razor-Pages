using Repository.Models;
using Repository.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
   public class ManufactureService
    {
        protected readonly ManufactureRepo _manufactureRepo;

        public ManufactureService(ManufactureRepo manufactureRepo)
        {
            _manufactureRepo = manufactureRepo;
        }

        public async Task<List<Manufacturer>> GetAll()
        {
            return await _manufactureRepo.GetAll();
        }
    }
}
