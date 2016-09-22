namespace TestingInNetCore.Web.Controllers {
	using System;
	using System.Collections.Generic;
	using Microsoft.AspNetCore.Mvc;
	using TestingInNetCore.Entity;
	using TestingInNetCore.Repository;
	using TestingInNetCore.Service;

	public class DashboardController : Controller {
		private readonly IDashboardGatherer dashboardGatherer;
		private readonly IDoorLocker doorLocker;
		private readonly IRandomDoorLocker randomDoorLocker;

		public DashboardController(IDashboardGatherer DashboardGatherer, IDoorLocker DoorLocker, IRandomDoorLocker RandomDoorLocker) {
			if (DashboardGatherer == null) {
				throw new ArgumentNullException(nameof(DashboardGatherer));
			}
			if (DoorLocker == null) {
				throw new ArgumentNullException(nameof(DoorLocker));
			}
			if (RandomDoorLocker == null) {
				throw new ArgumentNullException(nameof(RandomDoorLocker));
			}
			this.dashboardGatherer = DashboardGatherer;
			this.doorLocker = DoorLocker;
			this.randomDoorLocker = RandomDoorLocker;
		}

		public IActionResult Index() {
			return View(this.dashboardGatherer.GetAllData());
		}

		public IActionResult LockTheLock(int LockId, bool IsUnlocked) {
			this.doorLocker.LockTheLock(LockId, IsUnlocked);
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
