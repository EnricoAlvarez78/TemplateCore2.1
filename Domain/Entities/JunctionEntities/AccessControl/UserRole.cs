using DomainValidator.Notifications;
using DomainValidator.Validations;
using System;

namespace Domain.Entities.JunctionEntities.AccessControl
{
	public class UserRole : Notifiable
	{
		public Guid IdUser { get; private set; }
		public Guid IdRole { get; private set; }

		public User User { get; private set; }
		public Role Role { get; private set; }

		public UserRole(Guid idUser, Guid idRole)
		{
			IdUser = idUser;
			IdRole = idRole;

			Validate();
		}

		public void Validate()
		{
			AddNotifications(
				new Validation().IsNotNull(IdUser, "UserRole.IdUser"),
				new Validation().IsNotEmpty(IdUser, "UserRole.IdUser", "A prorpiedade UserRole.IdUser não pode ser vazia."),
				new Validation().IsNotNull(IdUser, "UserRole.IdRole"),
				new Validation().IsNotEmpty(IdUser, "UserRole.IdRole", "A prorpiedade UserRole.IdRole não pode ser vazia."));
		}
	}
}
