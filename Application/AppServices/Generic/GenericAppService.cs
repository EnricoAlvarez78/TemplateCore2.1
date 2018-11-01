using Application.Interfaces.Generic;
using CrossLayerHelpers.Filters;
using CrossLayerHelpers.Results;
using Domain.Interfaces.Services.Generic;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppServices.Generic
{
	public abstract class GenericAppService<TEntity> : IGenericAppService<TEntity> where TEntity : BaseEntity
	{
		private readonly IGenericService<TEntity> _service;

		public GenericAppService(IGenericService<TEntity> service)
		{
			_service = service;
		}

		#region Command´s

		public virtual async Task<OperationResult> Add(TEntity obj)
		{
			try
			{
				if (obj.IsValid)
				{
					var response = await _service.Add(obj);

					return OperationResult.WasSuccessful(response) ?
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
					var response = await _service.AddRange(objs);

					return OperationResult.WasSuccessful(response) ?
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
					var response = await _service.Update(obj);

					return OperationResult.WasSuccessful(response) ?
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
					var response = await _service.UpdateRange(objs);

					return OperationResult.WasSuccessful(response) ?
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
				var response = await _service.Remove(id);

				return OperationResult.WasSuccessful(response) ?
						OperationResult.SuccessResponse() :
						OperationResult.BadRequestResponse();
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
				var response = await _service.RemoveRange(ids);

				return OperationResult.WasSuccessful(response) ?
						OperationResult.SuccessResponse() :
						OperationResult.BadRequestResponse();
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
				var response = await _service.GetOne(x => x.Id.Equals(id), includes);

				return GetOneResult<TEntity>.WasSuccessful(response) ?
						GetOneResult<TEntity>.SuccessResponse(response.Entity) :
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
				var response = await _service.GetOne(filter, includes);

				return GetOneResult<TEntity>.WasSuccessful(response) ?
						GetOneResult<TEntity>.SuccessResponse(response.Entity) :
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
				var response = await _service.GetManyPaginated(filter, includes);

				return GetManyResult<TEntity>.WasSuccessful(response) ?
						GetManyResult<TEntity>.SuccessResponse(response.Entities, response.TotalAmount) :
						GetManyResult<TEntity>.BadRequestResponse();
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
				var response = await _service.GetMany(filter, orderBy, includes);

				return GetManyResult<TEntity>.WasSuccessful(response) ?
						GetManyResult<TEntity>.SuccessResponse(response.Entities) :
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
				//var response = await _specificRepository.CountAsync(filter);

				//return response >= default(long) ?
				//		GetCountResult.SuccessResponse(response) :
				//		GetCountResult.BadRequestResponse();
				throw new NotImplementedException();
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
				//var response = await _specificRepository.CountAsync(filter);

				//return response >= default(long) ?
				//		GetCountResult.SuccessResponse(response) :
				//		GetCountResult.BadRequestResponse();
				throw new NotImplementedException();
			}
			catch (Exception ex)
			{
				return GetCountResult.ExceptionResponse(ex);
			}
		}

		#endregion
	}
}
