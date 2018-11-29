using AutoMapper;
using CrossLayerHelpers.Enumerators;
using CrossLayerHelpers.Filters;
using CrossLayerHelpers.Results;
using Domain.Interfaces.Services.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mvc.Models;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Mvc.Controllers.Generic
{
	[Authorize]
	public abstract class GenericController<TEntity, TViewModel> : BaseController
		where TEntity : BaseEntity
		where TViewModel : class
	{
		private readonly object _service;

		protected GenericActionPermissions Permissions { get; set; } = new GenericActionPermissions();

		public GenericController(object service)
		{
			_service = service;

			SetDefaultActionMessages(typeof(TEntity).Name);
		}

		#region Pages

		public virtual IActionResult Index()
		{
			if (!AccessAllowed(EGenericAction.Index))
				return View("_AcessoNegado", new { area = "" });

			return View();
		}

		public virtual async Task<ActionResult> List()
		{
			if (!AccessAllowed(EGenericAction.List))
				return View("_AcessoNegado", new { area = "" });

			var result = new ManyResultsViewModel<TViewModel>();

			try
			{
				var response = await ((IGenericService<TEntity>)_service).GetMany(null);

				if (response.Success)
				{
					result.Entities = Mapper.Map<IEnumerable<TEntity>, IEnumerable<TViewModel>>(response.Entities);
					result.Total = response.TotalAmount;
				}
				else
				{
					result.Entities = response.Entities == null ? null : Mapper.Map<IEnumerable<TEntity>, IEnumerable<TViewModel>>(response.Entities);
					result.Total = response.TotalAmount <= 0 ? 0 : response.TotalAmount;

					TempData["ErrorMessage"] = ListErrorMessage;
				}

				return View(result);
			}
			catch
			{
				TempData["ErrorMessage"] = ListExceptionMessage;
				return RedirectToAction(nameof(Error));
			}
		}

		public virtual async Task<IActionResult> Details(Guid id)
		{
			if (!AccessAllowed(EGenericAction.Details))
				return View("_AcessoNegado", new { area = "" });

			TViewModel result = null;

			try
			{
				var response = await ((IGenericService<TEntity>)_service).GetById(id);

				if (!response.Success)
				{
					TempData["ErrorMessage"] = DetailsErrorMessage;

					return RedirectToAction(nameof(Index));
				}

				result = Mapper.Map<TEntity, TViewModel>(response.Entity);

				return View(result);
			}
			catch
			{
				TempData["ErrorMessage"] = DetailsExceptionMessage;
				return RedirectToAction(nameof(Error));
			}
		}

		public virtual async Task<IActionResult> Create()
		{
			if (!AccessAllowed(EGenericAction.Create))
				return View("_AcessoNegado", new { area = "" });

			var model = (TViewModel)Activator.CreateInstance(typeof(TViewModel));

			await ExecuteBeforeRenderForm(model);

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public virtual async Task<IActionResult> Create(TViewModel model)
		{
			if (!AccessAllowed(EGenericAction.Create))
				return View("_AcessoNegado", new { area = "" });

			try
			{
				if (!ModelState.IsValid)
				{
					await ExecuteToReloadModel(model, ModelState);

					TempData["WarningMessage"] = ModelStateIsValidMessage;

					return View(model);
				}

				TEntity domain = await MapToEntity(model);

				var response = await ((IGenericService<TEntity>)_service).Add(domain);

				if (response.Success)
				{
					TempData["SuccessMessage"] = CreateSuccessMessage;

					return RedirectToAction(nameof(Index));
				}

				TempData["ErrorMessage"] = CreateErrorMessage;

				return View(model);
			}
			catch
			{
				TempData["ErrorMessage"] = CreateExceptionMessage;

				return RedirectToAction(nameof(Error));
			}
		}

		public virtual async Task<IActionResult> Edit(Guid id)
		{
			if (!AccessAllowed(EGenericAction.Edit))
				return View("_AcessoNegado", new { area = "" });

			try
			{
				var response = await ((IGenericService<TEntity>)_service).GetById(id);

				if (!response.Success)
				{
					TempData["ErrorMessage"] = EditGetErrorMessage;
					return RedirectToAction(nameof(Index));
				}

				TViewModel model = Mapper.Map<TEntity, TViewModel>(response.Entity);

				await ExecuteBeforeRenderForm(model);

				return View(model);
			}
			catch
			{
				TempData["ErrorMessage"] = EditGetExceptionMessage;
				return RedirectToAction(nameof(Error));
			}
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public virtual async Task<IActionResult> Edit(TViewModel model)
		{
			if (!AccessAllowed(EGenericAction.Edit))
				return View("_AcessoNegado", new { area = "" });

			try
			{
				if (!ModelState.IsValid)
				{
					await ExecuteToReloadModel(model, ModelState);

					TempData["WarningMessage"] = ModelStateIsValidMessage;

					return View(model);
				}

				var domain = await MapToEntity(model);

				var response = await ((IGenericService<TEntity>)_service).Update(domain);

				if (response.Success)
				{
					TempData["SuccessMessage"] = EditPostSuccessMessage;

					return RedirectToAction(nameof(Index));
				}

				TempData["ErrorMessage"] = EditPostErrorMessage;

				return RedirectToAction(nameof(Error));
			}
			catch
			{
				TempData["ErrorMessage"] = EditPostExceptionMessage;

				return RedirectToAction(nameof(Error));
			}
		}

		#endregion

		#region Asynchronous Calls

		[HttpPost]
		public virtual async Task<IActionResult> Grid()
		{
			if (!AccessAllowed(EGenericAction.Grid))
				return View("_AcessoNegado", new { area = "" });

			try
			{
				var request = Request.Form.ToList();

				var draw = request.Where(x => x.Key.Equals("draw")).Select(x => x.Value).FirstOrDefault();

				var start = request.Where(x => x.Key.Equals("start")).Select(x => x.Value).FirstOrDefault();

				var length = request.Where(x => x.Key.Equals("length")).Select(x => x.Value).FirstOrDefault();

				var sortColumnId = request.Where(x => x.Key.Equals("order[0][column]")).Select(x => x.Value).FirstOrDefault();
				var sortColumn = request.Where(x => x.Key.Equals($"columns[{ sortColumnId }][name]")).Select(x => x.Value).FirstOrDefault();

				var sortColumnDirection = request.Where(x => x.Key.Equals("order[0][dir]")).Select(x => x.Value).FirstOrDefault() == "asc" ?
												ESortDirection.Asc :
												ESortDirection.Desc;
				// Todo: Revisar
				var searchValue = request.Where(x => x.Key.Equals("search[value]")).Select(x => x.Value).FirstOrDefault();

				var filterList = !string.IsNullOrEmpty(searchValue) ?
									request.Where(x => x.Key.EndsWith("[name]")).Select(f => new Filter(f.Key, f.Value)).ToList() : 
									new List<Filter>();

				var sortDictionary = new Dictionary<string, ESortDirection> { { sortColumn, sortColumnDirection } };

				var filter = new GetPaginatedFilter((Convert.ToInt32(start) / Convert.ToInt32(length)) + 1,
													Convert.ToInt32(length),
													filterList,
													sortDictionary);

				var result = await ((IGenericService<TEntity>)_service).GetManyPaginated(filter);

				if (!result.Success)
				{
					TempData["ErrorMessage"] = GridErrorMessage;

					return Json(new
					{
						draw,
						data = string.Empty,
						recordsFiltered = 0,
						recordsTotal = 0,
						allowAdd = false,
						allowEdit = false,
						allowDelete = false,
						result
					});
				}

				var rows = Mapper.Map<IEnumerable<TEntity>, IEnumerable<TViewModel>>(result.Entities);

				return Json(new
				{
					draw,
					recordsFiltered = result.TotalAmount,
					recordsTotal = result.TotalAmount,
					data = rows,
					allowAdd = AccessAllowed(EGenericAction.Create),
					allowEdit = AccessAllowed(EGenericAction.Edit),
					allowDelete = AccessAllowed(EGenericAction.Delete)
				});
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = GridExceptionMessage;
				return Json(new { ex.Message });
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public virtual async Task<IActionResult> Delete(Guid id)
		{
			if (!AccessAllowed(EGenericAction.Delete))
				return View("_AcessoNegado", new { area = "" });

			try
			{
				var response = await ((IGenericService<TEntity>)_service).Remove(id);

				if (!response.Success)
					TempData["ErrorMessage"] = DeleteErrorMessage;

				return Json(response.Success);
			}
			catch
			{
				TempData["ErrorMessage"] = DeleteExceptionMessage;

				return RedirectToAction(nameof(Error));
			}
		}

		[HttpPost]
		[DisableRequestSizeLimit]
		public virtual async Task<JsonResult> GetListAsync([FromBody]SearchModel searchModel)
		{
			try
			{
				if (!AccessAllowed(EGenericAction.GetListAsync))
					return Json(null);

				var result = await ((IGenericService<TEntity>)_service).GetManyPaginated(Mapper.Map<GetPaginatedFilter>(searchModel));

				return GetManyResult<TEntity>.WasSuccessful(result) ?
					Json(Mapper.Map<IEnumerable<TEntity>, IEnumerable<TViewModel>>(result.Entities)) :
					Json(null);
			}
			catch
			{
				return Json(null);
			}
		}

		[HttpPost]
		[DisableRequestSizeLimit]
		public virtual async Task<JsonResult> GetOneAsync([FromBody]ICollection<FilterModel> filters)
		{
			try
			{
				if (!AccessAllowed(EGenericAction.GetOneAsync))
					return Json(HttpStatusCode.Forbidden.ToString());

				var searchModel = new SearchModel(1, 1, filters, new Dictionary<ESortDirection, string> { { ESortDirection.Asc, "Id" } });

				var result = await ((IGenericService<TEntity>)_service).GetManyPaginated(Mapper.Map<GetPaginatedFilter>(searchModel));

				return GetManyResult<TEntity>.WasSuccessful(result) ?
					Json(Mapper.Map<TEntity, TViewModel>(result.Entities.FirstOrDefault())) :
					Json(null);
			}
			catch
			{
				return Json(null);
			}
		}

		#endregion

		#region Auxiliary Methods

		protected virtual async Task ExecuteBeforeRenderForm(TViewModel model) { await Task.FromResult(0); }

		protected virtual async Task ExecuteToReloadModel(TViewModel model, ModelStateDictionary modelState) { await Task.FromResult(0); }

		protected virtual Task<TEntity> MapToEntity(TViewModel model) 
			=> Task.FromResult(Mapper.Map<TViewModel, TEntity>(model));

		protected virtual void SetDefaultActionMessages(string name, bool maleGender = true)
		{
			var article = maleGender ? "o" : "a";
			var reportAdmin = "Por favor, Informe a administração do sistema.";

			ListErrorMessage = $"Ocorreu um erro ao listar dados d{ article } { name }. { reportAdmin }";
			ListExceptionMessage = $"Ocorreu um erro ao listar dados d{ article } { name }.{ reportAdmin }";

			DetailsErrorMessage = $"Ocorreu um erro ao buscar dados d{ article } { name }.{ reportAdmin }";
			DetailsExceptionMessage = $"Ocorreu um erro ao buscar dados d{ article }  { name }.{ reportAdmin }";

			GridErrorMessage = $"Ocorreu um erro ao buscar dados d{ article } { name }.{ reportAdmin }";
			GridExceptionMessage = $"Ocorreu um erro ao buscar dados d{ article }  { name }.{ reportAdmin }";

			ModelStateIsValidMessage = "O formulário contém campos inválidos. Por favor, corrija-os e tente novamente.";

			CreateSuccessMessage = $"{ name } incluid{ article } com sucesso.";
			CreateErrorMessage = $"Ocorreu um erro ao incluir { article } { name }.{ reportAdmin }";
			CreateExceptionMessage = $"Ocorreu um erro ao incluir { article } { name }.{ reportAdmin }";

			EditGetErrorMessage = $"Ocorreu um erro ao buscar dados d{ article } { name }.{ reportAdmin }";
			EditGetExceptionMessage = $"Ocorreu um erro ao buscar dados d{ article } { name }.{ reportAdmin }";

			EditPostSuccessMessage = $"{ name } editad{ article } com sucesso.";
			EditPostErrorMessage = $"Ocorreu um erro ao editar { article } { name }.{ reportAdmin }";
			EditPostExceptionMessage = $"Ocorreu um erro ao editar { article } { name }.{ reportAdmin }";

			DeleteSuccessMessage = $"{ name } excluid{ article } com sucesso.";
			DeleteErrorMessage = $"Ocorreu um erro ao excluir { article } { name }.{ reportAdmin }";
			DeleteExceptionMessage = $"Ocorreu um erro ao excluir { article } { name }.{ reportAdmin }";
		}

		#endregion

		#region ActionMessages

		protected string ListErrorMessage { get; set; }
		protected string ListExceptionMessage { get; set; }

		protected string DetailsErrorMessage { get; set; }
		protected string DetailsExceptionMessage { get; set; }

		protected string ModelStateIsValidMessage { get; set; }

		protected string CreateSuccessMessage { get; set; }
		protected string CreateErrorMessage { get; set; }
		protected string CreateExceptionMessage { get; set; }

		protected string EditGetErrorMessage { get; set; }
		protected string EditGetExceptionMessage { get; set; }

		protected string EditPostSuccessMessage { get; set; }
		protected string EditPostErrorMessage { get; set; }
		protected string EditPostExceptionMessage { get; set; }

		protected string DeleteSuccessMessage { get; set; }
		protected string DeleteErrorMessage { get; set; }
		protected string DeleteExceptionMessage { get; set; }

		protected string GridErrorMessage { get; set; }
		protected string GridExceptionMessage { get; set; }

		#endregion

		public IActionResult Error() => RedirectToAction(nameof(HomeController.Error), "Home", new { area = "" });

		protected bool AccessAllowed(EGenericAction genericAction)
		{
			if (Permissions.PermissionList.Any())
			{
				if (Permissions.PermissionList.Select(x => x.Key).Contains(EGenericAction.AllowAll))
					return true;

				if (Permissions.PermissionList.Select(x => x.Key).Contains(genericAction) &&
					User.HasClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", Permissions.PermissionList.FirstOrDefault(x => x.Key.Equals(genericAction)).Value))
					return true;
			}

			return false;
		}
	}
}