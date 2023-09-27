using Microsoft.EntityFrameworkCore;
using P8.Model.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace P8.Repository.Repositories
{
    public class Repository<T> : IDisposable, IRepository<T> where T : class
    {
        protected AppDbContext _appDbContext;
        protected DbSet<T> _dbSet;

        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = appDbContext.Set<T>();
        } 
        public async Task CreateAsync(T t)
        { 
            await _dbSet.AddAsync(t);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task CreateAsync(List<T> t)
        {
            await _dbSet.AddRangeAsync(t);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(List<T> t)
        {
            _dbSet.UpdateRange(t);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(T t)
        {
            await Task.FromResult(_dbSet.Update(t)); 
            await _appDbContext.SaveChangesAsync();
        } 
        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id); 
            await Task.Run(() => _dbSet.Remove(entity)); 
            await _appDbContext.SaveChangesAsync(); 
        }
        public async Task DeleteRangeAsync(ICollection<T> entities)
        {
            await Task.Run(() => _dbSet.RemoveRange(entities)); 
            await _appDbContext.SaveChangesAsync();
        } 
        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        } 
        public async Task<ICollection<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        } 
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);

        }
        public async Task<T> GetLastOrDefaultAsync()
        {
            return await _dbSet.LastOrDefaultAsync();
        } 
        public IQueryable<T> GetBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _dbSet.Where(predicate);
            return query;
        } 
        public async Task<ICollection<T>> GetByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }
        public async Task<ICollection<T>> RunSqlQueryAsync(string sqlQuery)
        {
            return await _dbSet.FromSqlRaw(sqlQuery).ToListAsync();
        }
        public IQueryable<T> RunSqlQueryAsQueryable(string sqlQuery)
        {
            return _dbSet.FromSqlRaw(sqlQuery).AsNoTracking();
        } 
        public void Dispose()
        {
            _appDbContext.Dispose();
        }
    }
}
