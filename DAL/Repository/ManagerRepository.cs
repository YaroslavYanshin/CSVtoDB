using DAL.Repository.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repository
{
    public class ManagerRepository : IModelRepository<Models.Manager>
    {
        private DBModelContainer _context;

        public Manager ToEntity(DAL.Models.Manager manager)
        {
            return new Manager() { SecondName = manager.SecondName };
        }

        private Models.Manager ToObject(Manager manager)
        {
            return new Models.Manager() { SecondName = manager.SecondName };
        }

        public ManagerRepository(DBModelContainer context)
        {
            _context = context;
        }

        public void Add(Models.Manager manager)
        {
            _context.Managers.Add(ToEntity(manager));
        }

        public int? GetId(Models.Manager manager)
        {
            var tmp = _context.Managers.FirstOrDefault(m => (m.SecondName == manager.SecondName));
            if (tmp == null)
            {
                return null;
            }
            else
            {
                return tmp.Id;
            }
        }

        public IEnumerable<Models.Manager> GetAll()
        {
            return _context.Managers.Select(m => new Models.Manager() { Id = m.Id, SecondName = m.SecondName }).ToArray();
        }

        public DAL.Models.Manager GetById(int Id)
        {
            return ToObject(_context.Managers.FirstOrDefault(m => (m.Id == Id)));
        }

        public void Update(Models.Manager item)
        {
            var manager = _context.Managers.FirstOrDefault(m => (m.Id == item.Id));
            manager.SecondName = item.SecondName;
        }
    }
}
