namespace Ws_BancoTabajara.Infra.ORM.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    [ExcludeFromCodeCoverage]
    public class Configuration : DbMigrationsConfiguration<Ws_BancoTabajara.Infra.ORM.Base.BancoTabajaraDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Ws_BancoTabajara.Infra.ORM.Base.BancoTabajaraDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
