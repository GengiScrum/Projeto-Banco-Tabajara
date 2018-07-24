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
            throw new NotImplementedException();
        }
    }
}