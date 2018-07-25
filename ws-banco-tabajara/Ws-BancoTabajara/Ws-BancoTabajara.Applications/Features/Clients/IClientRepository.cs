using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ws_BancoTabajara.Domain.Features.Clients
{
    public interface IClientRepository
    {
        IQueryable<Client> GetAll();
        Client Add(Client client);
        bool Update(Client client);
        Client GetById(int clientId);
        bool Remove(int clientId);
    }
}
