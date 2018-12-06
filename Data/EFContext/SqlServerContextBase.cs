using Data.Factories;
using Data.Interfaces;
using DomainValidator.Notifications;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;
using System;
using System.Linq;
using System.Reflection;

namespace Data.EFContext
{
    public partial class SqlServerContext : DbContext
    {
		private readonly IAppSettings _appSettings;

		public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options) { }

		public SqlServerContext(IAppSettings appSettings) => _appSettings = appSettings;

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var connStr = _appSettings?.GetConnetionString();

			if (!optionsBuilder.IsConfigured && connStr != null && !string.IsNullOrEmpty(connStr.Value))
			{
				optionsBuilder.EnableSensitiveDataLogging();
				optionsBuilder.UseSqlServer(connStr.Value, b => b.UseRowNumberForPaging());
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fazendo o mapeamento com o banco de dados
            //Pega todas as classes que estão implementando a interface IMapping
            //Assim o Entity Framework é capaz de carregar os mapeamentos
            var typesToMapping = (from x in Assembly.GetExecutingAssembly().GetTypes()
                                  where x.IsClass && typeof(IMapping).IsAssignableFrom(x)
                                  select x).ToList();

            // Varrendo todos os tipos que são mapeamento 
            // Com ajuda do Reflection criamos as instancias 
            // e adicionamos no Entity Framework
            foreach (var mapping in typesToMapping)
            {
                dynamic mappingClass = Activator.CreateInstance(mapping);
                modelBuilder.ApplyConfiguration(mappingClass);
            }

            modelBuilder.Ignore<Notification>();
			
			base.OnModelCreating(modelBuilder);

			new DatabaseSeedInitializer(modelBuilder);			
		}
    }
}
