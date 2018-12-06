using Data.EFContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Data.Factories
{
	public class SqlServerContextFactory : IDesignTimeDbContextFactory<SqlServerContext>
	{
		public SqlServerContext CreateDbContext(string[] args)
		{
			IConfigurationRoot configuration = new ConfigurationBuilder()
													.SetBasePath(Directory.GetCurrentDirectory())
													.AddJsonFile("appsettings.json")
													.Build();
			
			var builder = new DbContextOptionsBuilder<SqlServerContext>().EnableSensitiveDataLogging()
																		 .UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"));
			return new SqlServerContext(builder.Options);
		}		
	}
}
