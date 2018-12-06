using Data.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Data.Mappings.AccessControl
{
	public class UserMapping : IEntityTypeConfiguration<User>, IMapping
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("User", "AccessControl");

			builder.HasKey(e => e.Id);

			builder.Property(e => e.Id).HasDefaultValueSql("(newid())");

			builder.Property(e => e.Name).IsRequired().HasMaxLength(256).IsUnicode(false);

			builder.Property(e => e.Password).IsRequired().HasMaxLength(255).IsUnicode(false);

			builder.Property(e => e.Email).IsRequired().HasMaxLength(120).IsUnicode(false);

			builder.Property(e => e.Status).HasDefaultValueSql("((1))");						
		}
	}
}
