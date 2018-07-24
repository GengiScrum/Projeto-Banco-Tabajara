using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ws_BancoTabajara.Domain.Features.Clients;
using Ws_BancoTabajara.Domain.Features.Transactions;

namespace Ws_BancoTabajara.Domain.Features.BankAccounts
{
    public class BankAccount : Entity
    {
        public int Number { get; set; }
        public Client Client { get; set; }
        public double Balance { get; set; }
        public bool Activated { get; set; }
        public double Limit { get; set; }
        public double TotalBalance { get { return Limit + Balance; } private set { } }
        public ICollection<Transaction> Transactions { get; set; }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}