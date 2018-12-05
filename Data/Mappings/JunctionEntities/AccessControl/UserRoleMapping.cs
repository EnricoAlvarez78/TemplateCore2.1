using Data.Interfaces;
using Domain.Entities.JunctionEntities.AccessControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings.JunctionEntities.AccessControl
{
	public class UserRoleMapping : IEntityTypeConfiguration<UserRole>, IMapping
	{
		public void Configure(EntityTypeBuilder<UserRole> builder)
		{
			builder.ToTable("UserRole", "AccessControl");

			builder.HasKey(e => new { e.IdUser, e.IdRole });

			builder.HasOne(d => d.User)
				   .WithMany(p => p.UserRoles)
				   .HasForeignKey(d => d.IdUser)
				   .HasConstraintName("FK_UserRole_User");

			builder.HasOne(d => d.Role)
				   .WithMany(p => p.UserRoles)
				   .HasForeignKey(d => d.IdRole)
				   .HasConstraintName("FK_UserRole_Role");
		}
	}
}
