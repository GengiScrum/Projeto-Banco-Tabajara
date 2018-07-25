using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Domain.Exceptions;

namespace Ws_BancoTabajara.Domain.Features.Transactions
{
    public class TransactionInvalidValueException : BusinessException
    {
        public TransactionInvalidValueException() : base(ErrorCodes.InvalidObject, "O valor não pode ser zero")
        {
        }
    }
}
