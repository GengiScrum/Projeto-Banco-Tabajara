using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public int Add(BankAccount bankAccount)
        {
            bankAccount.Client = _repositoryClient.GetById(bankAccount.Client.Id);
            bankAccount.Validate();
            return _repositoryBankAccount.Add(bankAccount).Id;
        }

        public bool Deposit(BankAccount bankAccount, double value)
        {
            if (value <= 0) throw new BankAccountInvalidTransactionValueException();

            Transaction transaction = new Transaction
            {
                Date = DateTime.Now,
                BankAccount = bankAccount,
                OperationType = OperationTypeEnum.Credit,
                Value = value
            };
            bankAccount.Deposit(value);
            transaction = _repositoryTransaction.Add(transaction);
            if (transaction.Id > 0)
            {
                bankAccount.Transactions.Add(transaction);
                return Update(bankAccount);
            }
            else
                return false;
        }

        public IQueryable<BankAccount> GetAll()
        {
            return _repositoryBankAccount.GetAll();
        }

        public BankAccount GetById(int id)
        {
            if (id == 0) throw new IdentifierUndefinedException();
            return _repositoryBankAccount.GetById(id);
        }

        public bool Remove(BankAccount bankAccount)
        {
            if (bankAccount.Id == 0) throw new IdentifierUndefinedException();
            return _repositoryBankAccount.Remove(bankAccount.Id);
        }

        public bool Transfer(BankAccount originBankAccount, BankAccount receiverBankAccount, double value)
        {
            if (value <= 0) throw new BankAccountInvalidTransactionValueException();
            var withdraw = Withdraw(originBankAccount, value);
            var deposit = Deposit(receiverBankAccount, value);
            if (withdraw && deposit)
                return true;
            else
                return false;
        }

        public bool Update(BankAccount bankAccount)
        {
            if (bankAccount.Id == 0) throw new IdentifierUndefinedException();
            bankAccount.Validate();
            return _repositoryBankAccount.Update(bankAccount);
        }

        public bool Withdraw(BankAccount bankAccount, double value)
        {
            if (value <= 0) throw new BankAccountInvalidTransactionValueException();
            Transaction transaction = new Transaction
            {
                Date = DateTime.Now,
                BankAccount = bankAccount,
                OperationType = OperationTypeEnum.Debit,
                Value = value
            };
            bankAccount.Withdraw(value);
            transaction = _repositoryTransaction.Add(transaction);
            if (transaction.Id > 0)
            {
                bankAccount.Transactions.Add(transaction);
                return Update(bankAccount);
            }
            else
                return false;
        }

        public BankStatement GenerateBankStatement(BankAccount bankAccount, int quantity = 0)
        {
            if (bankAccount.Id == 0)
                throw new IdentifierUndefinedException();

            bankAccount.Transactions = _repositoryTransaction.GetManyByBankAccountId(bankAccount.Id, quantity).ToList();
            return new BankStatement(bankAccount);
        }
    }
}
