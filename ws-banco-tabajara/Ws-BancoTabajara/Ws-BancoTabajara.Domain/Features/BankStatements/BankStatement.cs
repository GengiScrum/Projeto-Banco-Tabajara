using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Domain.Exceptions;
using Ws_BancoTabajara.Domain.Features.BankAccounts;
using Ws_BancoTabajara.Domain.Features.Transactions;

namespace Ws_BancoTabajara.Domain.Features.BankStatements
{
    public class BankStatement
    {
        public int BankAccountNumber { get; set; }
        public DateTime IssuanceDate { get; set; }
        public string ClientName { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public double AvailableBalance { get; set; }
        public double ActualLimit { get; set; }

        public void GenerateBankStatement(BankAccount bankAccount)
        {
            if (bankAccount == null)
                throw new NotFoundException();

            BankAccountNumber = bankAccount.Number;
            IssuanceDate = DateTime.Now;
            ClientName = bankAccount.Client.Name;
            Transactions = bankAccount.Transactions;
            AvailableBalance = bankAccount.Balance;
            ActualLimit = bankAccount.Limit;
        }
    }
}
