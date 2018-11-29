using Application.AppServices.Generic;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.AppServices
{
	public class UserAppService : GenericAppService<User>, IUserAppService
	{
		public UserAppService(IUserService service) : base(service)
		{
		}
	}
}
