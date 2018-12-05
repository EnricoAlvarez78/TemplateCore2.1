using Domain.Entities.JunctionEntities.AccessControl;
using Shared.Entities;
using System.Collections.Generic;

namespace Domain.Entities
{
	public class Role : BaseEntity
	{
		public string Name { get; private set; }

		public bool Status { get; private set; }

		public ICollection<UserRole> UserRoles { get;  set; }

		public override void Validate() { }
	}
}
