using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Repositories.GenericRepositories
{
    public class GenericRepo<T>(AppDbContext context) : IGenericRepo<T> where T : class
    {
    

        public async Task AddAsync(T item)
        {
           await context.Set<T>().AddAsync(item);
           await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
           return  await context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<T?> FindByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
         
            return await context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllPaginationAsync(int pageSize, int pageNumber=1)
        {
           return await context.Set<T>().Skip(pageSize*(pageNumber-1)).Take(pageSize).ToListAsync();
        }

        public  void Remove(T item)
        {
           context.Set<T>().Remove(item);
        }

        public void Update(T item)
        {
            context.Set<T>().Update(item);
        }
    }
}
