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

        public int Add(Client client)
        {
            client.Validate();

            var newClient = _clientRepository.Add(client);
            return newClient.Id;
        }

        public IQueryable<Client> GetAll(int quantity)
        {
            return _clientRepository.GetAll(quantity);
        }

        public Client GetById(int clientId)
        {
            if (clientId == 0)
                throw new IdentifierUndefinedException();

            return _clientRepository.GetById(clientId);
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

            var alteredClient = _clientRepository.GetById(client.Id);
            alteredClient.Name = client.Name;
            alteredClient.CPF = client.CPF;
            alteredClient.RG = client.RG;

            return _clientRepository.Update(alteredClient);
        }
    }
}
