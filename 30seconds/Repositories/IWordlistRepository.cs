using _30seconds.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _30seconds.Repositories {
	public interface IWordlistRepository {
		Task<List<Wordlist>> GetWordlists();
	}
}
