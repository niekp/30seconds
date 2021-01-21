using _30seconds.Data;
using _30seconds.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _30seconds.Repositories {
	public class WordlistRepository : IWordlistRepository {
		private readonly GameContext db;

		public WordlistRepository(GameContext db) {
			this.db = db;
		}

		public Task<List<Wordlist>> GetWordlists() {
			return db.Wordlist.ToListAsync();
		}

	}
}
