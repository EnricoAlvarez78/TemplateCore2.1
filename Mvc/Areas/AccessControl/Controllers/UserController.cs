using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mvc.Areas.AccessControl.ViewModels;
using Mvc.Controllers.Generic;

namespace Mvc.Areas.AccessControl.Controllers
{
	[Area("AccessControl")]
	[AllowAnonymous]
	public class UserController : Controller //: GenericController<User, UserViewModel>
	{
		//public UserController(IUserAppService appService) : base(appService)
		//{
		//}

		public ActionResult Index()
		{
			return View();
		}
	}
}