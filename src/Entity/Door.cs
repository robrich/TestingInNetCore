namespace TestingInNetCore.Entity {
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	public class Door {
		[Key]
		public int DoorId { get; set; }
		[Required]
		[StringLength(50)]
		public string DoorName { get; set; }
		public int RoomId { get; set; }
		public Room Room { get; set; }
		public List<Lock> Locks { get; set; }
	}
}
