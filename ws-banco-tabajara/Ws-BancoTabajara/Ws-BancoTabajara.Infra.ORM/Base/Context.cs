﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Domain.Features.Clients;

namespace Ws_BancoTabajara.Infra.ORM.Base
{
    public class Context : DbContext
    {
        public Context(string connection = "Name=GengiScrum_DBWS")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        protected Context(DbConnection connection) : base(connection, true) { }

        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
