using DAL.Repository.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repository
{
    public class ProductRepository : IModelRepository<Models.Product>
    {
        private DBModelContainer _context;

        private Product ToEntity(Models.Product product)
        {
            return new Product() { Name = product.Name };
        }

        private Models.Product ToObject(Product product)
        {
            return new Models.Product() { Name = product.Name };
        }

        public ProductRepository(DBModelContainer context)
        {
            _context = context;
        }

        public void Add(Models.Product product)
        {
            _context.Products.Add(ToEntity(product));
        }

        public int? GetId(Models.Product product)
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

        public IEnumerable<Models.Product> GetAll()
        {
            return _context.Products.Select(p => new Models.Product() { Id = p.Id, Name = p.Name }).ToArray();
        }

        public Models.Product GetById(int Id)
        {
            return ToObject(_context.Products.FirstOrDefault(p => (p.Id == Id)));
        }

        public void Update(Models.Product item)
        {
            throw new NotImplementedException();
        }
    }
}
