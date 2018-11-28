using System;

namespace Mvc.ViewModels
{
	public class BaseViewModel
	{
		public Guid Id { get; protected set; }

		public BaseViewModel() { }

		public BaseViewModel(Guid id) => Id = id;
	}
}
