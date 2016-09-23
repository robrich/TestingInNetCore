namespace TestingInNetCore.Service.Tests {
	using System.Collections.Generic;
	using FluentAssertions;
	using Moq;
	using TestingInNetCore.Entity;
	using TestingInNetCore.Repository;
	using Xunit;

	public class DoorLocker_Tests {

		// 2. a logic test
		[Fact]
		public void LockTheLock_SetsLock_True() {
			
			// Arrange
			const bool expected = true;
			Lock lck = new Lock {
				IsUnlocked = !expected
			};

			// Act
			DoorLocker locker = new DoorLocker(null);
			locker.LockTheLock(lck, expected);

			// Assert
			lck.IsUnlocked.Should().Be(expected);

		}

		// 3. let's mock things
		[Fact]
		public void LockEverything_Saves_Item() {

			// Arrange
			const bool expected = true;
			bool updateCalled = false;
			Lock lck = new Lock {
				IsUnlocked = !expected
			};
			Mock<ILockRepository> mockRepository = new Mock<ILockRepository>(MockBehavior.Loose);
			mockRepository.Setup(m => m.GetAll()).Returns(new List<Lock> { lck });
			mockRepository.Setup(m => m.Update(It.IsAny<Lock>())).Callback(() => updateCalled = true);


			// Act
			DoorLocker locker = new DoorLocker(mockRepository.Object);
			locker.LockEverything(expected);

			// Assert
			updateCalled.Should().Be(true, "It didn't save");
			mockRepository.VerifyAll();

		}

	}
}
