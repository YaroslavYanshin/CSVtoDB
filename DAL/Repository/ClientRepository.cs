using System;
using System.Collections.Generic;
using System.Linq;
using Model;

namespace DAL.Repository.Interfaces
{
    public class ClientRepository : IModelRepository<Models.Client>
    {
        private DBModelContainer _context;

        private Client ToEntity(Models.Client client)
        {
            return new Client() { FullName = client.FullName };
        }

        private Models.Client ToObject(Client client)
        {
            return new Models.Client() { FullName = client.FullName };
        }

        public ClientRepository(DBModelContainer context)
        {
            _context = context;
        }

        public void Add(Models.Client client)
        {
            _context.Clients.Add(ToEntity(client));
        }

        public int? GetId(Models.Client client)
        {
            var tmp = _context.Clients.FirstOrDefault(c => (c.FullName == client.FullName));
            if (tmp == null)
            {
                return null;
            }
            else
            {
                return tmp.Id;
            }
        }

        public IEnumerable<Models.Client> GetAll()
        {
            return _context.Clients.Select(c => new Models.Client() { Id = c.Id, FullName = c.FullName }).ToArray();
        }

        public Models.Client GetById(int Id)
        {
            return ToObject(_context.Clients.FirstOrDefault(c => (c.Id == Id)));
        }

        public void Update(Models.Client item)
        {
            throw new NotImplementedException();
        }
    }
}
