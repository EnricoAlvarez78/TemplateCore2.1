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
		public string Description { get; private set; }
		public bool Status { get; private set; }
		public DateTime CreationDate { get; private set; }
		public ICollection<UserRole> UserRoles { get; private set; } = new HashSet<UserRole>();
		public ICollection<RolePermission> RolePermissions { get; private set; } = new HashSet<RolePermission>();

		public Role(string name, string description, bool status, DateTime creationDate)
		{
			Name = name;
			Description = description;
			Status = status;
			CreationDate = creationDate;
			
			Validate();
		}

		public Role(string name, string description, bool status, DateTime creationDate, ICollection<UserRole> userRoles, ICollection<RolePermission> rolePermissions) 
			: this(name, description, status, creationDate)
		{
			UserRoles = userRoles;
			RolePermissions = rolePermissions;

			AddNotifications(new Validation().IsNotNullOrEmpty(UserRoles, "Role.UserRoles"));
			AddNotifications(new Validation().IsNotNullOrEmpty(RolePermissions, "Role.RolePermissions"));
		}

		public override void Validate()
		{
			AddNotifications(
				new Validation().IsNotNullOrEmpty(Name, "Role.Nome"),
				new Validation().HasMaxLen(Name, 64, "Role.Nome"),
				new Validation().IsNotNullOrEmpty(Description, "Role.Description"),
				new Validation().HasMaxLen(Description, 512, "Role.Description"),
				new Validation().IsNotNull(CreationDate, "Role.CreationDate"),
				new Validation().IsGreaterThan(CreationDate, DateTime.MinValue, "Role.CreationDate"));
		}
	}
}
