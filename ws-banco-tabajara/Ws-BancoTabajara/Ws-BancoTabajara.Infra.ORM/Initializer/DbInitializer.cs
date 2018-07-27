using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Infra.ORM.Base;
using Ws_BancoTabajara.Infra.ORM.Migrations;

namespace Ws_BancoTabajara.Infra.ORM.Initializer
{
    class DbInitializer : MigrateDatabaseToLatestVersion<BancoTabajaraDbContext, Configuration>
    {
    }
}
