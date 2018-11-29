using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mvc.Areas.AccessControl.ViewModels;
using Mvc.Controllers.Generic;
using Mvc.Models;

namespace Mvc.Areas.AccessControl.Controllers
{
	[Area("AccessControl")]
	[AllowAnonymous]
	public class UserController : GenericController<User, UserViewModel>
	{
		public UserController(IUserAppService appService) : base(appService)
		{
			Permissions.Add(EGenericAction.AllowAll, "");
		}
	}
}