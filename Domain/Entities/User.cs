using DomainValidator.Validations;
using Shared.Entities;

namespace Domain.Entities
{
	public class User : BaseEntity
	{
		public string Name { get; private set; }
		public string Password { get; private set; }
		public string Email { get; private set; }

		public override void Validate()
		{
			AddNotifications(
				new Validation().IsNotNullOrEmpty(Name, "Usuario.Nome"),
				new Validation().IsNotNullOrEmpty(Password, "Usuario.Senha"),
				new Validation().IsEmail(Email, "Usuario.Email"));
		}
	}
}
