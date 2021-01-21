using _30seconds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _30seconds.Repositories {
	public interface IGameRepository {
		Task<Game> GetNewGame(int IdRoom, string User);
		Task<Game> GetOrCreateGame(int IdRoom, string User);
	}
}
