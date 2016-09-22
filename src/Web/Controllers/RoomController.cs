namespace TestingInNetCore.Web.Controllers {
	using System;
	using Microsoft.AspNetCore.Mvc;
	using TestingInNetCore.Entity;
	using TestingInNetCore.Repository;

	public class RoomController : Controller {
		private readonly IRoomRepository roomRepository;

		public RoomController(IRoomRepository RoomRepository) {
			if (RoomRepository == null) {
				throw new ArgumentNullException(nameof(RoomRepository));
			}
			this.roomRepository = RoomRepository;
		}

		public IActionResult Index() {
			return View(this.roomRepository.GetAll());
		}

		public IActionResult Create() {
			return View();
		}

		[HttpPost]
		public IActionResult Create(Room Model) {
			if (!this.ModelState.IsValid) {
				return this.View(Model); // fix your data
			}
			this.roomRepository.Add(Model);
			return this.RedirectToAction("Index");
		}

		public IActionResult Edit(int id) {
			Room room = this.roomRepository.GetById(id);
			if (room == null) {
				return this.View("NotFound");
			}
			return View(room);
		}

		[HttpPost]
		public IActionResult Edit(int id, Room Model) {
			if (!this.ModelState.IsValid) {
				return this.View(Model);
			}
			Room entity = this.roomRepository.GetById(id);
			if (entity == null) {
				return this.View("NotFound");
			}
			entity.RoomName = Model.RoomName;
			this.roomRepository.Update(entity);
			return this.RedirectToAction("Index");
		}

	}
}
