using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ws_BancoTabajara.Infra.ORM.Base
{
    [ExcludeFromCodeCoverage]
    public class DbContextFactory : IDbContextFactory<BancoTabajaraDbContext>
    {
        public BancoTabajaraDbContext Create()
        {
            return new BancoTabajaraDbContext();
        }
    }
}
