using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProLab.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<bool> Delete(T entity);
        Task<bool> Update(T entity);
        Task<int> Insert(T entity);
        Task<IEnumerable<T>> GetList();
        Task<T> GetById(int id);
    }
}
