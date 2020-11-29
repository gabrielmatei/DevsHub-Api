using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevsHub.Data.Repositories
{
    public interface IRepository<TEntity>
    {
        Task<List<TEntity>> GetListAsync();
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetAsync(Guid id);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> Exists(Expression<Func<TEntity, bool>> predicate);
    }

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DataContext Context;

        public Repository(DataContext dataContext)
        {
            Context = dataContext;
        }

        public async Task<List<TEntity>> GetListAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
            var created = await Context.SaveChangesAsync();
            if (created > 0)
                return entity;
            return null;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            var updated = await Context.SaveChangesAsync();
            if (updated > 0)
                return entity;
            return null;
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            var deleted = await Context.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await this.GetAsync(id) != null;
        }

        public async Task<bool> Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().AnyAsync(predicate);
        }
    }
}
