using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Domain.Exceptions;
using Ws_BancoTabajara.Domain.Features.BankAccounts;
using Ws_BancoTabajara.Domain.Features.BankStatements;
using Ws_BancoTabajara.Domain.Features.Transactions;

namespace Ws_BancoTabajara.Applications.Features.BankAccounts
{
    public class BankAccountService : IBankAccountService
    {
        private readonly IBankAccountRepository _repositoryBankAccount;
        private readonly ITransactionRepository _repositoryTransaction;

        public BankAccountService(IBankAccountRepository repositoryBankAccount, ITransactionRepository repositoryTransaction)
        {
            _repositoryBankAccount = repositoryBankAccount;
            _repositoryTransaction = repositoryTransaction;
        }

        public int Add(BankAccount bankAccount)
        {
            bankAccount.Validate();
            return _repositoryBankAccount.Add(bankAccount).Id;
        }

        public void Deposit(BankAccount bankAccount, double value)
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
            bankAccount.Transactions.Add(transaction);
            Update(bankAccount);
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

        public void Transfer(BankAccount originBankAccount, BankAccount receiverBankAccount, double value)
        {
            if (value <= 0) throw new BankAccountInvalidTransactionValueException();
            Withdraw(originBankAccount, value);
            Deposit(receiverBankAccount, value);
        }

        public bool Update(BankAccount bankAccount)
        {
            if (bankAccount.Id == 0) throw new IdentifierUndefinedException();
            bankAccount.Validate();
            return _repositoryBankAccount.Update(bankAccount);
        }

        public void Withdraw(BankAccount bankAccount, double value)
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
            bankAccount.Transactions.Add(transaction);
            Update(bankAccount);
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
