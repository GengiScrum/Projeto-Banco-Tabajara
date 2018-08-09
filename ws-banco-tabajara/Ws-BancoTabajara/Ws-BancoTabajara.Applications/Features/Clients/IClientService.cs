using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Applications.Features.Clients.Commands;
using Ws_BancoTabajara.Applications.Features.Clients.Queries;
using Ws_BancoTabajara.Applications.Features.Clients.ViewModels;
using Ws_BancoTabajara.Domain.Features.Clients;

namespace Ws_BancoTabajara.Applications.Features.Clients
{
    public interface IClientService
    {
        IQueryable<Client> GetAll(ClientQuery quantity);
        int Add(ClientRegisterCommand client);
        bool Update(ClientUpdateCommand client);
        ClientViewModel GetById(int clientId);
        bool Remove(ClientRemoveCommand client);
    }
}
