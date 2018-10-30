using Domain.Entities;
using Domain.Interfaces.Repositories.Generic;
using Domain.Interfaces.Services;
using Domain.Interfaces.Services.Generic;
using Service.Services.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Services
{
	public class UserService : GenericService<User>, IUserService
	{
		public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}
	}
}
