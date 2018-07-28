using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;
using Ws_BancoTabajara.Api.Controllers.Common;
using Ws_BancoTabajara.Applications.Features.BankAccounts;
using Ws_BancoTabajara.Domain.Features.BankAccounts;
using Ws_BancoTabajara.Infra.ORM.Base;
using Ws_BancoTabajara.Infra.ORM.Features.BankAccounts;
using Ws_BancoTabajara.Infra.ORM.Features.Clients;
using Ws_BancoTabajara.Infra.ORM.Features.Transactions;
using System.Web.UI.WebControls;
using Ws_BancoTabajara.Api.Extensions;

namespace Ws_BancoTabajara.Api.Controllers.BankAccounts
{
    [RoutePrefix("api/bankaccounts")]
    public class BankAccountsController : ApiControllerBase
    {
        public IBankAccountService _bankAccountsService;

        public BankAccountsController() : base()
        {
            var context = new BancoTabajaraDbContext();
            var bankAccountRepository = new BankAccountRepository(context);
            var transactionRepository = new TransactionRepository(context);
            var clientRepository = new ClientRepository(context);
            _bankAccountsService = new BankAccountService(bankAccountRepository, transactionRepository, clientRepository);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var quantity = Request.GetQueryQuantityValueExtension();
            return HandleQueryable<BankAccount>(_bankAccountsService.GetAll(quantity));
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            return HandleCallback(() => _bankAccountsService.GetById(id));
        }

        [HttpGet]
        [Route("{id:int}/statement")]
        public IHttpActionResult GenerateBankStatement(int id)
        {
            return HandleCallback(() => _bankAccountsService.GenerateBankStatement(id));
        }

        [HttpPost]
        public IHttpActionResult Add(BankAccount bankAccount)
        {
            return HandleCallback(() => _bankAccountsService.Add(bankAccount));
        }

        [HttpPut]
        public IHttpActionResult Update(BankAccount bankAccount)
        {
            return HandleCallback(() => _bankAccountsService.Update(bankAccount));
        }

        [HttpPatch]
        [Route("{id:int}/withdraw")]
        public IHttpActionResult Withdraw(int id, [FromBody] double value)
        {
            return HandleCallback(() => _bankAccountsService.Withdraw(id, value));
        }

        [HttpPatch]
        [Route("{id:int}/deposit")]
        public IHttpActionResult Deposit(int id, [FromBody] double value)
        {
            return HandleCallback(() => _bankAccountsService.Deposit(id, value));
        }

        [HttpPatch]
        [Route("{idOrigin:int}/{idReceiver:int}/transfer")]
        public IHttpActionResult Transfer(int idOrigin, int idReceiver, [FromBody] double value)
        {
            return HandleCallback(() => _bankAccountsService.Transfer(idOrigin, idReceiver, value));
        }

        [HttpDelete]
        public IHttpActionResult Remove(BankAccount bankAccount)
        {
            return HandleCallback(() => _bankAccountsService.Remove(bankAccount));
        }
    }
}
