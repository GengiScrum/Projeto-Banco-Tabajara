using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Domain.Exceptions;

namespace Ws_BancoTabajara.Domain.Features.BankAccounts
{
    public class BankAccountInvalidTransactionValueException : BusinessException
    {
        public BankAccountInvalidTransactionValueException() : base(ErrorCodes.BadRequest, "O valor deve ser positivo")
        {
        }
    }
}
