using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Applications.Features.BankAccounts.Commands;
using Ws_BancoTabajara.Applications.Features.BankAccounts.Queries;
using Ws_BancoTabajara.Applications.Features.BankAccounts.ViewModels;
using Ws_BancoTabajara.Domain.Exceptions;
using Ws_BancoTabajara.Domain.Features.BankAccounts;
using Ws_BancoTabajara.Domain.Features.BankStatements;
using Ws_BancoTabajara.Domain.Features.Clients;
using Ws_BancoTabajara.Domain.Features.Transactions;

namespace Ws_BancoTabajara.Applications.Features.BankAccounts
{
    public class BankAccountService : IBankAccountService
    {
        private readonly IBankAccountRepository _repositoryBankAccount;
        private readonly ITransactionRepository _repositoryTransaction;
        private readonly IClientRepository _repositoryClient;

        public BankAccountService(IBankAccountRepository repositoryBankAccount, ITransactionRepository repositoryTransaction, IClientRepository repositoryClient)
        {
            _repositoryBankAccount = repositoryBankAccount;
            _repositoryTransaction = repositoryTransaction;
            _repositoryClient = repositoryClient;
        }

        public int Add(BankAccountRegisterCommand bankAccountCommand)
        {
            var bankAccount = Mapper.Map<BankAccountRegisterCommand, BankAccount>(bankAccountCommand);
            bankAccount = _repositoryBankAccount.Add(bankAccount);
            return bankAccount.Id;
        }

        public bool Deposit(BankAccountOperationCommand operation)
        {
            var bankAccount = GetById(operation.Id);
            bankAccount.Deposit(operation.Value);
            return _repositoryBankAccount.Update(bankAccount);
        }

        public IQueryable<BankAccount> GetAll(BankAccountQuery query)
        {
            return _repositoryBankAccount.GetAll(query.Quantity);
        }

        public BankAccount GetById(int id)
        {
            if (id == 0) throw new IdentifierUndefinedException();
            return _repositoryBankAccount.GetById(id);
        }

        public bool ChangeActivation(int id)
        {
            if (id == 0)
                throw new IdentifierUndefinedException();

            BankAccount bankAccount = GetById(id);

            if (bankAccount == null)
                throw new NotFoundException();

            bankAccount.ChangeActivation();
            return _repositoryBankAccount.Update(bankAccount);
        }

        public bool Remove(BankAccountRemoveCommand bankAccount)
        {
            if (bankAccount.Id == 0) throw new IdentifierUndefinedException();
            return _repositoryBankAccount.Remove(bankAccount.Id);
        }

        public bool Transfer(BankAccountTransferCommand transfer)
        {
            var withdraw = Withdraw(new BankAccountOperationCommand
                { Id = transfer.OriginId, Value = transfer.Value});
            var deposit = Deposit(new BankAccountOperationCommand
                { Id = transfer.DestinationId, Value = transfer.Value});
            if (withdraw && deposit)
                return true;
            else
                return false;
        }

        public bool Update(BankAccountUpdateCommand bankAccount)
        {
            int number = GetById(bankAccount.Id).Number;
            var alteredBankaccount = Mapper.Map<BankAccountUpdateCommand, BankAccount>(bankAccount);
            if (alteredBankaccount.Number != number)
                throw new BankAccountUpdateWithANewNumberException();
            return _repositoryBankAccount.Update(alteredBankaccount);
        }

        public bool Withdraw(BankAccountOperationCommand operation)
        {
            BankAccount bankAccount = GetById(operation.Id);
            bankAccount.Withdraw(operation.Value);
            return _repositoryBankAccount.Update(bankAccount);
        }

        public BankStatement GenerateBankStatement(int id)
        {
            if (id == 0)
                throw new IdentifierUndefinedException();

            BankAccount bankAccount = GetById(id);
            bankAccount.Transactions = _repositoryTransaction.GetManyByBankAccountId(bankAccount.Id).ToList();
            BankStatement bankStatement = new BankStatement();
            bankStatement.GenerateBankStatement(bankAccount);
            return bankStatement;
        }
    }
}
