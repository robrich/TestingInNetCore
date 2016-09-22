namespace TestingInNetCore.Repository {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Microsoft.EntityFrameworkCore;
	using TestingInNetCore.DataAccess;
	using TestingInNetCore.Entity;

	public interface IRoomRepository {
		List<Room> GetAll();
		Room GetById(int RoomId);
		void Add(Room Room);
		void Update(Room Room);
	}

	public class RoomRepository : IRoomRepository {
		private readonly LockSmithContext lockSmithContext;

		public RoomRepository(LockSmithContext LockSmithContext) {
			if (LockSmithContext == null) {
				throw new ArgumentNullException(nameof(LockSmithContext));
			}
			this.lockSmithContext = LockSmithContext;
		}

		public List<Room> GetAll() {
			return (
				from r in this.lockSmithContext.Room
				orderby r.RoomName
				select r
			).ToList();
		}
		
		public Room GetById(int RoomId) {
			if (RoomId < 1) {
				return null;
			}
			return this.lockSmithContext.Room.FirstOrDefault(b => b.RoomId == RoomId);
		}

		public void Add(Room Room) {
			this.lockSmithContext.Room.Add(Room);
			this.lockSmithContext.SaveChanges();
		}

		// FRAGILE: ASSUME: it's still attached
		public void Update(Room Room) {
			this.lockSmithContext.SaveChanges();
		}

	}
}
