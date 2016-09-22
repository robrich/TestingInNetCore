namespace TestingInNetCore.Repository {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Microsoft.EntityFrameworkCore;
	using TestingInNetCore.DataAccess;
	using TestingInNetCore.Entity;

	public interface ILockRepository {
		List<Lock> GetAll();
		List<Lock> GetAllInclude();
		List<Lock> GetForDoor(int DoorId);
		Lock GetById(int LockId);
		void Add(Lock Lock);
		void Update(Lock Lock);
	}

	public class LockRepository : ILockRepository {
		private readonly LockSmithContext lockSmithContext;

		public LockRepository(LockSmithContext LockSmithContext) {
			if (LockSmithContext == null) {
				throw new ArgumentNullException(nameof(LockSmithContext));
			}
			this.lockSmithContext = LockSmithContext;
		}

		public List<Lock> GetAll() {
			return (
				from l in this.lockSmithContext.Lock
				orderby l.Door.DoorName, l.LockType
				select l
			).ToList();
		}

		public List<Lock> GetAllInclude() {
			return (
				from l in this.lockSmithContext.Lock.Include(d => d.Door.Room)
				orderby l.Door.DoorName, l.LockType
				select l
			).ToList();
		}

		public List<Lock> GetForDoor(int DoorId) {
			return (
				from d in this.lockSmithContext.Lock
				where d.DoorId == DoorId
				select d
			).ToList();
		}

		public Lock GetById(int LockId) {
			if (LockId < 1) {
				return null;
			}
			return this.lockSmithContext.Lock.FirstOrDefault(b => b.LockId == LockId);
		}

		public void Add(Lock Lock) {
			this.lockSmithContext.Lock.Add(Lock);
			this.lockSmithContext.SaveChanges();
		}

		// FRAGILE: ASSUME: it's still attached
		public void Update(Lock Lock) {
			this.lockSmithContext.SaveChanges();
		}

	}
}
