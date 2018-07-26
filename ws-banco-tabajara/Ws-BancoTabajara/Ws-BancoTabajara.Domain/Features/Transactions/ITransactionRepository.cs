using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ws_BancoTabajara.Domain.Features.Transactions
{
    public interface ITransactionRepository
    {
        Transaction Add(Transaction transaction);
        IQueryable<Transaction> GetManyByBankAccountId(int bankAccountId, int quantity = 0);
    }
}
