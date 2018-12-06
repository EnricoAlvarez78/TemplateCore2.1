using DomainValidator.Notifications;
using DomainValidator.Validations;
using System;

namespace Domain.Entities.JunctionEntities.AccessControl
{
	public class RolePermission : Notifiable
	{
		public Guid IdRole { get; private set; }
		public Guid IdPermission { get; private set; }

		public Role Role { get; private set; }
		public Permission Permission { get; private set; }

		public RolePermission(Guid idRole, Guid idPermission)
		{
			IdRole = idRole;
			IdPermission = idPermission;

			Validate();
		}

		public void Validate()
		{
			AddNotifications(
				new Validation().IsNotNull(IdPermission, "RolePermission.IdPermission"),
				new Validation().IsNotEmpty(IdPermission, "RolePermission.IdPermission", "A prorpiedade UserRole.IdPermission não pode ser vazia."),
				new Validation().IsNotNull(IdRole, "RolePermission.IdRole"),
				new Validation().IsNotEmpty(IdRole, "RolePermission.IdRole", "A prorpiedade UserRole.IdRole não pode ser vazia."));
		}
	}
}
