namespace TestingInNetCore.Service.Tests {
	using System;
	using System.Collections.Generic;
	using FluentAssertions;
	using Moq;
	using TestingInNetCore.Entity;
	using TestingInNetCore.Repository;
	using Xunit;

	public class DoorLocker_LockEverything_Tests {
		
		[Theory]
		[InlineData(1, true)]
		[InlineData(2, false)]
		[InlineData(10, true)]
		public void LocksEverything(int expected, bool locked) {
			
			// Arrange
			int counter = 0;
			List<Lock> db = new List<Lock>();
			for (int i = 0; i < expected; i++) { 
				db.Add(new Lock {
					IsUnlocked = !locked
				});
			}

			Mock<ILockRepository> lockRepository = this.GetMockLockRepository(db, () => counter++);

			// Act
			DoorLocker doorLocker = new DoorLocker(lockRepository.Object);
			doorLocker.LockEverything(locked);

			// Assert
			counter.Should().Be(expected, "it should call update for as many items as are in the list");

		}

		
		private Mock<ILockRepository> GetMockLockRepository(List<Lock> db, Action CalledUpdate) {
			Mock<ILockRepository> lockRepository = new Mock<ILockRepository>(MockBehavior.Loose);
			lockRepository.Setup(m => m.GetAll()).Returns(db);
			lockRepository.Setup(m => m.Update(It.IsAny<Lock>())).Callback(CalledUpdate);
			return lockRepository;
		}

	}
}
