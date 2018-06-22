using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
