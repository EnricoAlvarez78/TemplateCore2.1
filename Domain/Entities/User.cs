using Domain.Entities.JunctionEntities.AccessControl;
using DomainValidator.Validations;
using Shared.Entities;
using System.Collections.Generic;
using System.Data;

namespace Domain.Entities
{
	public class User : BaseEntity
	{
		public string Name { get; private set; }
		public string Password { get; private set; }
		public string Email { get; private set; }
		public bool Status { get; private set; }

		public ICollection<UserRole> Roles { get;  set; }

		public override void Validate()
		{
			AddNotifications(
				new Validation().IsNotNullOrEmpty(Name, "Usuario.Nome"),
				new Validation().IsNotNullOrEmpty(Password, "Usuario.Senha"),
				new Validation().IsEmail(Email, "Usuario.Email"));
		}
	}
}
