using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Domain.Exceptions;
using Ws_BancoTabajara.Domain.Features.Clients;
using Ws_BancoTabajara.Infra.ORM.Base;

namespace Ws_BancoTabajara.Infra.ORM.Features.Clients
{
    public class ClientRepository : IClientRepository
    {
        private BancoTabajaraDbContext _context;

        public ClientRepository(BancoTabajaraDbContext context)
        {
            _context = context;
        }

        public Client Add(Client client)
        {
            client.Validate();
            var newClient = _context.Clients.Add(client);
            _context.SaveChanges();
            return newClient;
        }

        public IQueryable<Client> GetAll()
        {
            return _context.Clients;
        }

        public Client GetById(int clientId)
        {
            if (clientId == 0)
                throw new IdentifierUndefinedException();

            var clientFound = _context.Clients.Where(c => c.Id == clientId).FirstOrDefault();

            if (clientFound == null)
                throw new NotFoundException();

            return clientFound;
        }

        public bool Remove(int clientId)
        {
            if (clientId == 0)
                throw new IdentifierUndefinedException();
            var client = GetById(clientId);
            if (client == null)
                throw new NotFoundException();
            _context.Clients.Remove(client);
            return _context.SaveChanges() > 0;
        }

        public bool Update(Client client)
        {
            if (client.Id == 0)
                throw new IdentifierUndefinedException();
            client.Validate();
            var oldClient = GetById(client.Id);
            if (oldClient == null)
                throw new NotFoundException();
            oldClient = client;
            return _context.SaveChanges() > 0;
        }
    }
}
