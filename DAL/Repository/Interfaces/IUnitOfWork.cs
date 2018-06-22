using System;
using DAL.Models;

namespace DAL.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IModelRepository<Client> Clients { get; }
        IModelRepository<Manager> Managers { get; }
        IModelRepository<Product> Products { get; }
        IModelRepository<SaleInfo> SaleInfo { get; }

        void Save();
    }
}
