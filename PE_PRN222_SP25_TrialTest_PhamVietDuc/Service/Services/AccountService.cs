using Repository.Models;
using Repository.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public  class AccountService
    {
        protected readonly AccountRepository _accountRepo;
        public AccountService(AccountRepository accountRepo)
        {
            _accountRepo = accountRepo;
        }

        public async Task<StoreAccount>Login(string email, string password)
        {
            var result = await _accountRepo.GetAccount(email, password);

            return result;
        }
    }
}
