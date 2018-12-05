using Data.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings.AccessControl
{
	public class RoleMapping : IEntityTypeConfiguration<Role>, IMapping
	{
		public void Configure(EntityTypeBuilder<Role> builder)
		{
			builder.ToTable("Role", "AccessControl");

			builder.HasKey(e => e.Id);

			builder.Property(e => e.Id).HasDefaultValueSql("(newid())");

			builder.Property(e => e.Name).IsRequired().HasMaxLength(256).IsUnicode(false);

			builder.Property(e => e.Status).HasDefaultValueSql("((1))");
		}
	}
}
