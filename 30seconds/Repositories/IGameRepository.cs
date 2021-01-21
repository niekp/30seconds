using _30seconds.Models;
using System.Threading.Tasks;

namespace _30seconds.Repositories {
	public interface IGameRepository {
		Task<Game> GetNewGame(int IdRoom, string User);
		Task<Game> GetGame(int IdRoom);
	}
}
