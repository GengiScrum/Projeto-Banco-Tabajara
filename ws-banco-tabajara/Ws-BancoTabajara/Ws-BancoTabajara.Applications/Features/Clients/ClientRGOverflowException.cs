using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Domain.Exceptions;

namespace Ws_BancoTabajara.Domain.Features.Clients
{
    public class ClientRGOverflowException : BusinessException
    {
        public ClientRGOverflowException() : base(ErrorCodes.InvalidObject, "O RG não pode ter mais de 12 caracteres.")
        {
        }
    }
}
