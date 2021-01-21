using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _30seconds.Models
{
    public class LobbyViewModel
    {
        public LobbyViewModel()
        {
        }

        public List<Room> Rooms { get; set; }
        public Room NewRoom { get; set; }
        public List<Wordlist> Wordlists { get; set; }
    }
}
