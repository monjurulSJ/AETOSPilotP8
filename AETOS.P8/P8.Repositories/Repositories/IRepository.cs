using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace P8.Repository.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T entity);

        Task CreateAsync(List<T> t);

        Task UpdateAsync(T entity);

        Task UpdateAsync(List<T> t);

        Task DeleteAsync(int id);

        Task DeleteRangeAsync(ICollection<T> entities);

        IQueryable<T> GetAll();

        Task<ICollection<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);
        Task<T> GetLastOrDefaultAsync();

        IQueryable<T> GetBy(Expression<Func<T, bool>> predicate);

        Task<ICollection<T>> GetByAsync(Expression<Func<T, bool>> predicate);

        Task<ICollection<T>> RunSqlQueryAsync(string sqlQuery);

        IQueryable<T> RunSqlQueryAsQueryable(string sqlQuery);
    }
}
