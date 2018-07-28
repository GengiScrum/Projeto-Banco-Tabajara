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
            var bank = _repositoryBankAccount.Add(bankAccount);
            return bank.Id;
        }

        public bool Deposit(int id, double value)
        {
            if (value <= 0) throw new BankAccountInvalidTransactionValueException();
            BankAccount bankAccount = GetById(id);
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

        public IQueryable<BankAccount> GetAll(int quantity)
        {
            return _repositoryBankAccount.GetAll(quantity);
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

        public bool Transfer(int originBankAccountId, int receiverBankAccountId, double value)
        {
            if (value <= 0) throw new BankAccountInvalidTransactionValueException();
            if (originBankAccountId == receiverBankAccountId)
                throw new BankAccountTransferToSameBankAccountException();
            var withdraw = Withdraw(originBankAccountId, value);
            var deposit = Deposit(receiverBankAccountId, value);
            if (withdraw && deposit)
                return true;
            else
                return false;
        }

        public bool Update(BankAccount bankAccount)
        {
            if (bankAccount.Id == 0) throw new IdentifierUndefinedException();
            bankAccount.Validate();
            var alteredBankaccount = GetById(bankAccount.Id);
            alteredBankaccount.Client = bankAccount.Client;
            alteredBankaccount.Balance = bankAccount.Balance;
            alteredBankaccount.Activated = bankAccount.Activated;
            alteredBankaccount.Limit = bankAccount.Limit;
            alteredBankaccount.Transactions = bankAccount.Transactions;
            return _repositoryBankAccount.Update(alteredBankaccount);
        }

        public bool Withdraw(int id, double value)
        {
            if (value <= 0) throw new BankAccountInvalidTransactionValueException();
            BankAccount bankAccount = GetById(id);
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

        public BankStatement GenerateBankStatement(int id)
        {
            if (id == 0)
                throw new IdentifierUndefinedException();

            BankAccount bankAccount = GetById(id);
            bankAccount.Transactions = _repositoryTransaction.GetManyByBankAccountId(bankAccount.Id).ToList();
            return CreateBankStatement(bankAccount);
        }

        private static BankStatement CreateBankStatement(BankAccount bankAccount)
        {
            return new BankStatement
            {
                BankAccountNumber = bankAccount.Number,
                IssuanceDate = DateTime.Now,
                ClientName = bankAccount.Client.Name,
                Transactions = bankAccount.Transactions,
                AvailableBalance = bankAccount.Balance,
                ActualLimit = bankAccount.Limit
            };
        }
    }
}
