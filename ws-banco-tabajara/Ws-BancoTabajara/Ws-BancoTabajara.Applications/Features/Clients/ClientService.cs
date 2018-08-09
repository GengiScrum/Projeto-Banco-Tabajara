using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Applications.Features.Clients.Commands;
using Ws_BancoTabajara.Applications.Features.Clients.Queries;
using Ws_BancoTabajara.Applications.Features.Clients.ViewModels;
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

        public int Add(ClientRegisterCommand cmd)
        {
            var client = Mapper.Map<ClientRegisterCommand, Client>(cmd);

            var newClient = _clientRepository.Add(client);
            return newClient.Id;
        }

        public IQueryable<Client> GetAll(ClientQuery client)
        {
            return _clientRepository.GetAll(client.Quantity);
        }

        public ClientViewModel GetById(int clientId)
        {
            if (clientId == 0)
                throw new IdentifierUndefinedException();
            Client client = _clientRepository.GetById(clientId);
            return Mapper.Map<Client, ClientViewModel>(client);
        }

        public bool Remove(ClientRemoveCommand cmd)
        {
            return _clientRepository.Remove(cmd.Id);
        }

        public bool Update(ClientUpdateCommand cmd)
        {
            var modifiedClient = _clientRepository.GetById(cmd.Id);
            modifiedClient.Name = cmd.Name;
            modifiedClient.CPF = cmd.CPF;
            modifiedClient.RG = cmd.RG;

            return _clientRepository.Update(modifiedClient);
        }
    }
}
