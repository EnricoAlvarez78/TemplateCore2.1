using Domain.Entities.JunctionEntities.AccessControl;
using DomainValidator.Validations;
using Shared.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
	public class Role : BaseEntity
	{
		public string Name { get; private set; }
		public bool Status { get; private set; }
		public DateTime CreationDate { get; private set; }
		public ICollection<UserRole> UserRoles { get; private set; } = new HashSet<UserRole>();

		public Role(string name, bool status, DateTime creationDate)
		{
			Name = name;
			Status = status;
			CreationDate = creationDate;
			
			Validate();
		}

		public Role(string name, bool status, DateTime creationDate, ICollection<UserRole> userRoles) 
			: this(name, status, creationDate)
		{
			UserRoles = userRoles;

			AddNotifications(new Validation().IsNotNullOrEmpty(UserRoles, "Role.UserRoles"));
		}

		public override void Validate()
		{
			AddNotifications(
				new Validation().IsNotNullOrEmpty(Name, "Role.Nome"),
				new Validation().IsNotNull(CreationDate, "Role.CreationDate"),
				new Validation().IsGreaterThan(CreationDate, DateTime.MinValue, "Role.CreationDate"));
		}
	}
}
