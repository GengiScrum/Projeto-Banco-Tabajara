using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Domain.Exceptions;

namespace Ws_BancoTabajara.Domain.Features.BankAccounts
{
    public class BankAccountInvalidNumberException : BusinessException
    {
        public BankAccountInvalidNumberException() : base(ErrorCodes.InvalidObject, "Numero da conta nao pode ser menor que ou igual a 0")
        {
        }
    }
}
