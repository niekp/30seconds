using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using _30seconds.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _30seconds.Controllers
{
    public class GameController : Controller
    {
        private string _filename = "30seconds.json";

        private List<string> GetWords(int Amount = 5)
        {
            var words = System.IO.File.ReadAllLines("woordenlijst.txt");

            return words.OrderBy(x => Guid.NewGuid()).Take(Amount).ToList();
        }

        public Game GetGame(string User, bool forceNew = false)
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
