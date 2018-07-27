using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Domain.Features.Transactions;
using Ws_BancoTabajara.Infra.ORM.Base;

namespace Ws_BancoTabajara.Infra.ORM.Features.Transactions
{
    public class TransactionRepository : ITransactionRepository
    {
        BancoTabajaraDbContext _context;
        public TransactionRepository(BancoTabajaraDbContext context)
        {
            _context = context;
        }
        public Transaction Add(Transaction transaction)
        {
            transaction = _context.Transactions.Add(transaction);
            _context.SaveChanges();
            return transaction;
        }

        public IQueryable<Transaction> GetManyByBankAccountId(int bankAccountId)
        {
            return _context.Transactions.Where(t => t.BankAccount.Id == bankAccountId);
        }
    }
}
