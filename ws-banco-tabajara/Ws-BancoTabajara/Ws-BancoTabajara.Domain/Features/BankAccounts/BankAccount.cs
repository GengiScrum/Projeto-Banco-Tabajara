using System;
using System.Collections.Generic;
using Ws_BancoTabajara.Domain.Features.Clients;
using Ws_BancoTabajara.Domain.Features.Transactions;

namespace Ws_BancoTabajara.Domain.Features.BankAccounts
{
    public class BankAccount : Entity
    {
        public BankAccount()
        {
            Transactions = new List<Transaction>();
        }

        public int Number { get; set; }
        public virtual Client Client { get; set; }
        public double Balance { get; set; }
        public bool Activated { get; set; }
        public double Limit { get; set; }
        public double TotalBalance { get { return Balance + Limit; } private set { } }
        public ICollection<Transaction> Transactions { get; set; }

        public override void Validate()
        {
            if (Number <= 0) throw new BankAccountInvalidNumberException();
            if (Client == null) throw new BankAccountWithoutClientException();

            Client.Validate();
        }

        public void ChangeActivation()
        {
            if (Activated)
                this.Activated = false;
            else
                this.Activated = true;
        }

        public void Withdraw(double value)
        {
            if (value <= 0) throw new BankAccountInvalidTransactionValueException();

            if (value > TotalBalance)
                throw new BankAccountWhitdrawValueHigherThanTotalBalanceException();

            this.Balance -= value;
            AddNewTransaction(value, OperationTypeEnum.Debit);

        }

        public void Deposit(double value)
        {
            if (value <= 0) throw new BankAccountInvalidTransactionValueException();

            this.Balance += value;
            AddNewTransaction(value, OperationTypeEnum.Credit);
        }

        private void AddNewTransaction(double value, OperationTypeEnum type)
        {
            Transaction transaction = new Transaction
            {
                Date = DateTime.Now,
                BankAccountId = Id,
                OperationType = type,
                Value = value
            };

            Transactions.Add(transaction);
        }
    }
}