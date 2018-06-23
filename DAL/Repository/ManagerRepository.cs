using DAL.Repository.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
namespace DAL.Repository
{
    public class ManagerRepository : IModelRepository<DAL.Models.Manager>
    {
        private DBModelContainer _context;

        public Model.Manager ToEntity(DAL.Models.Manager manager)
        {
            return new Model.Manager() { SecondName = manager.SecondName };
        }

        private DAL.Models.Manager ToObject(Model.Manager manager)
        {
            return new DAL.Models.Manager() { SecondName = manager.SecondName };
        }

        public ManagerRepository(DBModelContainer context)
        {
            _context = context;
        }

        public void Add(DAL.Models.Manager manager)
        {
            _context.Managers.Add(ToEntity(manager));
        }

        public int? GetId(DAL.Models.Manager manager)
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

        public IEnumerable<DAL.Models.Manager> GetAll()
        {
            return _context.Managers.Select(m => new DAL.Models.Manager() { Id = m.Id, SecondName = m.SecondName }).ToArray();
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
