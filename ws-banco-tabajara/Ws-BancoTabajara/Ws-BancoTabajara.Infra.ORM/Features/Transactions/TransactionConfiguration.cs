using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Domain.Features.Transactions;

namespace Ws_BancoTabajara.Infra.ORM.Features.Transactions
{
    public class TransactionConfiguration : EntityTypeConfiguration<Transaction>
    {
        public TransactionConfiguration()
        {
            ToTable("TBTransaction");
            HasKey(t => t.Id);
            Property(t => t.Date).IsRequired();
            Property(t => t.OperationType).IsRequired();
            Property(t => t.Value).IsRequired();
            HasRequired(t => t.BankAccount);
        }
    }
}
