namespace TestingInNetCore.Web.Models
{
	using System.Collections.Generic;
	using TestingInNetCore.Entity;

	public class DoorViewModel
	{
		public Door Door { get; set; }
		public List<Room> AllRooms { get; set; }
	}
}
