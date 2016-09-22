namespace TestingInNetCore.Repository {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Microsoft.EntityFrameworkCore;
	using TestingInNetCore.DataAccess;
	using TestingInNetCore.Entity;

	public interface IDoorRepository {
		List<Door> GetAll();
		List<Door> GetAllInclude();
		List<Door> GetForRoom(int RoomId);
		Door GetById(int DoorId);
		void Add(Door Door);
		void Update(Door Door);
	}

	public class DoorRepository : IDoorRepository {
		private readonly LockSmithContext lockSmithContext;

		public DoorRepository(LockSmithContext LockSmithContext) {
			if (LockSmithContext == null) {
				throw new ArgumentNullException(nameof(LockSmithContext));
			}
			this.lockSmithContext = LockSmithContext;
		}

		public List<Door> GetAll() {
			return (
				from d in this.lockSmithContext.Door
				orderby d.DoorName
				select d
			).ToList();
		}

		public List<Door> GetAllInclude() {
			return (
				from d in this.lockSmithContext.Door.Include(d => d.Room)
				orderby d.Room.RoomName, d.DoorName
				select d
			).ToList();
		}

		public List<Door> GetForRoom(int RoomId) {
			return (
				from d in this.lockSmithContext.Door
				where d.RoomId == RoomId
				select d
			).ToList();
		}

		public Door GetById(int DoorId) {
			if (DoorId < 1) {
				return null;
			}
			return this.lockSmithContext.Door.FirstOrDefault(b => b.DoorId == DoorId);
		}

		public void Add(Door Door) {
			this.lockSmithContext.Door.Add(Door);
			this.lockSmithContext.SaveChanges();
		}

		// FRAGILE: ASSUME: it's still attached
		public void Update(Door Door) {
			this.lockSmithContext.SaveChanges();
		}

	}
}
