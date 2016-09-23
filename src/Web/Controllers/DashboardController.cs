namespace TestingInNetCore.Web.Controllers {
	using System;
	using System.Collections.Generic;
	using Microsoft.AspNetCore.Mvc;
	using TestingInNetCore.Entity;
	using TestingInNetCore.Repository;
	using TestingInNetCore.Service;

	public class DashboardController : Controller {
		private readonly ILockRepository lockRepository;
		private readonly IDashboardGatherer dashboardGatherer;
		private readonly IDoorLocker doorLocker;
		private readonly IRandomDoorLocker randomDoorLocker;

		public DashboardController(ILockRepository LockRepository, IDashboardGatherer DashboardGatherer, IDoorLocker DoorLocker, IRandomDoorLocker RandomDoorLocker) {
			if (LockRepository == null) {
				throw new ArgumentNullException(nameof(LockRepository));
			}
			if (DashboardGatherer == null) {
				throw new ArgumentNullException(nameof(DashboardGatherer));
			}
			if (DoorLocker == null) {
				throw new ArgumentNullException(nameof(DoorLocker));
			}
			if (RandomDoorLocker == null) {
				throw new ArgumentNullException(nameof(RandomDoorLocker));
			}
			this.lockRepository = LockRepository;
			this.dashboardGatherer = DashboardGatherer;
			this.doorLocker = DoorLocker;
			this.randomDoorLocker = RandomDoorLocker;
		}

		public IActionResult Index() {
			return View(this.dashboardGatherer.GetAllData());
		}

		public IActionResult LockTheLock(int LockId, bool IsUnlocked) {
			Lock lck = this.lockRepository.GetById(LockId);
			this.doorLocker.LockTheLock(lck, IsUnlocked);
			this.lockRepository.Update(lck);
			return Json(new {success = true});
		}

		public IActionResult LockEverything(bool IsUnlocked) {
			this.doorLocker.LockEverything(IsUnlocked);
			return Json(new { success = true });
		}

		public IActionResult LockRandomDoor(bool IsUnlocked) {
			Door door = this.randomDoorLocker.LockRandomDoor(IsUnlocked);
			return Json(new { success = true, DoorId = door.DoorId });
		}

	}
}
