namespace TestingInNetCore.Service {
	using System;
	using System.Collections.Generic;
	using TestingInNetCore.Entity;
	using TestingInNetCore.Repository;

	public interface IDoorLocker {
		void LockTheLock(int LockId, bool IsUnlocked);
		void LockEverything(bool IsUnlocked);
	}

	public class DoorLocker : IDoorLocker {
		private readonly ILockRepository lockRepository;

		public DoorLocker(ILockRepository LockRepository) {
			if (LockRepository == null) {
				throw new ArgumentNullException(nameof(LockRepository));
			}
			this.lockRepository = LockRepository;
		}


		public void LockTheLock(int LockId, bool IsUnlocked) {
			Lock lck = this.lockRepository.GetById(LockId);
			lck.IsUnlocked = IsUnlocked;
			this.lockRepository.Update(lck);
		}

		public void LockEverything(bool IsUnlocked) {
			List<Lock> locks = this.lockRepository.GetAll();
			locks.ForEach(l => {
				l.IsUnlocked = IsUnlocked;
				this.lockRepository.Update(l);
			});
		}
		
	}
}
