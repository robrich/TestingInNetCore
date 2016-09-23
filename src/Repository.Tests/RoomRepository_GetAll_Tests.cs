namespace TestingInNetCore.Repository.Tests {
	using System.Linq;
	using FluentAssertions;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.DependencyInjection;
	using TestingInNetCore.DataAccess;
	using TestingInNetCore.Entity;
	using Xunit;

	// 3. Use an in-memory database
	public class RoomRepository_GetAll_Tests {

		// https://docs.efproject.net/en/latest/miscellaneous/testing.html
		protected DbContextOptions<LockSmithContext> CreateNewContextOptions() {
			// Create a fresh service provider, and therefore a fresh
			// InMemory database instance.
			var serviceProvider = new ServiceCollection()
				.AddEntityFrameworkInMemoryDatabase()
				.BuildServiceProvider();

			// Create a new options instance telling the context to use an
			// InMemory database and the new service provider.
			var builder = new DbContextOptionsBuilder<LockSmithContext>();
			builder.UseInMemoryDatabase()
				   .UseInternalServiceProvider(serviceProvider);

			return builder.Options;
		}

		[Fact]
		public void CanGetAllFromInMemoryDatabase() {

			// Arrange
			string roomName = "test room";

			var options = CreateNewContextOptions();

			using (var db1 = new LockSmithContext(options)) {
				db1.Room.Add(new Room {
					RoomName = roomName
				});
				db1.SaveChanges();
			}

			using (var db2 = new LockSmithContext(options)) {

				// Act
				RoomRepository roomRepository = new RoomRepository(db2);
				var locks = roomRepository.GetAll();

				// Assert
				locks.Count.Should().Be(1);
				locks.First().RoomName.Should().Be(roomName);
			}

		}

	}
}
