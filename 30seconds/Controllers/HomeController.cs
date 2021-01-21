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
using _30seconds.Repositories;

namespace _30seconds.Controllers
{
    public class HomeController : Controller
    {
		private readonly IRoomRepository roomRepository;
		private readonly IWordlistRepository wordlistRepository;

        public HomeController (
            IRoomRepository roomRepository,
            IWordlistRepository wordlistRepository
        )
        {
			this.roomRepository = roomRepository;
			this.wordlistRepository = wordlistRepository;
        }

        public async Task<IActionResult> Index()
        {
            var lobby = new LobbyViewModel() {
                Rooms = await roomRepository.GetRooms(),
                NewRoom = new Room(),
                Wordlists = await wordlistRepository.GetWordlists()
            };

            return View(lobby);
        }

        [HttpPost]
        public async Task<IActionResult> Index(LobbyViewModel lobbyViewModel) {
            var room = await roomRepository.CreateRoom(lobbyViewModel.NewRoom.Name,
                lobbyViewModel.NewRoom.IdWordlist,
                lobbyViewModel.NewRoom.AmountOfSeconds
            );

            return RedirectToAction(nameof(Game), new { room.Id });
		}

        public async Task<IActionResult> Game(int Id)
        {
            var room = await roomRepository.GetRoom(Id);
            if (!(room is Room))
            {
                return RedirectToAction(nameof(Index));
            }

            return View(room);
        }


    }
}
