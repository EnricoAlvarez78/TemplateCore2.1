using Data.EFContext;
using Data.Repositories.Generic;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Data.Repositories
{
	internal class UserRepository : GenericRepository<User>, IUserRepository
	{
		public UserRepository(SqlServerContext context) : base(context)
		{
		}
	}
}
