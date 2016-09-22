namespace TestingInNetCore.Entity {
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	public class Room {
		[Key]
		public int RoomId { get; set; }
		[Required]
		[StringLength(50)]
		public string RoomName { get; set; }
		public List<Door> Doors { get; set; }
	}
}
