using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ws_BancoTabajara.Domain.Features.Clients
{
    public interface IClientService
    {
        IQueryable<Client> GetAll(int quantity);
        int Add(Client client);
        bool Update(Client client);
        Client GetById(int clientId);
        bool Remove(Client client);
    }
}
