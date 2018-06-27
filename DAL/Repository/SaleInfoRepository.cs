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
                ManagerId = Convert.ToString(saleInfo.ManagerId),
                ClientId = Convert.ToString(saleInfo.ClientId),
                ProductId = Convert.ToString(saleInfo.ProductId),
                Amount = Convert.ToString(saleInfo.Amount)
            };
        }

        private DAL.Models.SaleInfo ToObject(Model.SaleInfo saleInfo)
        {
            return new DAL.Models.SaleInfo()
            {
                Date = Convert.ToDateTime(saleInfo.Date),
                ManagerId = Convert.ToInt32( saleInfo.ManagerId),
                ClientId = Convert.ToInt32( saleInfo.ClientId),
                ProductId = Convert.ToInt32( saleInfo.ProductId),
                Amount = Convert.ToDouble( saleInfo.Amount)
            };
        }

        public SaleInfoRepository(DBModelContainer context)
        {
            _context = context;
        }

        public void Add(DAL.Models.SaleInfo salesInfo)
        {
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
                ManagerId = Convert.ToInt32(s.ManagerId),
                ClientId = Convert.ToInt32(s.ClientId),
                ProductId = Convert.ToInt32(s.ProductId),
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
            sale.ManagerId = Convert.ToString(item.ManagerId);
            sale.ClientId = Convert.ToString(item.ClientId);
            sale.ProductId = Convert.ToString(item.ProductId);
            sale.Amount = Convert.ToString(item.Amount);
        }
    }
}
