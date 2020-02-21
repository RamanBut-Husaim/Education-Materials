using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TrivialArchitecture.DAL;

namespace TrivialArchitecture.UI.Console.Infrastructure.IoC.Modules
{
	public class DbContextModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
			configurationBuilder.Sources.Clear();
			configurationBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
			IConfigurationRoot configurationRoot = configurationBuilder.Build();

			string connectionString = configurationRoot.GetConnectionString("TrivialArchitectureDB");

			var dbContextOptionsBuilder = new DbContextOptionsBuilder<TrivialArchitectureDbContext>()
				.UseSqlServer(connectionString);
		
			builder
				.RegisterType<TrivialArchitectureDbContext>()
				.WithParameter("options", dbContextOptionsBuilder.Options)
				.As<DbContext>()
				.InstancePerLifetimeScope();
		}
	}
}
