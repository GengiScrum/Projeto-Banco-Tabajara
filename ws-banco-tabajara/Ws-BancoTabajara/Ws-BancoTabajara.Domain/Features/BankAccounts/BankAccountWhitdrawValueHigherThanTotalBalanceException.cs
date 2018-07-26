using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Domain.Exceptions;

namespace Ws_BancoTabajara.Domain.Features.BankAccounts
{
    public class BankAccountWhitdrawValueHigherThanTotalBalanceException : BusinessException
    {
        public BankAccountWhitdrawValueHigherThanTotalBalanceException() : base(ErrorCodes.BadRequest, "O valor a ser sacado é maior que o saldo total.")
        {
        }
    }
}
