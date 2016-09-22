namespace TestingInNetCore.Web.Controllers {
	using System;
	using Microsoft.AspNetCore.Mvc;
	using TestingInNetCore.Entity;
	using TestingInNetCore.Repository;
	using TestingInNetCore.Web.Models;

	public class DoorController : Controller {
		private readonly IDoorRepository doorRepository;
		private readonly IRoomRepository roomRepository;

		public DoorController(IDoorRepository DoorRepository, IRoomRepository RoomRepository) {
			if (DoorRepository == null) {
				throw new ArgumentNullException(nameof(DoorRepository));
			}
			if (RoomRepository == null) {
				throw new ArgumentNullException(nameof(RoomRepository));
			}
			this.doorRepository = DoorRepository;
			this.roomRepository = RoomRepository;
		}

		public IActionResult Index() {
			return View(this.doorRepository.GetAllInclude());
		}

		public IActionResult Create() {
			DoorViewModel model = new DoorViewModel {
				Door = new Door(),
				AllRooms = this.roomRepository.GetAll()
			};
			return View(model);
		}

		[HttpPost]
		public IActionResult Create(Door Model) {
			if (!this.ModelState.IsValid) {
				// fix your data
				DoorViewModel model = new DoorViewModel {
					Door = Model,
					AllRooms = this.roomRepository.GetAll()
				};
				return this.View(model);
			}
			this.doorRepository.Add(Model);
			return this.RedirectToAction("Index");
		}

		public IActionResult Edit(int id) {
			Door door = this.doorRepository.GetById(id);
			if (door == null) {
				return this.View("NotFound");
			}
			DoorViewModel model = new DoorViewModel {
				Door = door,
				AllRooms = this.roomRepository.GetAll()
			};
			return View(model);
		}

		[HttpPost]
		public IActionResult Edit(int id, Door Model) {
			if (!this.ModelState.IsValid) {
				// fix your data
				DoorViewModel model = new DoorViewModel {
					Door = Model,
					AllRooms = this.roomRepository.GetAll()
				};
				return this.View(model);
			}
			Door entity = this.doorRepository.GetById(id);
			if (entity == null) {
				return this.View("NotFound");
			}
			entity.RoomId = Model.RoomId;
			entity.DoorName = Model.DoorName;
			this.doorRepository.Update(entity);
			return this.RedirectToAction("Index");
		}

	}
}
