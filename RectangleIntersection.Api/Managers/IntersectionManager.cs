using RectangleIntersection.Api.Models;
using RectangleIntersection.Api.Repositories;

namespace RectangleIntersection.Api.Managers {
	public class IntersectionManager {
		private readonly RectangleMsSqlRepository _rectangleMsSqlRepository;
		private readonly AppConfiguration _appConfiguration;

		public IntersectionManager(RectangleMsSqlRepository rectangleMsSqlRepository, AppConfiguration appConfiguration) {
			_rectangleMsSqlRepository = rectangleMsSqlRepository;
			_appConfiguration = appConfiguration;
		}

		public List<Rectangle> SearchRectanglesIntersectedBySegment(Segment segment) {
			List<Rectangle> intersectedRectangles = new List<Rectangle>();

			foreach (Rectangle rectangle in _rectangleMsSqlRepository.GetAllRectangles(_appConfiguration.DbSettings.RectangleReadingBatchSise)) {
				if (rectangle.IsIntersectedBySegment(segment)) {
					intersectedRectangles.Add(rectangle);
				}
			}

			return intersectedRectangles;
		}
	}
}