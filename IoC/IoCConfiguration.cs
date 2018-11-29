using Application.AppServices;
using Application.Interfaces;
using Data.EFContext;
using Data.Repositories.Generic;
using Domain.Interfaces.Repositories.Generic;
using Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Service.Services;

namespace IoC
{
	public class IoCConfiguration
	{
		public static void Configure(IServiceCollection services)
		{
			// Application
			services.AddScoped(typeof(IUserAppService), typeof(UserAppService));

			// Core
			services.AddScoped(typeof(IUserService), typeof(UserService));

			// Infra
			services.AddScoped<SqlServerContext>();
			services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
		}
	}
}
