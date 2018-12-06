using Domain.Entities.JunctionEntities.AccessControl;
using DomainValidator.Validations;
using Shared.Entities;
using System;
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
		public DateTime CreationDate { get; private set; }

		public ICollection<UserRole> UserRoles { get; set; }

		public User(string name, string password, string email, bool status, DateTime creationDate)
		{
			Name = name;
			Password = password;
			Email = email;
			Status = status;
			CreationDate = creationDate;
			
			Validate();
		}

		public User(string name, string password, string email, bool status, DateTime creationDate, ICollection<UserRole> userRoles) 
			: this(name, password, email, status, creationDate)
		{
			UserRoles = userRoles;

			AddNotifications(new Validation().IsNotNullOrEmpty(UserRoles, "Usuario.UserRoles"));
		}

		public override void Validate()
		{
			AddNotifications(
				new Validation().IsNotNullOrEmpty(Name, "Usuario.Nome"),
				new Validation().IsNotNullOrEmpty(Password, "Usuario.Senha"),
				new Validation().IsNotNullOrEmpty(Email, "Usuario.Email"),
				new Validation().IsEmail(Email, "Usuario.Email"),
				new Validation().IsNotNull(CreationDate, "Usuario.CreationDate"),
				new Validation().IsGreaterThan(CreationDate, DateTime.MinValue, "Usuario.CreationDate"));
		}
	}
}
