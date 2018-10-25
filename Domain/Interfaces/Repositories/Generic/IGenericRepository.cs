using CrossLayerHelpers.Filters;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories.Generic
{
	public interface IGenericRepository<TEntity> where TEntity : BaseEntity
	{
		IEnumerable<string> IncludeCollection { get; set; }

		Task<int> AddAsync(TEntity entity);
		Task<int> AddRangeAsync(IEnumerable<TEntity> entities);

		Task<int> UpdateAsync(TEntity entity);
		Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities);

		Task<int> RemoveAsync(TEntity entity);
		Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities);

		Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> filter, IEnumerable<string> includes = null);

		Task<IEnumerable<TEntity>> GetManyPaginatedAsync(GetPaginatedFilter filter = null, IEnumerable<string> includes = null);
		Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, IEnumerable<string> includes = null);

		Task<long> CountAsync(GetManyFilter filter = null);
		Task<long> CountAsync(Expression<Func<TEntity, bool>> filter = null);
	}
}
