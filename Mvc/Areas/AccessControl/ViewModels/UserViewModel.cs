using Mvc.ViewModels;

namespace Mvc.Areas.AccessControl.ViewModels
{
	public class UserViewModel : BaseViewModel
	{
		public string Name { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
	}
}
