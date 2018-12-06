using Data.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings.AccessControl
{
	public class PermissionMapping : IEntityTypeConfiguration<Permission>, IMapping
	{
		public void Configure(EntityTypeBuilder<Permission> builder)
		{
			builder.ToTable("Permission", "AccessControl");

			builder.HasKey(e => e.Id);

			builder.Property(e => e.Id).HasDefaultValueSql("(newid())");

			builder.Property(e => e.Name).IsRequired().HasMaxLength(64).IsUnicode(false);

			builder.Property(e => e.Description).IsRequired().HasMaxLength(512).IsUnicode(false);

			builder.Property(e => e.Status).HasDefaultValueSql("((1))");
		}
	}
}
