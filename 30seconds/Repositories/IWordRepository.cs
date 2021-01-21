using _30seconds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _30seconds.Repositories {
	public interface IWordRepository {
		Task<List<Word>> GetWords(int IdWordlist, int Amount = 5);
	}
}
