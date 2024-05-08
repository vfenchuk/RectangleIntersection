using RectangleIntersection.Api.Models;
using RectangleIntersection.Api.Repositories;

namespace RectangleIntersection.Api.Managers {
	public class ServiceManager {
		private readonly RectangleMsSqlRepository _rectangleMsSqlRepository;
		private readonly Random _random;

		public ServiceManager(RectangleMsSqlRepository rectangleMsSqlRepository) {
			_rectangleMsSqlRepository = rectangleMsSqlRepository;
			_random = new Random();
		}

		public bool InsertRandomRectangles(int count) {
			_rectangleMsSqlRepository.InsertRectangles(CreateRandomRectangles(count));

			return true;
		}

		private List<Rectangle> CreateRandomRectangles(int count, double min = -313, double max = 313) {
			List<Rectangle> resultRectangles = new List<Rectangle>();

			for (int i = 0; i < count; i++) {
				double x1 = (_random.NextDouble() * (max - min)) + min;
				double y1 = (_random.NextDouble() * (max - min)) + min;
				double x2 = (_random.NextDouble() * (max - min)) + min;
				double y2 = (_random.NextDouble() * (max - min)) + min;
				Rectangle rectangle = new Rectangle {
					ID = Guid.NewGuid(),
					LTX = Math.Min(x1, x2),
					LTY = Math.Min(y1, y2),
					RBX = Math.Max(x1, x2),
					RBY = Math.Max(y1, y2)
				};
				resultRectangles.Add(rectangle);
			}

			return resultRectangles;
		}
	}
}