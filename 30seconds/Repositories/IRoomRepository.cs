using _30seconds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _30seconds.Repositories {
	public interface IRoomRepository {
		Task<Room> CreateRoom(string Name, int IdWordlist, int AmountOfSeconds = 30);
		Task<Room> GetRoom(int Id);
		Task<List<Room>> GetRooms();
	}
}
