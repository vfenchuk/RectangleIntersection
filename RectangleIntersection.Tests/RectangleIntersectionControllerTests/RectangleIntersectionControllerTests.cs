using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RectangleIntersection.Api;
using RectangleIntersection.Api.Controllers;
using RectangleIntersection.Api.Managers;
using RectangleIntersection.Api.Models;
using RectangleIntersection.Api.Repositories;

namespace RectangleIntersection.Tests.RectangleIntersectionControllerTests {
	[TestFixture]
	public class RectangleIntersectionControllerTests {
		private IServiceScope _serviceScope;

		[SetUp]
		public void Setup() {
			ServiceCollection services = new ServiceCollection();

			#region Configuration
			IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.development.json", optional: false, reloadOnChange: true).Build();
			AppConfiguration appConfiguration = new AppConfiguration();
			configuration.Bind(appConfiguration);
			#endregion

			services.AddSingleton(appConfiguration);

			services.AddDbContext<RectangleDbContext>(options => options.UseSqlServer(appConfiguration.DbSettings.RectangleMsSqlRepositoryConnectionString));
			services.AddScoped<RectangleMsSqlRepository>();

			services.AddScoped<IntersectionManager>();

			_serviceScope = services.BuildServiceProvider().CreateScope();
		}

		[TearDown]
		public void TearDown() {
			_serviceScope?.Dispose();
		}

		[Test]
		public void GetRectanglesIntersectedBySegment_ReturnsOkObjectResult_WithIntersectedRectangles() {
			// Arrange
			Segment inputSegment = JsonConvert.DeserializeObject<Segment>(File.ReadAllText("inputSegment.json")) ?? new Segment();
			List<Rectangle> resultRectangles = JsonConvert.DeserializeObject<List<Rectangle>>(File.ReadAllText("resultRectangles.json")) ?? new List<Rectangle>();
			RectangleIntersectionController controller = new RectangleIntersectionController(_serviceScope.ServiceProvider.GetRequiredService<IntersectionManager>());

			// Act
			OkObjectResult? result = controller.GetRectanglesIntersectedBySegment(inputSegment.StartX, inputSegment.StartY, inputSegment.EndX, inputSegment.EndY) as OkObjectResult;

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Is.InstanceOf<OkObjectResult>());
			Assert.That(result.Value, Is.EqualTo(resultRectangles));
		}
	}
}