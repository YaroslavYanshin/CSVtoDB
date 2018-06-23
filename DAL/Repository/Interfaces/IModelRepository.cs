using System;
using System.Collections.Generic;

namespace DAL.Repository.Interfaces
{
    public interface IModelRepository<T>
    {
        void Add(T item);
        int? GetId(T item);
        IEnumerable<T> GetAll();
        T GetById(int Id);
        void Update(T item);
    }
}
