namespace TestingInNetCore.Service {
	using System;
	using TestingInNetCore.Entity;
	using TestingInNetCore.Repository;

	public interface IRandomDoorLocker {
		Door LockRandomDoor(bool IsUnlocked);
	}

	public class RandomDoorLocker : IRandomDoorLocker {
		private readonly ILockRepository lockRepository;
		private readonly IDoorRepository doorRepository;
		private readonly IRoomRepository roomRepository;

		public RandomDoorLocker(ILockRepository LockRepository, IDoorRepository DoorRepository, IRoomRepository RoomRepository) {
			if (LockRepository == null) {
				throw new ArgumentNullException(nameof(LockRepository));
			}
			if (DoorRepository == null) {
				throw new ArgumentNullException(nameof(DoorRepository));
			}
			if (RoomRepository == null) {
				throw new ArgumentNullException(nameof(RoomRepository));
			}
			this.lockRepository = LockRepository;
			this.doorRepository = DoorRepository;
			this.roomRepository = RoomRepository;
		}

		public Door LockRandomDoor(bool IsUnlocked) {
			throw new NotImplementedException();
		}

	}
}
