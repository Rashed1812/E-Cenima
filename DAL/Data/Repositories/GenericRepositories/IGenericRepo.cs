using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Repositories.GenericRepositories
{
    public interface IGenericRepo<T> where T : class
    {

        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllPaginationAsync(int pageSize ,int pageNumber);
        Task<T?> FindByIdAsync(int id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T item);
        void Remove(T item);
        void Update(T item);
    }
}
