using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Domain.Exceptions;

namespace Ws_BancoTabajara.Domain.Features.BankAccounts
{
    public class BankAccountWithoutClientException : BusinessException
    {
        public BankAccountWithoutClientException() : base(ErrorCodes.InvalidObject, "A conta precisa ter um Cliente")
        {
        }
    }
}
