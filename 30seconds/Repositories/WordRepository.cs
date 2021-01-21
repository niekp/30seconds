using _30seconds.Data;
using _30seconds.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _30seconds.Repositories {
	public class WordRepository : IWordRepository {
		private readonly GameContext db;
		private readonly IMemoryCache cache;

		public WordRepository(
			GameContext db,
			IMemoryCache cache
		) {
			this.db = db;
			this.cache = cache;
		}

		private Task<List<Word>> GetWords(int IdWordlist) {
			return cache.GetOrCreateAsync(string.Format("GetWords_{0}", IdWordlist), c => {
				c.SetSlidingExpiration(TimeSpan.FromMinutes(30));

				return db.Word.Where(
					w => w.IdWordlist == IdWordlist
				).ToListAsync();
			});
		}

		public async Task<List<Word>> GetWords(int IdWordlist, int Amount = 5) {
			return (await GetWords(IdWordlist))
				.OrderBy(x => Guid.NewGuid())
				.Take(Amount)
				.ToList();
		}
	}
}
