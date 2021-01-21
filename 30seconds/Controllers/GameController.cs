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

namespace _30seconds.Controllers
{
    public class GameController : Controller
    {
        private readonly GameContext gameContext;

        public GameController(GameContext gameContext)
        {
            this.gameContext = gameContext;
        }


        private Task<List<Word>> GetWords(int IdWordlist, int Amount = 5)
        {
            return gameContext.Word.Where(
                w => w.IdWordlist == IdWordlist
            ).OrderBy(x => Guid.NewGuid()).Take(Amount)
            .ToListAsync();

            //var words = System.IO.File.ReadAllLines("woordenlijst.txt");
        }

        private async Task<Game> GetNewGame(int IdRoom, string User)
        {
            var game = new Game()
            {
                IdRoom = IdRoom,
                Start = DateTime.Now,
                User = User
            };
            foreach (var word in await GetWords(5)) {
                game.Words.Add(word);
			}

            gameContext.Add(game);

            await gameContext.SaveChangesAsync();

            return game;
        }

        private async Task<Game> GetOrCreateGame(int IdRoom, string User)
        {
            var game = await gameContext.Game.Where(
                g => g.IdRoom == IdRoom
            ).Include(g => g.Words)
            .LastOrDefaultAsync();

            if (!(game is Game))
            {
                game = await GetNewGame(IdRoom, User);
            }

            return game;
        }

        public Task<Game> GetGame(int IdRoom, string User, bool forceNew = false)
        {
            if (!forceNew) {
                return GetOrCreateGame(IdRoom, User);
			} else {
                return GetNewGame(IdRoom, User);
			}
        }

    }
}
