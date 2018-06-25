using DAL.Repository.Interfaces;
using Model;
using System;
using DAL.Models;

namespace DAL.Repository
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private DBModelContainer _context;
        private ClientRepository _clientRepository;
        private ManagerRepository _managerRepository;
        private ProductRepository _productRepository;
        private SaleInfoRepository _saleInfoRepository;

        public EFUnitOfWork()
        {
            _context = new DBModelContainer();
        }

        public IModelRepository<Models.Client> Clients
        {
            get
            {
                if (_clientRepository == null)
                {
                    _clientRepository = new ClientRepository(_context);
                }
                return _clientRepository;
            }
        }

        public IModelRepository<Models.Manager> Managers
        {
            get
            {
                if (_managerRepository == null)
                {
                    _managerRepository = new ManagerRepository(_context);
                }
                return _managerRepository;
            }
        }

        public IModelRepository<Models.Product> Products
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(_context);
                }
                return _productRepository;
            }
        }

        public IModelRepository<Models.SaleInfo> SalesInfo
        {
            get
            {
                if (_saleInfoRepository == null)
                {
                    _saleInfoRepository = new SaleInfoRepository(_context);
                }
                return _saleInfoRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
        }

        ~EFUnitOfWork()
        {
            Dispose();
        }
    }
}
