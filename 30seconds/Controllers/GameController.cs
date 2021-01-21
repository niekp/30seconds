using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using _30seconds.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using _30seconds.Data;
using Microsoft.EntityFrameworkCore;
using _30seconds.Repositories;

namespace _30seconds.Controllers
{
    public class GameController : Controller
    {
		private readonly IGameRepository gameRepository;

		public GameController(
            IGameRepository gameRepository
        )
        {
			this.gameRepository = gameRepository;
		}


        public Task<Game> GetGame(int IdRoom, string User, bool forceNew = false)
        {
            if (!forceNew) {
                return gameRepository.GetOrCreateGame(IdRoom, User);
			} else {
                return gameRepository.GetNewGame(IdRoom, User);
			}
        }

    }
}
