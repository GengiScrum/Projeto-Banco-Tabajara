using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ws_BancoTabajara.Domain.Exceptions
{
    public class NotFoundEsception : BusinessException
    {
        public NotFoundException() : base(ErrorCodes.NotFound, "Registry not found") { }
    }
}
