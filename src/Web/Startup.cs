namespace TestingInNetCore.Web
{
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;
	using Microsoft.EntityFrameworkCore;
	using TestingInNetCore.DataAccess;
	using TestingInNetCore.Repository;
	using TestingInNetCore.Service;

	public class Startup
	{

		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			this.Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {

			services.AddTransient<IRoomRepository, RoomRepository>();
			services.AddTransient<IDoorRepository, DoorRepository>();
			services.AddTransient<ILockRepository, LockRepository>();
			services.AddTransient<ILockTypeRepository, LockTypeRepository>();
			services.AddTransient<IDashboardGatherer, DashboardGatherer>();
			services.AddTransient<IDoorLocker, DoorLocker>();
			services.AddTransient<IRandomDoorLocker, RandomDoorLocker>();

			var connection = this.Configuration.GetValue<string>("Data:DefaultConnection:ConnectionString");
			services.AddDbContext<LockSmithContext>(options => options.UseSqlServer(connection));


			services.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}

	}
}
