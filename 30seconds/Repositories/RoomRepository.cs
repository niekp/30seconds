using _30seconds.Data;
using _30seconds.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _30seconds.Repositories {
	public class RoomRepository : IRoomRepository {
		private readonly GameContext db;

		public RoomRepository(GameContext db) {
			this.db = db;
		}

        public Task<List<Room>> GetRooms() {
            return db.Room.Where(
                r => r.LastPing >= DateTime.Now.AddMinutes(-5)
                || r.Games.Where(g => g.Start >= DateTime.Now.AddMinutes(-5)).Any()
            ).Include(r => r.Games).ToListAsync();
        }

        public Task<Room> GetRoom(int Id) {
            return db.Room.Where(r => r.Id == Id)
                .Include(r => r.Games)
                .FirstOrDefaultAsync();
        }

        public async Task<Room> CreateRoom(string Name, int IdWordlist, int AmountOfSeconds = 30) {
            var room = new Room() {
                Name = Name,
                IdWordlist = IdWordlist,
                AmountOfSeconds = AmountOfSeconds
            };

            db.Add(room);
            await db.SaveChangesAsync();

            return room;
        }

    }
}
