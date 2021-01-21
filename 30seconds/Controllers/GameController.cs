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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        private Task<Game> GetNewGame(int IdRoom, string User)
        {
            var game = new Game()
            {
                Words = GetWords(5),
                IdRoom = IdRoom,
                Start = DateTime.Now,
                User = User
            };
        }

        private async Task<Game> GetOrCreateGame(int IdRoom, string User)
        {
            var game = await gameContext.Game.Where(
                g => g.IdRoom == IdRoom
            ).LastOrDefaultAsync();

            if (!(game is Game))
            {
                game = await GetNewGame(IdRoom, User);
            }

            return game;
        }

        public Game GetGame(int IdRoom, string User, bool forceNew = false)
        {
            Game game = null;
            if (System.IO.File.Exists(_filename) && !forceNew)
            {
                using StreamReader r = new StreamReader(_filename);
                string json = r.ReadToEnd();
                game = JsonConvert.DeserializeObject<Game>(json);
            }

            if (game is not Game) {
                game = new Game()
                {
                    Words = GetWords(5),
                    Start = DateTime.Now,
                    User = User
                };

                var json = JsonConvert.SerializeObject(game, Formatting.Indented);
                System.IO.File.WriteAllText(_filename, json);
            }

            return game;
        }

    }
}
