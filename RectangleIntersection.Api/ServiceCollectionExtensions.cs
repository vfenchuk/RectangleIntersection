using Microsoft.EntityFrameworkCore;
using RectangleIntersection.Api.Managers;
using RectangleIntersection.Api.Repositories;

namespace RectangleIntersection.Api {
	public static class ServiceCollectionExtensions {
		public static void RegisterServices(this IServiceCollection services, AppConfiguration appConfiguration) {
			services.AddSingleton(appConfiguration);

			services.AddDbContext<RectangleDbContext>(options => options.UseSqlServer(appConfiguration.DbSettings.RectangleMsSqlRepositoryConnectionString));
			services.AddScoped<RectangleMsSqlRepository>();

			services.AddScoped<IntersectionManager>();
			services.AddScoped<ServiceManager>();
		}
	}
}