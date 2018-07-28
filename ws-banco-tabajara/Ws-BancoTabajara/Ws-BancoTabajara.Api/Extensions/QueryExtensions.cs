using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using Ws_BancoTabajara.Domain.Features.BankAccounts;

namespace Ws_BancoTabajara.Api.Extensions
{
    public static class QueryExtensions
    {
        static HttpRequestMessage Request { get; set; }

        public static int GetQueryQuantityValueExtension(this HttpRequestMessage request)
        {
            int quantity = Convert.ToInt32(request.GetQueryNameValuePairs()
                .Where(x => x.Key.Equals("quantity"))
                .FirstOrDefault().Value);
            return quantity;
        }
}