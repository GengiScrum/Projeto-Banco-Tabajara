using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Domain.Features.BankAccounts;

namespace Ws_BancoTabajara.Infra.ORM.Features.BankAccounts
{
    public class BankAccountConfiguration : EntityTypeConfiguration<BankAccount>
    {
        public BankAccountConfiguration()
        {
            this.ToTable("TBBankAccount");
            this.HasKey(ba => ba.Id);
            this.Property(ba => ba.Activated).IsRequired();
            this.Property(ba => ba.Balance).IsRequired();
            this.Property(ba => ba.Limit).IsRequired();
            this.Property(ba => ba.Number).IsRequired();
            this.Property(ba => ba.TotalBalance).IsRequired();
            this.HasRequired(ba => ba.Client);
            this.HasMany(ba => ba.Transactions);
        }
    }
}
