namespace RectangleIntersection.Api {
	public class AppConfiguration {
		public DbSettings DbSettings { get; set; }
	}

	public class DbSettings {
		public string RectangleMsSqlRepositoryConnectionString { get; set; }
		public int RectangleReadingBatchSise { get; set; }
	}
}