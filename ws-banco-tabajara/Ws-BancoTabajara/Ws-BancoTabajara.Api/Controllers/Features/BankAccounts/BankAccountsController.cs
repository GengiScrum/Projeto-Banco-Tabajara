﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Ws_BancoTabajara.Api.Controllers.Common;
using Ws_BancoTabajara.Applications.Features.BankAccounts;
using Ws_BancoTabajara.Domain.Features.BankAccounts;
using Ws_BancoTabajara.Infra.ORM.Base;
using Ws_BancoTabajara.Infra.ORM.Features.BankAccounts;
using Ws_BancoTabajara.Infra.ORM.Features.Clients;
using Ws_BancoTabajara.Infra.ORM.Features.Transactions;

namespace Ws_BancoTabajara.Api.Controllers.Features.BankAccounts
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
            var query = _bankAccountsService.GetAll();
            return HandleQueryable<BankAccount>(query);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            return HandleCallback(() => _bankAccountsService.GetById(id));
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

        [HttpPut]
        [Route("withdraw/{id:int}/{value:double}")]
        public IHttpActionResult Withdraw(int id, double value)
        {
            BankAccount bankAccount = _bankAccountsService.GetById(id);
            return HandleCallback(() => _bankAccountsService.Withdraw(bankAccount, value));
        }

        [HttpPut]
        [Route("deposit/{id:int}/{value:double}")]
        public IHttpActionResult Deposit(int id, double value)
        {
            BankAccount bankAccount = _bankAccountsService.GetById(id);
            return HandleCallback(() => _bankAccountsService.Deposit(bankAccount, value));
        }

        [HttpPut]
        [Route("transfer/{idOrigin:int}/{value:double}/{idReceiver:int}")]
        public IHttpActionResult Transfer(int idOrigin, double value, int idReceiver)
        {
            BankAccount originBankAccount = _bankAccountsService.GetById(idOrigin);
            BankAccount receiverBankAccount = _bankAccountsService.GetById(idReceiver);
            return HandleCallback(() => _bankAccountsService.Transfer(originBankAccount, receiverBankAccount, value));
        }


        [HttpDelete]
        public IHttpActionResult Remove(BankAccount bankAccount)
        {
            return HandleCallback(() => _bankAccountsService.Remove(bankAccount));
        }
    }
}
