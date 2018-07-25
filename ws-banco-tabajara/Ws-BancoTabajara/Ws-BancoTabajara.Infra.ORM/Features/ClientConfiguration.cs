using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Domain.Features.Clients;

namespace Ws_BancoTabajara.Infra.ORM.Features
{
    public class ClientConfiguration : EntityTypeConfiguration<Client>
    {
        public ClientConfiguration()
        {
            ToTable("TBClient");

            this.HasKey(c => c.Id);

            this.Property(c => c.Name).HasMaxLength(50).IsRequired();
            this.Property(c => c.BirthDate).IsRequired();
            this.Property(c => c.CPF).HasMaxLength(14).IsRequired();
            this.Property(c => c.RG).HasMaxLength(12).IsRequired();
        }
    }
}
