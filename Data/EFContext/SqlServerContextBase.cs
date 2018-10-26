using Data.Interfaces;
using DomainValidator.Notifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace Data.EFContext
{
    public partial class SqlServerContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connectionStrings = GetConnectionStringsHelper.GetConnectionStrings();

            //var connStr = connectionStrings["DefaultConnectionString"];

            //if (!optionsBuilder.IsConfigured && !string.IsNullOrEmpty(connStr))
            //{
            //    optionsBuilder.EnableSensitiveDataLogging();
            //    optionsBuilder.UseSqlServer(connStr, b => b.UseRowNumberForPaging());
            //}
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
        }
    }
}
