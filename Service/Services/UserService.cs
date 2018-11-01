using Domain.Entities;
using Domain.Interfaces.Repositories.Generic;
using Domain.Interfaces.Services;
using Service.Services.Generic;

namespace Service.Services
{
	public class UserService : GenericService<User>, IUserService
	{
		public UserService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
	}
}
