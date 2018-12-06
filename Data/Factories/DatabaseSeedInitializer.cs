using Domain.Entities;
using Domain.Entities.JunctionEntities.AccessControl;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data.Factories
{
	/// <summary>
	/// Para ajuda sobre migrations use:
	/// PowerShell: Get-Help about_EntityFrameworkCore
	/// Cli: dotnet ef --help
	/// </summary>
	public class DatabaseSeedInitializer
	{
		public DatabaseSeedInitializer(ModelBuilder modelBuilder)
		{				
			var user = new User("AdministratorUser", "Change123", "admin@admin.com", true, DateTime.Now);

			var role = new Role("AdministratorRole", "Role of system administrator", true, DateTime.Now);

			var permission1 = new Permission("AddUser", "Create new User", true, DateTime.Now);
			var permission2 = new Permission("EditUser", "Edit User", true, DateTime.Now);
			var permission3 = new Permission("RemoveUser", "Remove User", true, DateTime.Now);
			var permission4 = new Permission("ListUser", "List User", true, DateTime.Now);
			var permission5 = new Permission("AddRole", "Create new Role", true, DateTime.Now);
			var permission6 = new Permission("EditRole", "Edit Role", true, DateTime.Now);
			var permission7 = new Permission("RemoveRole", "Remove Role", true, DateTime.Now);
			var permission8 = new Permission("ListRole", "List Role", true, DateTime.Now);

			var userRoles = new UserRole[] { //Obs: Só funciona se for array.
				new UserRole(user.Id, role.Id)
			}; 

			var rolePermissions = new RolePermission[] {
				new RolePermission(role.Id, permission1.Id),
				new RolePermission(role.Id, permission2.Id),
				new RolePermission(role.Id, permission3.Id),
				new RolePermission(role.Id, permission4.Id),
				new RolePermission(role.Id, permission5.Id),
				new RolePermission(role.Id, permission6.Id),
				new RolePermission(role.Id, permission7.Id),
				new RolePermission(role.Id, permission8.Id),
			};

			modelBuilder.Entity<User>().HasData(user);
			modelBuilder.Entity<Role>().HasData(role);
			modelBuilder.Entity<Permission>().HasData(permission1);
			modelBuilder.Entity<Permission>().HasData(permission2);
			modelBuilder.Entity<Permission>().HasData(permission3);
			modelBuilder.Entity<Permission>().HasData(permission4);
			modelBuilder.Entity<Permission>().HasData(permission5);
			modelBuilder.Entity<Permission>().HasData(permission6);
			modelBuilder.Entity<Permission>().HasData(permission7);
			modelBuilder.Entity<Permission>().HasData(permission8);
			modelBuilder.Entity<UserRole>().HasData(userRoles);
			modelBuilder.Entity<RolePermission>().HasData(rolePermissions);
		}
	}
}
