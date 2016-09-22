namespace TestingInNetCore.Entity {
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Lock {
		[Key]
		public int LockId { get; set; }
		public int DoorId { get; set; }
		public Door Door { get; set; }
		public bool IsUnlocked { get; set; }
		[Column("LockTypeId")]
		public LockType LockType { get; set; }
	}
}
