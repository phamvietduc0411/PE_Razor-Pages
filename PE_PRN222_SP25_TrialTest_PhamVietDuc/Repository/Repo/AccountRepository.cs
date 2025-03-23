using Microsoft.EntityFrameworkCore;

using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repo
{
    public class AccountRepository
    {
        private readonly Sp25PharmaceuticalDbContext _context;

        public AccountRepository(Sp25PharmaceuticalDbContext context)
        {
            _context = context;
        }

        public async Task<StoreAccount> GetAccount(string email, string pass)
        {
            var result = await _context.StoreAccounts.FirstOrDefaultAsync(a => a.EmailAddress == email && a.StoreAccountPassword == pass);
            return result;
        }

    }
}
