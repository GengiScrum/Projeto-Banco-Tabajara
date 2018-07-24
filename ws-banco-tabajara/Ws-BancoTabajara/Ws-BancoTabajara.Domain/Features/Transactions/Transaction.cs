using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ws_BancoTabajara.Domain.Features.BankAccounts;

namespace Ws_BancoTabajara.Domain.Features.Transactions
{
    public class Transaction : Entity
    {
        public DateTime Date { get; set; }
        public OperationTypeEnum OperationType { get; set; }
        public double Value { get; set; }
        public BankAccount BankAccount { get; set; }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}