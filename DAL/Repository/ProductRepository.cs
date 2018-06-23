using DAL.Repository.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repository
{
    public class ProductRepository : IModelRepository<DAL.Models.Product>
    {
        private DBModelContainer _context;

        private Model.Product ToEntity(DAL.Models.Product product)
        {
            return new Model.Product() { Name = product.Name };
        }

        private DAL.Models.Product ToObject(Model.Product product)
        {
            return new DAL.Models.Product() { Name = product.Name };
        }

        public ProductRepository(DBModelContainer context)
        {
            _context = context;
        }

        public void Add(DAL.Models.Product product)
        {
            _context.Products.Add(ToEntity(product));
        }

        public int? GetId(DAL.Models.Product product)
        {
            var tmp = _context.Products.FirstOrDefault(p => (p.Name == product.Name));
            if (tmp == null)
            {
                return null;
            }
            else
            {
                return tmp.Id;
            }
        }

        public IEnumerable<DAL.Models.Product> GetAll()
        {
            return _context.Products.Select(p => new DAL.Models.Product() { Id = p.Id, Name = p.Name }).ToArray();
        }

        public DAL.Models.Product GetById(int Id)
        {
            return ToObject(_context.Products.FirstOrDefault(p => (p.Id == Id)));
        }

        public void Update(Models.Product item)
        {
            throw new NotImplementedException();
        }
    }
}
