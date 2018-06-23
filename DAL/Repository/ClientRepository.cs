using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repository
{
    public class ClientRepository
    {
        private DBModelContainer _context;

        private Model.Client ToEntity(DAL.Models.Client client)
        {
            return new Model.Client() { FullName = client.FullName };
        }

        private DAL.Models.Client ToObject(Model.Client client)
        {
            return new DAL.Models.Client() { FullName = client.FullName };
        }

        public ClientRepository(DBModelContainer context)
        {
            _context = context;
        }

        public void Add(DAL.Models.Client client)
        {
            _context.Clients.Add(ToEntity(client));
        }

        public int? GetId(DAL.Models.Client client)
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

        public IEnumerable<DAL.Models.Client> GetAll()
        {
            return _context.Clients.Select(c => new DAL.Models.Client() { Id = c.Id, FullName = c.FullName }).ToArray();
        }

        public DAL.Models.Client GetById(int Id)
        {
            return ToObject(_context.Clients.FirstOrDefault(c => (c.Id == Id)));
        }

        public void Update(Models.Client item)
        {
            throw new NotImplementedException();
        }
    }
}
}
