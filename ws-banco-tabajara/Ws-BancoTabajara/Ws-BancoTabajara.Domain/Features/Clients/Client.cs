using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ws_BancoTabajara.Domain.Features.Clients
{
    public class Client : Entity
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(Name)) throw new ClientNullOrEmptyNameException();
            if (Name.Length > 50) throw new ClientNameOverflowException();
            if (string.IsNullOrEmpty(CPF)) throw new ClientNullOrEmptyCPFException();
            if (CPF.Length > 14) throw new ClientCPFOverflowException();
            if (string.IsNullOrEmpty(RG)) throw new ClientNullOrEmptyRGException();
            if (RG.Length > 12) throw new ClientRGOverflowException();
        }
    }
}