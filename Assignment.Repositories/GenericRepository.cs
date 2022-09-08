using Assignment.Data;
using Assignment.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Assignment.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private ApplicationContext _dbContext;
        public ApplicationContext DbContext => _dbContext ??= new ApplicationContext(new DbContextOptions<ApplicationContext>());

        protected GenericRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbContext.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query.Include(includeProperty);
            }
            return query;
        }

        public async Task<List<T>> AllIncludingAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            var query = DbContext.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query.Include(includeProperty);
            }
            var res = query.ToListAsync();
            return await res;
        }

        public IQueryable<T> All => GetAll();

        public IQueryable<T> GetAll()
        {
            return DbContext.Set<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await DbContext.Set<T>().ToListAsync<T>();
        }
        public IQueryable<T> GetAllByPage(int pageNumber, int pageSize)
        {
            return DbContext.Set<T>().Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return DbContext.Set<T>().Where(predicate);
        }

        public async Task<List<T>> FindByAsnc(Expression<Func<T, bool>> predicate)
        {
            return await DbContext.Set<T>().Where(predicate).ToListAsync();
        }
        public async Task<List<T>> GetAllByPageAsync(int pageNumber, int pageSize)
        {
            return await DbContext.Set<T>().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public void Add(T entity)
        {
            EntityEntry dbEntityEntry = DbContext.Entry<T>(entity);
            DbContext.Set<T>().Add(entity);
        }

        public async Task<T> AddAsync(T entity)
        {
            DbContext.Set<T>().Add(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public void Delete(T entity)
        {
            EntityEntry dbEntityEntry = DbContext.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public async Task<int> DeleteAsync(T t)
        {
            DbContext.Set<T>().Remove(t);
            return await DbContext.SaveChangesAsync();
        }

        public void DeleteRange(IQueryable<T> entity)
        {
            DbContext.Set<T>().RemoveRange(entity);
            DbContext.SaveChanges();
        }

        public async Task<int> DeleteRangeAsync(IQueryable<T> entity)
        {
            DbContext.Set<T>().RemoveRange(entity);
            return await DbContext.SaveChangesAsync();
        }

        public void Edit(T entity)
        {
            EntityEntry dbEntityEntry = DbContext.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
            DbContext.SaveChanges();
        }

        public async Task<T> EditAsync(T updated, long id)
        {
            if (updated == null)
                return null;

            T existing = await DbContext.Set<T>().FindAsync(id);
            if (existing != null)
            {
                DbContext.Entry(existing).CurrentValues.SetValues(updated);
                await DbContext.SaveChangesAsync();
            }
            return existing;
        }

        public int Count()
        {
            return DbContext.Set<T>().Count();
        }

        public async Task<int> CountAsync()
        {
            return await DbContext.Set<T>().CountAsync();
        }

    }
}
