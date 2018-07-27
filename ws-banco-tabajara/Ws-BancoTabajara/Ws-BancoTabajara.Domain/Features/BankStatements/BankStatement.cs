using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Domain.Features.BankAccounts;
using Ws_BancoTabajara.Domain.Features.Transactions;

namespace Ws_BancoTabajara.Domain.Features.BankStatements
{
    public class BankStatement
    {
        public BankStatement(BankAccount bankAccount)
        {
            BankAccountNumber = bankAccount.Number;
            IssuanceDate = DateTime.Now;
            ClientName = bankAccount.Client.Name;
            Transactions = bankAccount.Transactions;
            AvailableBalance = bankAccount.Balance;
            ActualLimit = bankAccount.Limit;
        }

        public BankStatement()
        {

        }

        public int BankAccountNumber { get; private set; }
        public DateTime IssuanceDate { get; private set; }
        public string ClientName { get; private set; }
        public ICollection<Transaction> Transactions { get; private set; }
        public double AvailableBalance { get; private set; }
        public double ActualLimit { get; private set; }
    }
}
