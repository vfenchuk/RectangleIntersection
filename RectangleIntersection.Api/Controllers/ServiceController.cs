using Microsoft.AspNetCore.Mvc;
using RectangleIntersection.Api.Managers;

namespace RectangleIntersection.Api.Controllers {
	[ApiController]
	[Route("service")]
	public class ServiceController : Controller {
		private readonly ServiceManager _serviceManager;

		public ServiceController(ServiceManager serviceManager) {
			_serviceManager = serviceManager;
		}

		[HttpGet]
		[Route("")]
		public IActionResult CheckStatus() {
			return Ok($"{nameof(ServiceController)} Alive");
		}

		//[HttpPost("insertrandomrectangles")]
		[HttpGet("insertrandomrectangles")]
		public IActionResult InsertRandomRectangles(int count = 1000) {
			_serviceManager.InsertRandomRectangles(count);

			return Ok("Done");
		}
	}
}