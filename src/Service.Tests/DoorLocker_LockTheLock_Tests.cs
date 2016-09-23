namespace TestingInNetCore.Service.Tests {
	using System;
	using FluentAssertions;
	using TestingInNetCore.Entity;
	using Xunit;
	using Xunit.Sdk;

	public class DoorLocker_LockTheLock_Tests {

		[Fact]
		public void SetsLockToUnlocked() {

			// Arrange
			const bool expected = true;
			Lock lck = new Lock {
				IsUnlocked = !expected
			};

			// Act
			DoorLocker doorLocker = new DoorLocker(null);
			doorLocker.LockTheLock(lck, expected);

			// Assert
			lck.IsUnlocked.Should().Be(expected);

		}

		[Fact]
		public void SetsLockToLocked() {

			// Arrange
			const bool expected = false;
			Lock lck = new Lock {
				IsUnlocked = !expected
			};

			// Act
			DoorLocker doorLocker = new DoorLocker(null);
			doorLocker.LockTheLock(lck, expected);

			// Assert
			lck.IsUnlocked.Should().Be(expected);

		}

		[Fact]
		public void GracefullyHandlesANullLock() {

			// Arrange
			const bool expected = false;
			Lock lck = null;

			// Act
			DoorLocker doorLocker = new DoorLocker(null);
			bool threw = false;
			try {
				doorLocker.LockTheLock(lck, expected);

				// Assert
				// We're still here, it worked

			} catch (ArgumentNullException ex) {
				ex.ParamName.Should().Be(typeof(Lock).Name);
				threw = true;
			}

			threw.Should().Be(true);

		}

	}
}
