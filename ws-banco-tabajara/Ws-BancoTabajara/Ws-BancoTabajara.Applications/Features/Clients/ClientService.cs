using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Domain.Exceptions;
using Ws_BancoTabajara.Domain.Features.Clients;

namespace Ws_BancoTabajara.Applications.Features.Clients
{
    public class ClientService : IClientService
    {
        IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public Client Add(Client client)
        {
            client.Validate();

            return _clientRepository.Add(client);
        }

        public IQueryable<Client> GetAll()
        {
            return _clientRepository.GetAll();
        }

        public Client GetById(Client client)
        {
            if (client.Id == 0)
                throw new IdentifierUndefinedException();

            return _clientRepository.GetById(client.Id);
        }

        public bool Remove(Client client)
        {
            if (client.Id == 0)
                throw new IdentifierUndefinedException();

            return _clientRepository.Remove(client.Id);
        }

        public bool Update(Client client)
        {
            client.Validate();

            if (client.Id == 0)
                throw new IdentifierUndefinedException();

            return _clientRepository.Update(client);
        }
    }
}
