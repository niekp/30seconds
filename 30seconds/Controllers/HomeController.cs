using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using _30seconds.Models;
using _30seconds.Data;
using Microsoft.EntityFrameworkCore;

namespace _30seconds.Controllers
{
    public class HomeController : Controller
    {
        private readonly GameContext gameContext;

        public HomeController (
            GameContext gameContext
        )
        {
            this.gameContext = gameContext;
        }

        // TODO: Verplaats naar een repo
        private Task<List<Room>> GetRooms()
        {
            return gameContext.Room.Where(
                r => r.LastPing >= DateTime.Now.AddMinutes(-5)
                || r.Games.Where(g => g.Start >= DateTime.Now.AddMinutes(-5)).Any()
            ).Include(r => r.Games).ToListAsync();
        }

        // TODO: Verplaats naar een repo
        private Task<Room> GetRoom(int Id)
        {
            return gameContext.Room.Where(r => r.Id == Id)
                .Include(r => r.Games)
                .FirstOrDefaultAsync();
        }

        public async Task<IActionResult> Index()
        {
            return View(await GetRooms());
        }

        public async Task<IActionResult> Game(int Id)
        {
            var room = await GetRoom(Id);
            if (!(room is Room))
            {
                return RedirectToAction(nameof(Index));
            }

            return View(room);
        }


    }
}
