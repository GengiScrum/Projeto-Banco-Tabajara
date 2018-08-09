using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ws_BancoTabajara.Api.Controllers.Common;
using Ws_BancoTabajara.Api.Extensions;
using Ws_BancoTabajara.Applications.Features.Clients;
using Ws_BancoTabajara.Applications.Features.Clients.Commands;
using Ws_BancoTabajara.Applications.Features.Clients.Queries;
using Ws_BancoTabajara.Applications.Features.Clients.ViewModels;
using Ws_BancoTabajara.Domain.Features.Clients;
using Ws_BancoTabajara.Infra.ORM.Base;
using Ws_BancoTabajara.Infra.ORM.Features.Clients;

namespace Ws_BancoTabajara.Api.Controllers.Clients
{
    [RoutePrefix("api/clients")]
    public class ClientsController : ApiControllerBase
    {
        public IClientService _clientService;

        public ClientsController(IClientService clientService) : base()
        {
            _clientService = clientService;
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            int quantity = Request.GetQueryQuantityValueExtension();
            var query = new ClientQuery { Quantity = quantity };
            return HandleQueryable1<Client, ClientViewModel>(_clientService.GetAll(query));
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            return HandleCallback(() => _clientService.GetById(id));
        }

        [HttpPost]
        public IHttpActionResult Add(ClientRegisterCommand client)
        {
            var validate = client.Validate();
            if (!validate.IsValid)
                return HandleValidationFailure(validate.Errors);

            return HandleCallback(() => _clientService.Add(client));
        }

        [HttpPut]
        public IHttpActionResult Update(ClientUpdateCommand client)
        {
            var validate = client.Validate();
            if (!validate.IsValid)
                return HandleValidationFailure(validate.Errors);

            return HandleCallback(() => _clientService.Update(client));
        }

        [HttpDelete]
        public IHttpActionResult Remove(ClientRemoveCommand client)
        {
            var validate = client.Validate();
            if (!validate.IsValid)
                return HandleValidationFailure(validate.Errors);

            return HandleCallback(() => _clientService.Remove(client));
        }
    }
}
