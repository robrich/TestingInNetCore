namespace TestingInNetCore.Web.Models {
	using System.Collections.Generic;
	using TestingInNetCore.Entity;

	public class LockViewModel {
		public Lock Lock { get; set; }
		public List<LockType> AllLockTypes { get; set; }
		public List<Door> AllDoors { get; set; }
	}
}
