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
                Date = saleInfo.Date,
                ManagerId = saleInfo.ManagerId,
                ClientId = saleInfo.ClientId,
                ProductId = saleInfo.ProductId,
                Amount = saleInfo.Amount
            };
        }

        private DAL.Models.SaleInfo ToObject(Model.SaleInfo saleInfo)
        {
            return new DAL.Models.SaleInfo()
            {
                Date = saleInfo.Date,
                ManagerId = saleInfo.ManagerId,
                ClientId = saleInfo.ClientId,
                ProductId = saleInfo.ProductId,
                Amount = saleInfo.Amount
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
                Date = s.Date,
                ManagerId = s.ManagerId,
                ClientId = s.ClientId,
                ProductId = s.ProductId,
                Amount = s.Amount
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
            sale.Date = item.Date;
            sale.ManagerId = item.ManagerId;
            sale.ClientId = item.ClientId;
            sale.ProductId = item.ProductId;
            sale.Amount = item.Amount;
        }
    }
}
