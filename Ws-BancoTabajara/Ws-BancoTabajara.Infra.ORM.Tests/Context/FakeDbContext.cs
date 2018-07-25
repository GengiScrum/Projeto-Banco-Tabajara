using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Infra.ORM.Base;

namespace Ws_BancoTabajara.Infra.ORM.Tests.Context
{
    public class FakeDbContext : BancoTabajaraDbContext
    {
        public FakeDbContext(DbConnection connection) : base(connection)
        {

        }
    }
}
