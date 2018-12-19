using Mvc.ViewModels;
using System;

namespace Mvc.Areas.AccessControl.ViewModels
{
	public class UserViewModel : BaseViewModel
	{
		public string Name { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public bool Status { get; private set; }
		public DateTime CreationDate { get; private set; }
	}
}
