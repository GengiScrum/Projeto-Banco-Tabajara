using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Domain.Exceptions;

namespace Ws_BancoTabajara.Domain.Features.BankAccounts
{
    public class BankAccountTransferToSameBankAccountException : BusinessException
    {
        public BankAccountTransferToSameBankAccountException() : base(ErrorCodes.BadRequest, "Não pode transferir para a mesma conta")
        {
        }
    }
}
