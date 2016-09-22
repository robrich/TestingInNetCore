namespace TestingInNetCore.Service {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using TestingInNetCore.Entity;
	using TestingInNetCore.Repository;

	public interface IDashboardGatherer {
		List<Room> GetAllData();
	}

	public class DashboardGatherer : IDashboardGatherer {
		private readonly ILockRepository lockRepository;
		private readonly IDoorRepository doorRepository;
		private readonly IRoomRepository roomRepository;

		public DashboardGatherer(ILockRepository LockRepository, IDoorRepository DoorRepository, IRoomRepository RoomRepository) {
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

		public List<Room> GetAllData() {
			List<Room> rooms = this.roomRepository.GetAll();
			List<Door> doors = this.doorRepository.GetAll();
			List<Lock> locks = this.lockRepository.GetAll();

			rooms.ForEach(r => r.Doors = new List<Door>());
			doors.ForEach(d => d.Locks = new List<Lock>());

			foreach (Door door in doors) {
				Room room = rooms.FirstOrDefault(r => r.RoomId == door.RoomId);
				room.Doors.Add(door);
			}
			foreach (Lock lck in locks) {
				Door door = doors.FirstOrDefault(d => d.DoorId == lck.DoorId);
				door.Locks.Add(lck);
			}

			return rooms;
		}

	}
}
