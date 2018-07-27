using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Domain.Exceptions;
using Ws_BancoTabajara.Domain.Features.BankAccounts;
using Ws_BancoTabajara.Infra.ORM.Base;

namespace Ws_BancoTabajara.Infra.ORM.Features.BankAccounts
{
    public class BankAccountRepository : IBankAccountRepository
    {
        BancoTabajaraDbContext _context;

        public BankAccountRepository(BancoTabajaraDbContext context)
        {
            _context = context;
        }

        public BankAccount Add(BankAccount bankAccount)
        {
            bankAccount.Validate();
            _context.BankAccounts.Add(bankAccount);
            SaveChanges();
            return bankAccount;
        }

        public IQueryable<BankAccount> GetAll()
        {
            return _context.BankAccounts;
        }

        public BankAccount GetById(int bankAccountId)
        {
            if (bankAccountId == 0)
                throw new IdentifierUndefinedException();

            var bankAccountFound = _context.BankAccounts.Where(ba => ba.Id == bankAccountId).FirstOrDefault() 
                                    ?? throw new NotFoundException();

            return bankAccountFound;
        }

        public bool Remove(int bankAccountId)
        {
            if (bankAccountId == 0)
                throw new IdentifierUndefinedException();

            var bankAccount = GetById(bankAccountId);

            _context.BankAccounts.Remove(bankAccount);
            return SaveChanges();
        }

        public bool SaveChanges()
        {
            var tal = _context.SaveChanges() > 0;
            return tal;
        }

        public bool Update(BankAccount bankAccount)
        {
            if (bankAccount.Id == 0)
                throw new IdentifierUndefinedException();

            bankAccount.Validate();

            var updatedBankAccount = GetById(bankAccount.Id);

            updatedBankAccount = bankAccount;
            return SaveChanges();
        }
    }
}
