using Data.Interfaces;
using Domain.Entities.JunctionEntities.AccessControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings.JunctionEntities.AccessControl
{
	public class RolePermissionMapping : IEntityTypeConfiguration<RolePermission>, IMapping
	{
		public void Configure(EntityTypeBuilder<RolePermission> builder)
		{
			builder.ToTable("RolePermission", "AccessControl");

			builder.HasKey(e => new { e.IdRole, e.IdPermission });

			builder.HasOne(d => d.Role)
				   .WithMany(p => p.RolePermissions)
				   .HasForeignKey(d => d.IdRole)
				   .HasConstraintName("FK_RolePermission_Role");

			builder.HasOne(d => d.Permission)
				   .WithMany(p => p.RolePermissions)
				   .HasForeignKey(d => d.IdPermission)
				   .HasConstraintName("FK_RolePermission_Permission");
		}
	}
}
