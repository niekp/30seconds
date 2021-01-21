using _30seconds.Data;
using _30seconds.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace _30seconds.Repositories {
	public class GameRepository : IGameRepository {
		private readonly GameContext db;
		private readonly IWordRepository wordRepository;

		public GameRepository(
			GameContext db,
			IWordRepository wordRepository
		) {
			this.db = db;
			this.wordRepository = wordRepository;
		}

		public async Task<Game> GetNewGame(int IdRoom, string User) {
			var room = await db.Room.Where(r => r.Id == IdRoom).FirstOrDefaultAsync();
			if (!(room is Room)) {
				throw new ArgumentException("Invalid room.");
			}

			var game = new Game() {
				IdRoom = IdRoom,
				Room = room,
				Start = DateTime.Now,
				User = User
			};
			foreach (var word in await wordRepository.GetWords(room.IdWordlist, 5)) {
				db.Attach(word);
				game.Words.Add(word);
			}

			db.Add(game);

			await db.SaveChangesAsync();

			return game;
		}

		public async Task<Game> GetOrCreateGame(int IdRoom, string User) {
			var game = await db.Game.Where(
				g => g.IdRoom == IdRoom
			).Include(g => g.Words)
			.Include(g => g.Room)
			.OrderByDescending(g => g.Start)
			.FirstOrDefaultAsync();

			if (!(game is Game)) {
				game = await GetNewGame(IdRoom, User);
			}

			return game;
		}
	}
}
