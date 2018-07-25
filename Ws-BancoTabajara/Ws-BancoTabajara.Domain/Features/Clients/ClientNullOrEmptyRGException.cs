using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Domain.Exceptions;

namespace Ws_BancoTabajara.Domain.Features.Clients
{
    public class ClientNullOrEmptyRGException : BusinessException
    {
        public ClientNullOrEmptyRGException() : base(ErrorCodes.InvalidObject, "O RG não pode estar vazio")
        {
        }
    }
}
