using DAL.Repository.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DAL.Repository
{
    public class SaleInfoRepository : IModelRepository<DAL.Models.SaleInfo>
    {
        private DBModelContainer _context;

        private Model.SaleInfo ToEntity(DAL.Models.SaleInfo saleInfo)
        {
            return new Model.SaleInfo()
            {
                Date = Convert.ToString(saleInfo.Date),
                Manager = _context.Managers.FirstOrDefault(x=>saleInfo.ManagerId == x.Id), // new Manager() { Id = saleInfo.ManagerId }, // Convert.ToString(saleInfo.ManagerId),
                Client =_context.Clients.FirstOrDefault(x=>saleInfo.ClientId == x.Id),
                Product = _context.Products.FirstOrDefault(x=>saleInfo.ProductId ==x.Id),
                Amount = Convert.ToString(saleInfo.Amount)
            };
        }

        private DAL.Models.SaleInfo ToObject(Model.SaleInfo saleInfo)
        {
            return new DAL.Models.SaleInfo()
            {
                Date = Convert.ToDateTime(saleInfo.Date),
                ManagerId = Convert.ToInt32( saleInfo.Manager.Id),
                ClientId = Convert.ToInt32( saleInfo.Client.Id),
                ProductId = Convert.ToInt32( saleInfo.Product.Id),
                Amount = Convert.ToDouble( saleInfo.Amount)
            };
        }

        public SaleInfoRepository(DBModelContainer context)
        {
            _context = context;
        }

        public void Add(DAL.Models.SaleInfo salesInfo)
        {
            //var obj = ToEntity(salesInfo);            
            //_context.SalesInfo.Add(obj);
            _context.SalesInfo.Add(ToEntity(salesInfo));
        }

        public int? GetId(DAL.Models.SaleInfo saleInfo)
        {
            var tmp = _context.SalesInfo.FirstOrDefault(s => (s.Id == saleInfo.Id));
            if (tmp == null)
            {
                return null;
            }
            else
            {
                return tmp.Id;
            }
        }

        public IEnumerable<DAL.Models.SaleInfo> GetAll()
        {
            return _context.SalesInfo.Select(s => new DAL.Models.SaleInfo()
            {
                Id = s.Id,
                Date = Convert.ToDateTime(s.Date),
                ManagerId = Convert.ToInt32(s.Manager.Id),
                ClientId = Convert.ToInt32(s.Client.Id),
                ProductId = Convert.ToInt32(s.Product.Id),
                Amount = Convert.ToDouble( s.Amount)
            }
                                            )
                                     .ToArray();
        }

        public DAL.Models.SaleInfo GetById(int Id)
        {
            return ToObject(_context.SalesInfo.FirstOrDefault(s => (s.Id == Id)));
        }

        public void Update(Models.SaleInfo item)
        {
            var sale = _context.SalesInfo.FirstOrDefault(s => (s.Id == item.Id));
            sale.Date = Convert.ToString(item.Date);
            sale.Manager = new Manager() { Id = item.ManagerId };
            sale.Client = new Client() {Id = item.ClientId };
            sale.Product = new Product() {Id = item.ProductId };
            sale.Amount = Convert.ToString(item.Amount);
        }
    }
}
