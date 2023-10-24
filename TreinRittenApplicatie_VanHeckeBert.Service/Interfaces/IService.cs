using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreinRittenApplicatie_VanHeckeBert.Service.Interfaces
{
    public interface IService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<bool> Add(T entity);
        Task<bool> Delete(T entity);
        Task<bool> Update(T entity);
    }
}
