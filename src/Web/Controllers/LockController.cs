namespace TestingInNetCore.Web.Controllers {
	using System;
	using Microsoft.AspNetCore.Mvc;
	using TestingInNetCore.Entity;
	using TestingInNetCore.Repository;
	using TestingInNetCore.Web.Models;

	public class LockController : Controller {
		private readonly ILockRepository lockRepository;
		private readonly IDoorRepository doorRepository;
		private readonly ILockTypeRepository lockTypeRepository;

		public LockController(ILockRepository LockRepository, IDoorRepository DoorRepository, ILockTypeRepository LockTypeRepository) {
			if (LockRepository == null) {
				throw new ArgumentNullException(nameof(LockRepository));
			}
			if (DoorRepository == null) {
				throw new ArgumentNullException(nameof(DoorRepository));
			}
			if (LockTypeRepository == null) {
				throw new ArgumentNullException(nameof(LockTypeRepository));
			}
			this.lockRepository = LockRepository;
			this.doorRepository = DoorRepository;
			this.lockTypeRepository = LockTypeRepository;
		}

		public IActionResult Index() {
			return View(this.lockRepository.GetAllInclude());
		}

		public IActionResult Create() {
			LockViewModel model = new LockViewModel {
				Lock = new Lock(),
				AllLockTypes = this.lockTypeRepository.GetAll(),
				AllDoors = this.doorRepository.GetAllInclude()
			};
			return View(model);
		}

		[HttpPost]
		public IActionResult Create(Lock Model) {
			if (!this.ModelState.IsValid) {
				// fix your data
				LockViewModel model = new LockViewModel {
					Lock = Model,
					AllLockTypes = this.lockTypeRepository.GetAll(),
					AllDoors = this.doorRepository.GetAllInclude()
				};
				return this.View(model);
			}
			this.lockRepository.Add(Model);
			return this.RedirectToAction("Index");
		}

		public IActionResult Edit(int id) {
			Lock l = this.lockRepository.GetById(id);
			if (l == null) {
				return this.View("NotFound");
			}
			LockViewModel model = new LockViewModel {
				Lock = l,
				AllLockTypes = this.lockTypeRepository.GetAll(),
				AllDoors = this.doorRepository.GetAllInclude()
			};
			return View(model);
		}

		[HttpPost]
		public IActionResult Edit(int id, Lock Model) {
			if (!this.ModelState.IsValid) {
				// fix your data
				LockViewModel model = new LockViewModel {
					Lock = Model,
					AllLockTypes = this.lockTypeRepository.GetAll(),
					AllDoors = this.doorRepository.GetAllInclude()
				};
				return this.View(model);
			}
			Lock entity = this.lockRepository.GetById(id);
			if (entity == null) {
				return this.View("NotFound");
			}
			entity.DoorId = Model.DoorId;
			entity.LockType = Model.LockType;
			this.lockRepository.Update(entity);
			return this.RedirectToAction("Index");
		}

	}
}
