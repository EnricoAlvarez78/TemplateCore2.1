using Domain.Entities;
using Domain.Entities.JunctionEntities.AccessControl;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Data.Factories
{
	public class DatabaseSeedInitializer
	{
		public DatabaseSeedInitializer(ModelBuilder modelBuilder)
		{				
			var user = new User("AdministratorUser", "Change123", "admin@admin.com", true, DateTime.Now, new List<UserRole>());

			var role = new Role("AdministratorRole", true, DateTime.Now);

			var userRoles = new List<UserRole> { new UserRole(user.Id, role.Id) };

			modelBuilder.Entity<User>().HasData(user);
			modelBuilder.Entity<Role>().HasData(role);
			modelBuilder.Entity<UserRole>().HasData(userRoles);
		}
	}
}
