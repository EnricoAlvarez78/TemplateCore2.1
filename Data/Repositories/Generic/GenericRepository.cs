using CrossLayerHelpers.Filters;
using Data.EFContext;
using Domain.Interfaces.Repositories.Generic;
using LinqQueryHelpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Repositories.Generic
{
	public abstract class GenericRepository<TEntity> : IDisposable, IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly SqlServerContext _context;

        public IEnumerable<string> IncludeCollection { get; set; }

        protected GenericRepository(SqlServerContext context) => _context = context;

        #region Commands

        public virtual async Task<int> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public virtual async Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
            return await _context.SaveChangesAsync();
        }

        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            return await _context.SaveChangesAsync();
        }

        public virtual async Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().UpdateRange(entities);
            return await _context.SaveChangesAsync();
        }

        public virtual async Task<int> RemoveAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public virtual async Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            return await _context.SaveChangesAsync();
        }

        #endregion

        #region Querys

        public virtual async Task<IEnumerable<TEntity>> GetManyPaginatedAsync(GetPaginatedFilter filter = null, IEnumerable<string> includes = null)
        {
			IQueryable<TEntity> query = MountQuery(includes);

			if (filter != null)
			{
				if (filter.Filter.Any())
					query = GenericFilterHelper<TEntity>.GenericFilter(query, filter.Filter);

				if (filter.Sort != null && filter.Sort.Any())
					query = GenericSortHelper<TEntity>.GenericSort(query, filter.Sort);

				if (filter.PageIndex != null && filter.PageIndex > 0 && filter.PageSize != null && filter.PageSize > 0)
					query = query.Skip(filter.PageSize.GetValueOrDefault() * (filter.PageIndex.GetValueOrDefault() - 1)).Take(filter.PageSize.GetValueOrDefault());
			}

			return await query.AsNoTracking().ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, IEnumerable<string> includes = null)
        {
            IQueryable<TEntity> query = MountQuery(includes);

            if (filter != null)
                query = query.Where(filter);

            return orderBy == null ?
                await query.AsNoTracking().ToListAsync() :
                await orderBy(query).AsNoTracking().ToListAsync();
        }

        public virtual async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> filter, IEnumerable<string> includes = null)
        {
            IQueryable<TEntity> query = MountQuery(includes);

            if (filter != null)
                query = query.Where(filter);

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public virtual async Task<long> CountAsync(GetManyFilter filter = null)
        {
            IQueryable<TEntity> query = MountQuery(null);

            if (filter != null && filter.Filter.Any())
                query = GenericFilterHelper<TEntity>.GenericFilter(query, filter.Filter);

            return await query.AsNoTracking().CountAsync();
        }

        public virtual async Task<long> CountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = MountQuery(null);

            if (filter != null)
                query = query.Where(filter);

            return await query.AsNoTracking().CountAsync();
        }

        #endregion

        #region Privates

        private IQueryable<TEntity> MountQuery(IEnumerable<string> includes = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            includes = includes ?? IncludeCollection;

            if (includes != null && includes.Any())
                includes.ToList().ForEach(includeProperty => query = query.Include(includeProperty));

            return query;
        }

        #endregion

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
