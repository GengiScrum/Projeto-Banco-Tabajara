using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Domain.Exceptions;

namespace Ws_BancoTabajara.Domain.Features.Clients
{
    public class ClientNullOrEmptyNameException : BusinessException
    {
        public ClientNullOrEmptyNameException() : base(ErrorCodes.InvalidObject, "O nome não pode estar vazio")
        {
        }
    }
}
