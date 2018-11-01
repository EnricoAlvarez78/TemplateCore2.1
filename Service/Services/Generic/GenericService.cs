using CrossLayerHelpers.Filters;
using CrossLayerHelpers.Results;
using Domain.Interfaces.Repositories.Generic;
using Domain.Interfaces.Services.Generic;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Service.Services.Generic
{
	public abstract class GenericService<TEntity> : IGenericService<TEntity> where TEntity : BaseEntity
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IGenericRepository<TEntity> _specificRepository;

		public GenericService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;

			_specificRepository = (IGenericRepository<TEntity>)_unitOfWork.GetType().GetProperty(typeof(TEntity).Name + "Repository").GetValue(_unitOfWork, null);
		}

		#region Command´s

		public virtual async Task<OperationResult> Add(TEntity obj)
		{
			try
			{
				if (obj.IsValid)
				{
					var response = await _specificRepository.AddAsync(obj);

					return response > 0 ?
							OperationResult.SuccessResponse(string.Join("; ", obj.Notifications.Select(x => x.Message))) :
							OperationResult.BadRequestResponse(string.Join("; ", obj.Notifications.Select(x => x.Message)));
				}

				return OperationResult.BadRequestResponse(string.Join("; ", obj.Notifications.Select(x => x.Message)));
			}
			catch (Exception ex)
			{
				return OperationResult.ExceptionResponse(ex);
			}
		}

		public virtual async Task<OperationResult> AddRange(IEnumerable<TEntity> objs)
		{
			try
			{
				if (!objs.Select(x => x.IsValid == false).Any())
				{
					var response = await _specificRepository.AddRangeAsync(objs);

					return response > 0 ?
							OperationResult.SuccessResponse(objs.Select(x => x.Notifications).ToString()) :
							OperationResult.BadRequestResponse(objs.Select(x => x.Notifications).ToString());
				}

				return OperationResult.BadRequestResponse(objs.Select(x => x.Notifications).ToString());
			}
			catch (Exception ex)
			{
				return OperationResult.ExceptionResponse(ex);
			}
		}

		public virtual async Task<OperationResult> Update(TEntity obj)
		{
			try
			{
				if (obj.IsValid)
				{
					var response = await _specificRepository.UpdateAsync(obj);

					return response > 0 ?
							OperationResult.SuccessResponse(obj.Notifications.ToString()) :
							OperationResult.BadRequestResponse(obj.Notifications.ToString());
				}

				return OperationResult.BadRequestResponse(obj.Notifications.ToString());
			}
			catch (Exception ex)
			{
				return OperationResult.ExceptionResponse(ex);
			}
		}

		public virtual async Task<OperationResult> UpdateRange(IEnumerable<TEntity> objs)
		{
			try
			{
				if (objs.FirstOrDefault(x => x.IsValid == false) == null)
				{
					var response = await _specificRepository.UpdateRangeAsync(objs);

					return response > 0 ?
							OperationResult.SuccessResponse(objs.Select(x => x.Notifications).ToString()) :
							OperationResult.BadRequestResponse(objs.Select(x => x.Notifications).ToString());
				}

				return OperationResult.BadRequestResponse(objs.Select(x => x.Notifications).ToString());
			}
			catch (Exception ex)
			{
				return OperationResult.ExceptionResponse(ex);
			}
		}

		public virtual async Task<OperationResult> Remove(Guid id)
		{
			try
			{
				var obj = await _specificRepository.GetOneAsync(x => x.Id.Equals(id));

				if (obj.IsValid)
				{
					var response = await _specificRepository.RemoveAsync(obj);

					return response > 0 ?
							OperationResult.SuccessResponse() :
							OperationResult.BadRequestResponse();
				}

				return OperationResult.BadRequestResponse(obj.Notifications.ToString());
			}
			catch (Exception ex)
			{
				return OperationResult.ExceptionResponse(ex);
			}
		}

		public virtual async Task<OperationResult> RemoveRange(IEnumerable<Guid> ids)
		{
			try
			{
				var objs = await _specificRepository.GetManyAsync(x => x.Id.Equals(ids));

				if (!objs.Select(x => x.IsValid == false).Any())
				{
					var response = await _specificRepository.RemoveRangeAsync(objs);

					return response > 0 ?
							OperationResult.SuccessResponse() :
							OperationResult.BadRequestResponse();
				}

				return OperationResult.BadRequestResponse(objs.Select(x => x.Notifications).ToString());
			}
			catch (Exception ex)
			{
				return OperationResult.ExceptionResponse(ex);
			}
		}

		#endregion

		#region Query´s

		public virtual async Task<GetOneResult<TEntity>> GetById(Guid id, IEnumerable<string> includes = null)
		{
			try
			{
				var response = await _specificRepository.GetOneAsync(x => x.Id.Equals(id), includes);

				return response != null ?
						GetOneResult<TEntity>.SuccessResponse(response) :
						GetOneResult<TEntity>.BadRequestResponse();
			}
			catch (Exception ex)
			{
				return GetOneResult<TEntity>.ExceptionResponse(ex);
			}
		}

		public virtual async Task<GetOneResult<TEntity>> GetOne(Expression<Func<TEntity, bool>> filter, IEnumerable<string> includes = null)
		{
			try
			{
				var response = await _specificRepository.GetOneAsync(filter, includes);

				return response != null ?
						GetOneResult<TEntity>.SuccessResponse(response) :
						GetOneResult<TEntity>.BadRequestResponse();
			}
			catch (Exception ex)
			{
				return GetOneResult<TEntity>.ExceptionResponse(ex);
			}
		}

		public virtual async Task<GetManyResult<TEntity>> GetManyPaginated(GetPaginatedFilter filter, IEnumerable<string> includes = null)
		{
			try
			{
				//var entities = await _specificRepository.GetManyPaginatedAsync(filter, includes);
				//var totalAmount = await _specificRepository.CountAsync(filter);

				//return entities != null && totalAmount >= 0 ?
				//		GetManyResult<TEntity>.SuccessResponse(entities, totalAmount) :
				//		GetManyResult<TEntity>.BadRequestResponse();

				return GetManyResult<TEntity>.BadRequestResponse();
			}
			catch (Exception ex)
			{
				return GetManyResult<TEntity>.ExceptionResponse(ex);
			}
		}

		public virtual async Task<GetManyResult<TEntity>> GetMany(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, IEnumerable<string> includes = null)
		{
			try
			{
				var response = await _specificRepository.GetManyAsync(filter, orderBy, includes);

				return response != null && response.Any() ?
						GetManyResult<TEntity>.SuccessResponse(response) :
						GetManyResult<TEntity>.BadRequestResponse();
			}
			catch (Exception ex)
			{
				return GetManyResult<TEntity>.ExceptionResponse(ex);
			}
		}

		public virtual async Task<GetCountResult> Count(GetManyFilter filter)
		{
			try
			{
				var response = await _specificRepository.CountAsync(filter);

				return response >= default(long) ?
						GetCountResult.SuccessResponse(response) :
						GetCountResult.BadRequestResponse();
			}
			catch (Exception ex)
			{
				return GetCountResult.ExceptionResponse(ex);
			}
		}

		public virtual async Task<GetCountResult> Count(Expression<Func<TEntity, bool>> filter)
		{
			try
			{
				var response = await _specificRepository.CountAsync(filter);

				return response >= default(long) ?
						GetCountResult.SuccessResponse(response) :
						GetCountResult.BadRequestResponse();
			}
			catch (Exception ex)
			{
				return GetCountResult.ExceptionResponse(ex);
			}
		}

		#endregion
	}
}
