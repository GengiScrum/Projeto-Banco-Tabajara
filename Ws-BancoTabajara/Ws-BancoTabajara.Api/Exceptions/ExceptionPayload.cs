using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ws_BancoTabajara.Domain.Exceptions;

namespace Ws_BancoTabajara.Api.Exceptions
{
    public class ExceptionPayload
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public static ExceptionPayload New<T>(T exception) where T : Exception
        {
            int errorCode;
            if (exception is BusinessException)
                errorCode = (exception as BusinessException).ErrorCode.GetHashCode();
            else
                errorCode = ErrorCodes.Unhandled.GetHashCode();
            return new ExceptionPayload
            {
                ErrorCode = errorCode,
                ErrorMessage = exception.Message,
            };
        }
    }
}