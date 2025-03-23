using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repo
{
    public class ManufactureRepo 
    {
        private readonly Sp25PharmaceuticalDbContext _contextt;
        public ManufactureRepo(Sp25PharmaceuticalDbContext contextt)
        {
            _contextt = contextt;
        }

        public async Task<List<Manufacturer>> GetAll()
        {
            return await _contextt.Manufacturers.ToListAsync();
        }
    }
}

