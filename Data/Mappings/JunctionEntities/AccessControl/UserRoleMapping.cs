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
			builder.HasOne(d => d.User)
							.WithMany(p => p.Roles)
							.HasForeignKey(d => d.IdUser)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("FK_UserRole_User");

			builder.HasOne(d => d.Role)
				.WithMany(p => p.Users)
				.HasForeignKey(d => d.IdRole)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_UserRole_Role");
		}
	}
}
