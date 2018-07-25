using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Domain.Exceptions;

namespace Ws_BancoTabajara.Domain.Features.Transactions
{
    public class TransactionNullBankAccount : BusinessException
    {
        public TransactionNullBankAccount() : base(ErrorCodes.InvalidObject, "A conta bancaria não pode ser nula")
        {
        }
    }
}
