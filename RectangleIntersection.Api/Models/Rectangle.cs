namespace RectangleIntersection.Api.Models {
	public class Rectangle {
		public Guid ID { get; set; }
		public double LTX { get; set; }
		public double LTY { get; set; }
		public double RBX { get; set; }
		public double RBY { get; set; }

		public override bool Equals(object obj) {
			if (obj == null || GetType() != obj.GetType())
				return false;

			Rectangle other = (Rectangle) obj;

			return ID == other.ID &&
				   LTX == other.LTX &&
				   LTY == other.LTY &&
				   RBX == other.RBX &&
				   RBY == other.RBY;
		}

		public override int GetHashCode() {
			return HashCode.Combine(ID, LTX, LTY, RBX, RBY);
		}

		public bool IsIntersectedBySegment(Segment segment) {
			return IsIntersecting(LTX, LTY, RBX, LTY, segment.StartX, segment.StartY, segment.EndX, segment.EndY) ||
				   IsIntersecting(RBX, LTY, RBX, RBY, segment.StartX, segment.StartY, segment.EndX, segment.EndY) ||
				   IsIntersecting(LTX, RBY, RBX, RBY, segment.StartX, segment.StartY, segment.EndX, segment.EndY) ||
				   IsIntersecting(LTX, LTY, LTX, RBY, segment.StartX, segment.StartY, segment.EndX, segment.EndY);
		}

		#region Private helpers
		private static bool IsIntersecting(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4) {
			double ua = (((x4 - x3) * (y1 - y3)) - ((y4 - y3) * (x1 - x3))) / (((y4 - y3) * (x2 - x1)) - ((x4 - x3) * (y2 - y1)));
			double ub = (((x2 - x1) * (y1 - y3)) - ((y2 - y1) * (x1 - x3))) / (((y4 - y3) * (x2 - x1)) - ((x4 - x3) * (y2 - y1)));

			return ua >= 0 && ua <= 1 && ub >= 0 && ub <= 1;
		}
		#endregion
	}
}