using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ws_BancoTabajara.Applications.Features.BankAccounts.ViewModels
{
    public class BankAccountViewModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string ClientName { get; set; }
        public double Balance { get; set; }
        public bool Activated { get; set; }
        public double Limit { get; set; }
        public double TotalBalance { get; set; }
    }
}
