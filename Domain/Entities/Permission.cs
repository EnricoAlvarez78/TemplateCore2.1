using Domain.Entities.JunctionEntities.AccessControl;
using DomainValidator.Validations;
using Shared.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
	public class Permission : BaseEntity
	{
		public string Name { get; private set; }
		public string Description { get; private set; }
		public bool Status { get; private set; }
		public DateTime CreationDate { get; private set; }
		public ICollection<RolePermission> RolePermissions { get; private set; } = new HashSet<RolePermission>();

		public Permission(string name, string description, bool status, DateTime creationDate)
		{
			Name = name;
			Description = description;
			Status = status;
			CreationDate = creationDate;

			Validate();
		}

		public Permission(string name, bool status, string description, DateTime creationDate, ICollection<RolePermission> rolePermissions) 
			: this(name, description, status, creationDate)
		{
			RolePermissions = rolePermissions;

			AddNotifications(new Validation().IsNotNullOrEmpty(RolePermissions, "Permission.RolePermissions"));
		}

		public override void Validate()
		{
			AddNotifications(
				new Validation().IsNotNullOrEmpty(Name, "Permission.Nome"),
				new Validation().HasMaxLen(Name, 64, "Permission.Nome"),
				new Validation().IsNotNullOrEmpty(Description, "Permission.Description"),
				new Validation().HasMaxLen(Description, 512, "Permission.Description"),
				new Validation().IsNotNull(CreationDate, "Permission.CreationDate"),
				new Validation().IsGreaterThan(CreationDate, DateTime.MinValue, "Permission.CreationDate"));
		}
	}
}
