using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Domain.Features.BankAccounts;
using Ws_BancoTabajara.Domain.Features.Clients;
using Ws_BancoTabajara.Domain.Features.Transactions;

namespace Ws_BancoTabajara.Infra.ORM.Base
{
    public class BancoTabajaraDbContext : DbContext
    {
        public BancoTabajaraDbContext(string connection = "name=BancoTabajara_Gengiscrum") : base(connection)
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        protected BancoTabajaraDbContext(DbConnection connection) : base(connection, true) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
