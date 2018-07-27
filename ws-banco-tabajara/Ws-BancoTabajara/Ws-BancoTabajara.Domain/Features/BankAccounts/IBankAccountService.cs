﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Domain.Features.BankStatements;

namespace Ws_BancoTabajara.Domain.Features.BankAccounts
{
    public interface IBankAccountService
    {
        IQueryable<BankAccount> GetAll();
        int Add(BankAccount bankAccount);
        bool Update(BankAccount bankAccount);
        BankAccount GetById(int id);
        bool Remove(BankAccount bankAccount);
        void Withdraw(BankAccount bankAccount, double value);
        void Deposit(BankAccount bankAccount, double value);
        void Transfer(BankAccount originBankAccount, BankAccount receiverBankAccount, double value);
        BankStatement GenerateBankStatement(BankAccount bankAccount, int quantity = 0);
    }
}