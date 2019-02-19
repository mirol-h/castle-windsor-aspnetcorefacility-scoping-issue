using System;
using Castle.Facilities.AspNetCore;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CastleWindsorAspNetCoreFacilityRepro.Controllers;
using CastleWindsorAspNetCoreFacilityRepro.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CastleWindsorAspNetCoreFacilityRepro
{
	public class Startup
	{
		private readonly WindsorContainer container = new WindsorContainer();

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			container.AddFacility<AspNetCoreFacility>(f => f.CrossWiresInto(services));

			services.AddMvc();
			services.AddLogging((lb) => lb.AddConsole().AddDebug());

			container.Register(Component.For<IService>().ImplementedBy<Service>().LifestyleScoped()/*.CrossWired()*/);
			container.Register(Component.For<IFacade>().ImplementedBy<Facade>().LifestyleScoped().CrossWired());

			return services.AddWindsor(container,
				opts => opts.UseEntryAssembly(typeof(HomeController).Assembly));
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
