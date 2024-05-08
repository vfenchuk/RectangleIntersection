using Microsoft.AspNetCore.Mvc;
using RectangleIntersection.Api.Managers;
using RectangleIntersection.Api.Models;
using System.Drawing;

namespace RectangleIntersection.Api.Controllers {
	[ApiController]
	[Route("rectangleintersection")]
	public class RectangleIntersectionController : Controller {
		private readonly IntersectionManager _intersectionManager;

		public RectangleIntersectionController(IntersectionManager intersectionManager) {
			_intersectionManager = intersectionManager;
		}

		[HttpGet]
		[Route("")]
		public IActionResult CheckStatus() {
			return Ok($"{nameof(RectangleIntersectionController)} Alive");
		}

		[HttpGet("getrectanglesintersectedbysegment")]
		public IActionResult GetRectanglesIntersectedBySegment(double segmentStartX, double segmentStartY, double segmentEndX, double segmentEndY) {
			Segment segment = new Segment() {
				StartX = segmentStartX,
				StartY = segmentStartY,
				EndX = segmentEndX,
				EndY = segmentEndY,
			};

			List<Rectangle> intersectedRectangles = _intersectionManager.SearchRectanglesIntersectedBySegment(segment);

			return Ok(intersectedRectangles);
		}
	}
}