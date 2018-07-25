using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Domain.Exceptions;

namespace Ws_BancoTabajara.Domain.Features.Clients
{
    public class ClientNullOrEmptyCPFException : BusinessException
    {
        public ClientNullOrEmptyCPFException() : base(ErrorCodes.InvalidObject, "O CPF não pode ser nulo")
        {
        }
    }
}
