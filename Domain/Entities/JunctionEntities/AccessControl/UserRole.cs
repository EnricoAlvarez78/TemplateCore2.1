using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.JunctionEntities.AccessControl
{
	public class UserRole
	{
		public Guid IdUser { get; private set; }
		public Guid IdRole { get; private set; }

		public User User { get; set; }
		public Role Role { get; set; }
	}
}
