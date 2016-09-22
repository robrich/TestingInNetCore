namespace TestingInNetCore.Repository {
	using System;
	using TestingInNetCore.Entity;
	using System.Collections.Generic;
	using System.Linq;

	public interface ILockTypeRepository {
		List<LockType> GetAll();
	}

	public class LockTypeRepository : ILockTypeRepository {

		// https://robrich.org/archive/2010/11/18/get-all-enum-values-as-a-list.aspx
		public List<LockType> GetAll() => Enum.GetValues(typeof(LockType)).Cast<LockType>().OrderBy(l => l).ToList();

	}
}
