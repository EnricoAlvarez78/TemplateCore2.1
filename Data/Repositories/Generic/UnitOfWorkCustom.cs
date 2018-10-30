using Domain.Interfaces.Repositories;
using Domain.Interfaces.Repositories.Generic;

namespace Data.Repositories.Generic
{
    public partial class UnitOfWork : IUnitOfWork
    {
		private readonly IUserRepository _userRepository;
		public IUserRepository UserRepository { get { return _userRepository ?? new UserRepository(_context); } }
	}
}
