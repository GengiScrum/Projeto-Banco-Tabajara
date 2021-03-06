﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ws_BancoTabajara.Domain.Features.BankAccounts
{
    public interface IBankAccountRepository
    {
        IQueryable<BankAccount> GetAll(int quantity);
        BankAccount Add(BankAccount bankAccount);
        bool Update(BankAccount bankAccount);
        BankAccount GetById(int bankAccountId);
        bool Remove(int bankAccountId);
        bool SaveChanges();
    }
}
