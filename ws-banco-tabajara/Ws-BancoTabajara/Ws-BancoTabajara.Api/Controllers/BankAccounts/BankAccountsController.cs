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
using Ws_BancoTabajara.Applications.Features.Clients.ViewModels;
using Ws_BancoTabajara.Applications.Features.BankAccounts.Queries;
using Ws_BancoTabajara.Applications.Features.BankAccounts.ViewModels;
using Ws_BancoTabajara.Applications.Features.BankAccounts.Commands;

namespace Ws_BancoTabajara.Api.Controllers.BankAccounts
{
    [RoutePrefix("api/bankaccounts")]
    public class BankAccountsController : ApiControllerBase
    {
        public IBankAccountService _bankAccountsService;

        public BankAccountsController(IBankAccountService bankAccountsService) : base()
        {
            _bankAccountsService = bankAccountsService;
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            BankAccountQuery query = new BankAccountQuery() { Quantity = Request.GetQueryQuantityValueExtension() };
            return HandleQueryable<BankAccount, BankAccountViewModel>(_bankAccountsService.GetAll(query));
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            return HandleCallback<BankAccount, BankAccountViewModel>(() => _bankAccountsService.GetById(id));
        }

        [HttpGet]
        [Route("{id:int}/statement")]
        public IHttpActionResult GenerateBankStatement(int id)
        {
            return HandleCallback(() => _bankAccountsService.GenerateBankStatement(id));
        }

        [HttpPost]
        public IHttpActionResult Add(BankAccountRegisterCommand bankAccount)
        {
            var validate = bankAccount.Validate();
            if (!validate.IsValid)
                return HandleValidationFailure(validate.Errors);

            return HandleCallback(() => _bankAccountsService.Add(bankAccount));
        }

        [HttpPut]
        public IHttpActionResult Update(BankAccountUpdateCommand bankAccount)
        {
            return HandleCallback(() => _bankAccountsService.Update(bankAccount));
        }

        [HttpPatch]
        [Route("{id:int}/withdraw")]
        public IHttpActionResult Withdraw(BankAccountOperationCommand operation)
        {
            return HandleCallback(() => _bankAccountsService.Withdraw(operation));
        }

        [HttpPatch]
        [Route("{id:int}/deposit")]
        public IHttpActionResult Deposit(BankAccountOperationCommand operation)
        {
            return HandleCallback(() => _bankAccountsService.Deposit(operation));
        }

        [HttpPatch]
        [Route("{idOrigin:int}/{idReceiver:int}/transfer")]
        public IHttpActionResult Transfer(BankAccountTransferCommand transfer)
        {
            return HandleCallback(() => _bankAccountsService.Transfer(transfer));
        }

        [HttpPatch]
        [Route("{id:int}/changeactivation")]
        public IHttpActionResult ChangeActivation(int id)
        {
            return HandleCallback(() => _bankAccountsService.ChangeActivation(id));
        }

        [HttpDelete]
        public IHttpActionResult Remove(BankAccountRemoveCommand bankAccount)
        {
            return HandleCallback(() => _bankAccountsService.Remove(bankAccount));
        }
    }
}
