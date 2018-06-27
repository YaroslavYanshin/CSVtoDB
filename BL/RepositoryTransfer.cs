using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repository;
using DAL.Repository.Interfaces;
using BL.DTO;


namespace BL
{
    public class RepositoryTransfer : IRepositoryTransfer
    {
        private IUnitOfWork _repositories;
        private object _locker = new object();

        public RepositoryTransfer()
        {
            _repositories = new EFUnitOfWork();
        }

        public void AddSaleInfo(SaleDTO saleDto)
        {
            lock (_locker)
            {
                var manager = new DAL.Models.Manager { SecondName = saleDto.Manager };
                var client = new DAL.Models.Client { FullName = saleDto.Client };
                var product = new DAL.Models.Product { Name = saleDto.Product };

                var managerId = _repositories.Managers.GetId(manager);
                if (managerId == null)
                {
                    _repositories.Managers.Add(manager);
                    _repositories.Save();
                    managerId = _repositories.Managers.GetId(manager);
                }

                var clientId = _repositories.Clients.GetId(client);
                if (clientId == null)
                {
                    _repositories.Clients.Add(client);
                    _repositories.Save();
                    clientId = _repositories.Clients.GetId(client);
                }

                var productId = _repositories.Products.GetId(product);
                if (productId == null)
                {
                    _repositories.Products.Add(product);
                    _repositories.Save();
                    productId = _repositories.Products.GetId(product);
                }

                var saleInfo = new DAL.Models.SaleInfo()
                {
                    Date = saleDto.Date,
                    ManagerId = (int)managerId,
                    ClientId = (int)clientId,
                    ProductId = (int)productId,
                    Amount = saleDto.Amount
                };

                _repositories.SalesInfo.Add(saleInfo);
                _repositories.Save();

            }
        }

        public IEnumerable<SaleDTO> GetSales()
        {
            var salesDTO = _repositories.SalesInfo.GetAll().Select(s => new SaleDTO()
            {
                Id = s.Id,
                Date = s.Date,
                Manager = _repositories.Managers.GetById(s.ManagerId).SecondName,
                Client = _repositories.Clients.GetById(s.ClientId).FullName,
                Product = _repositories.Products.GetById(s.ProductId).Name,
                Amount = s.Amount
            });
            return salesDTO.ToArray();
        }

        public IEnumerable<ManagerDTO> GetManagers()
        {
            var managerDTO = _repositories.Managers.GetAll().Select(m => new ManagerDTO() { Id = m.Id, SecondName = m.SecondName });
            return managerDTO.ToArray();
        }

        public IEnumerable<ClientDTO> GetClients()
        {
            var clientDTO = _repositories.Clients.GetAll().Select(c => new ClientDTO() { Id = c.Id, FullName = c.FullName });
            return clientDTO.ToArray();
        }

        public IEnumerable<ProductDTO> GetProducts()
        {
            var productDTO = _repositories.Products.GetAll().Select(p => new ProductDTO() { Id = p.Id, Name = p.Name });
            return productDTO.ToArray();
        }

        public void UpdateManager(ManagerDTO managerDTO)
        {
            var manager = _repositories.Managers.GetAll().FirstOrDefault(m => (m.Id == managerDTO.Id));
            manager.SecondName = managerDTO.SecondName;
            _repositories.Managers.Update(manager);
            _repositories.Save();
        }

        public void UpdateSaleInfo(SaleDTO saleDto)
        {
            var manager = new DAL.Models.Manager() { SecondName = saleDto.Manager };
            var client = new DAL.Models.Client() { FullName = saleDto.Client };
            var product = new DAL.Models.Product() { Name = saleDto.Product };

            var managerId = _repositories.Managers.GetId(manager);
            if (managerId == null)
            {
                _repositories.Managers.Add(manager);
                _repositories.Save();
                managerId = _repositories.Managers.GetId(manager);
            }

            var clientId = _repositories.Clients.GetId(client);
            if (clientId == null)
            {
                _repositories.Clients.Add(client);
                _repositories.Save();
                clientId = _repositories.Clients.GetId(client);
            }

            var productId = _repositories.Products.GetId(product);
            if (productId == null)
            {
                _repositories.Products.Add(product);
                _repositories.Save();
                productId = _repositories.Products.GetId(product);
            }

            var saleInfo = new DAL.Models.SaleInfo()
            {
                Date = saleDto.Date,
                ManagerId = (int)managerId,
                ClientId = (int)clientId,
                ProductId = (int)productId,
                Amount = saleDto.Amount
            };

            var sale = _repositories.SalesInfo.GetAll().FirstOrDefault(s => (s.Id == saleDto.Id));
            sale.Date = saleDto.Date;
            sale.ManagerId = (int)managerId;
            sale.ClientId = (int)clientId;
            sale.ProductId = (int)productId;
            sale.Amount = saleDto.Amount;

            _repositories.SalesInfo.Update(sale);
            _repositories.Save();
        }
    }
}
