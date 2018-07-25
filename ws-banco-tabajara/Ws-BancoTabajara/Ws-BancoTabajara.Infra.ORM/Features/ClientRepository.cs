using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Domain.Exceptions;
using Ws_BancoTabajara.Domain.Features.Clients;
using Ws_BancoTabajara.Infra.ORM.Base;

namespace Ws_BancoTabajara.Infra.ORM.Features
{
    public class ClientRepository : IClientRepository
    {
        private Context _context;

        public ClientRepository(Context context)
        {
            _context = context;
        }

        public Client Add(Client client)
        {
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
            return _context.Clients.Where(c => c.Id == clientId).FirstOrDefault();
        }

        public bool Remove(int clientId)
        {
            var client = _context.Clients.Where(c => c.Id == clientId).FirstOrDefault();
            if (client == null)
                throw new NotFoundEsception();
            _context.Clients.Remove(client);
            return _context.SaveChanges() > 0;
        }

        public bool Update(Client client)
        {
            var alteredClient = _context.Clients.Where(c => c.Id == client.Id).FirstOrDefault();
            alteredClient = client;
            return _context.SaveChanges() > 0;
        }
    }
}
