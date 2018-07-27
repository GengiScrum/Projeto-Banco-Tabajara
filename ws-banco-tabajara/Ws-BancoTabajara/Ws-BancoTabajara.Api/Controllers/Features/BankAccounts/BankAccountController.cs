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
using Ws_BancoTabajara.Infra.ORM.Features.Transactions;

namespace Ws_BancoTabajara.Api.Controllers.Features.BankAccounts
{
    [RoutePrefix("api/bankaccounts")]
    public class BankAccountController : ApiControllerBase
    {
        public IBankAccountService _bankAccountsService;

        public BankAccountController() : base()
        {
            var context = new BancoTabajaraDbContext();
            var bankAccountRepository = new BankAccountRepository(context);
            var transactionRepository = new TransactionRepository(context);
            _bankAccountsService = new BankAccountService(bankAccountRepository, transactionRepository);
        }

        [HttpGet]
        public IHttpActionResult Get()
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
        public IHttpActionResult Post(BankAccount bankAccount)
        {
            return HandleCallback(() => _bankAccountsService.Add(bankAccount));
        }

        [HttpPut]
        public IHttpActionResult Update(BankAccount bankAccount)
        {
            return HandleCallback(() => _bankAccountsService.Update(bankAccount));
        }

        [HttpPut]
        [Route("{id:int}/{-value:double}")]
        public IHttpActionResult Withdraw(int id, double value)
        {
            BankAccount bankAccount = _bankAccountsService.GetById(id);
            return HandleCallback(() => _bankAccountsService.Withdraw(bankAccount, value));
        }

        [HttpDelete]
        public IHttpActionResult Delete(BankAccount bankAccount)
        {
            return HandleCallback(() => _bankAccountsService.Remove(bankAccount));
        }
    }
}
