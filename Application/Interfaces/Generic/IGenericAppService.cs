using CrossLayerHelpers.Filters;
using CrossLayerHelpers.Results;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Interfaces.Generic
{
	public interface IGenericAppService<TEntity> where TEntity : BaseEntity
	{
		Task<OperationResult> Add(TEntity obj);
		Task<OperationResult> AddRange(IEnumerable<TEntity> objs);

		Task<OperationResult> Update(TEntity obj);
		Task<OperationResult> UpdateRange(IEnumerable<TEntity> objs);

		Task<OperationResult> Remove(Guid id);
		Task<OperationResult> RemoveRange(IEnumerable<Guid> ids);

		Task<GetOneResult<TEntity>> GetById(Guid id, IEnumerable<string> includes = null);
		Task<GetOneResult<TEntity>> GetOne(Expression<Func<TEntity, bool>> filter, IEnumerable<string> includes = null);

		Task<GetManyResult<TEntity>> GetManyPaginated(GetPaginatedFilter filter, IEnumerable<string> includes = null);
		Task<GetManyResult<TEntity>> GetMany(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, IEnumerable<string> includes = null);

		Task<GetCountResult> Count(GetManyFilter filter);
		Task<GetCountResult> Count(Expression<Func<TEntity, bool>> filter);
	}
}
