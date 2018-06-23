using System;

namespace DAL.Repository.Interfaces
{
    interface IUnitOfWork : IDisposable
    {
        IModelRepository<DAL.Models.Client> Clients { get; }
        IModelRepository<DAL.Models.Manager> Managers { get; }
        IModelRepository<DAL.Models.Product> Products { get; }
        IModelRepository<DAL.Models.SaleInfo> SalesInfo { get; }

        void Save();
    }
}
