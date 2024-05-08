namespace RectangleIntersection.Api {
	public class Startup {
		public void ConfigureServices(IServiceCollection services) {
			services.AddControllers();

			#region Configuration
			IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.development.json", optional: false, reloadOnChange: true).Build();
			AppConfiguration appConfiguration = new AppConfiguration();
			configuration.Bind(appConfiguration);
			#endregion

			services.RegisterServices(appConfiguration);
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			} else {
				app.UseExceptionHandler("/error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthorization();
			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
			});
		}
	}
}