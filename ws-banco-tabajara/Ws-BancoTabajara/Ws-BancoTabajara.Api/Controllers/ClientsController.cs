using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ws_BancoTabajara.Api.Controllers.Common;
using Ws_BancoTabajara.Api.Extensions;
using Ws_BancoTabajara.Applications.Features.Clients;
using Ws_BancoTabajara.Domain.Features.Clients;
using Ws_BancoTabajara.Infra.ORM.Base;
using Ws_BancoTabajara.Infra.ORM.Features.Clients;

namespace Ws_BancoTabajara.Api.Controllers.Clients
{
    [RoutePrefix("api/clients")]
    public class ClientsController : ApiControllerBase
    {
        public IClientService _clientService;

        public ClientsController()
        {
            var context = new BancoTabajaraDbContext();
            var repository = new ClientRepository(context);
            _clientService = new ClientService(repository);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            int quantity = Request.GetQueryValueExtension();
            return HandleQueryable<Client>(_clientService.GetAll(quantity));
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            return HandleCallback(() => _clientService.GetById(id));
        }

        [HttpPost]
        public IHttpActionResult Add(Client client)
        {
            return HandleCallback(() => _clientService.Add(client));
        }

        [HttpPut]
        public IHttpActionResult Update(Client client)
        {
            return HandleCallback(() => _clientService.Update(client));
        }

        [HttpDelete]
        public IHttpActionResult Remove(Client client)
        {
            return HandleCallback(() => _clientService.Remove(client));
        }
    }
}
