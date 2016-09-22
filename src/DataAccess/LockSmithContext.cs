namespace TestingInNetCore.DataAccess {
	using Microsoft.EntityFrameworkCore;
	using TestingInNetCore.Entity;

	/// <summary>
	/// FRAGILE: property names here match table names
	/// </summary>
	public class LockSmithContext : DbContext {

		public LockSmithContext(DbContextOptions<LockSmithContext> options)
			: base(options) { }

		public DbSet<Room> Room { get; set; }
		public DbSet<Door> Door { get; set; }
		public DbSet<Lock> Lock { get; set; }

	}
}
