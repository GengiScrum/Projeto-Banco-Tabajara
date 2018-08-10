﻿using System;

namespace Ws_BancoTabajara.Domain.Features.Transactions
{
    public class Transaction : Entity
    {
        public DateTime Date { get; set; }
        public OperationTypeEnum OperationType { get; set; }
        public double Value { get; set; }
        //public BankAccount BankAccount { get; set; }
        public int BankAccountId { get; set; }

        public override void Validate()
        {
            if (Value == 0) throw new TransactionInvalidValueException();
            //if (BankAccount == null) throw new TransactionNullBankAccount();
        }

        public override string ToString()
        {
            return string.Format("Date: {0} - Operation Type: {1} - Value: {2}", Date.ToShortTimeString(), OperationType, Value);
        }
    }
}