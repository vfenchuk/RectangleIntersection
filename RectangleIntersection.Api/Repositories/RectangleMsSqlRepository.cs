using Microsoft.EntityFrameworkCore;
using RectangleIntersection.Api.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace RectangleIntersection.Api.Repositories {
	public class RectangleDbContext : DbContext {
		public DbSet<Rectangle> Rectangles { get; set; }

		public RectangleDbContext(DbContextOptions<RectangleDbContext> options)
		: base(options) {
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Rectangle>().ToTable("Rectangles");
		}
	}

	public class RectangleMsSqlRepository {
		private readonly RectangleDbContext _dbContext;

		public RectangleMsSqlRepository(RectangleDbContext dbContext) {
			_dbContext = dbContext;
		}

		public IEnumerable<Rectangle> GetAllRectangles(int batchSize) {
			IQueryable<Rectangle> rectangles = _dbContext.Rectangles.AsNoTracking();
			int totalCount = rectangles.Count();
			for (int i = 0; i < totalCount; i += batchSize) {
				List<Rectangle> batch = rectangles.Skip(i).Take(batchSize).ToList();
				foreach (Rectangle rectangle in batch) {
					yield return rectangle;
				}
			}
		}

		public void InsertRectangles(List<Rectangle> rectangles) {
			_dbContext.Rectangles.AddRange(rectangles);
			_dbContext.SaveChanges();
		}
	}
}