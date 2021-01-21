using _30seconds.Models;
using _30seconds.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace _30seconds.Controllers {
	public class GameController : Controller {
		private readonly IGameRepository gameRepository;

		public GameController(
			IGameRepository gameRepository
		) {
			this.gameRepository = gameRepository;
		}


		public Task<Game> GetGame(int IdRoom) {
			return gameRepository.GetGame(IdRoom);
		}

		public Task<Game> CreateGame(int IdRoom, string User) {
			return gameRepository.GetNewGame(IdRoom, User);
		}

	}
}
