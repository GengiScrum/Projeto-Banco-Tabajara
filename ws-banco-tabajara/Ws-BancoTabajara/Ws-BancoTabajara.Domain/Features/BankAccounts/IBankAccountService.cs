using System;
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
        bool Withdraw(int id, double value);
        bool Deposit(int id, double value);
        bool Transfer(int originBankAccountId, int receiverBankAccountId, double value);
        BankStatement GenerateBankStatement(int id);
    }
}
