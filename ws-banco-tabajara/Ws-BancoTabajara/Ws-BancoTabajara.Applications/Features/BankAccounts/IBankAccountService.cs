using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Applications.Features.BankAccounts.Commands;
using Ws_BancoTabajara.Applications.Features.BankAccounts.Queries;
using Ws_BancoTabajara.Applications.Features.BankAccounts.ViewModels;
using Ws_BancoTabajara.Domain.Features.BankAccounts;
using Ws_BancoTabajara.Domain.Features.BankStatements;

namespace Ws_BancoTabajara.Applications.Features.BankAccounts
{
    public interface IBankAccountService
    {
        IQueryable<BankAccount> GetAll(BankAccountQuery query);
        int Add(BankAccountRegisterCommand bankAccount);
        bool Update(BankAccountUpdateCommand bankAccount);
        BankAccount GetById(int id);
        bool ChangeActivation(int id);
        bool Remove(BankAccountRemoveCommand bankAccount);
        bool Withdraw(BankAccountOperationCommand operation);
        bool Deposit(BankAccountOperationCommand operation);
        bool Transfer(BankAccountTransferCommand transfer);
        BankStatement GenerateBankStatement(int id);
    }
}
